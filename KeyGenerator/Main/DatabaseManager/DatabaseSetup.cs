using KeyGenerator.Classes.Database;
using KeyGenerator.Codes;
using Spectre.Console;

namespace KeyGenerator.Main.DatabaseManager;

public class DatabaseSetup
{
    public void Main()
    {
        Console.WriteLine("Database Setup");
        Console.WriteLine(
            "This Setup will guide you through the process of setting up a database to store you keys in.");
        Console.WriteLine(
            "You will need to provide the following information and have to bring some experience with databases:");
        Console.WriteLine(" - Database Type (MySQL, SQLite)");
        Console.WriteLine(" - Database Connection Details");
        Console.WriteLine(" - The Table and Structure itself");
        if (AnsiConsole.Confirm("Do you want to continue?"))
        {
            Console.WriteLine("Please select the Database Type you want to use.");
            Console.WriteLine("Currently supported: MySQL");
            var databaseType = AnsiConsole.Prompt(new SelectionPrompt<DatabaseType>()
                .Title("What Database Type do you want to use?")
                .WrapAround(true)
                .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                .PageSize(10)
                .AddChoices(new[] { DatabaseType.MySQL }));
            switch (databaseType)
            {
                case DatabaseType.MySQL:
                    SetupMySQL();
                    break;
                default:
                    Console.WriteLine("This Database Type is not supported!");
                    break;
            }
        }
        else
        {
            new Menu().Main();
        }
    }

    private void SetupMySQL()
    {
        var userAccepted = false;
        var db = new MySqlDatabaseInformation();
        db.Name = AnsiConsole.Ask<string>("What is the Name of the Database? (Only used for you to identify it)");
        if (SystemHandler.DatabaseManager.Databases.Any(x => x.Name == db.Name))
        {
            Console.WriteLine("A Database with this name already exists!");
            db.Name = AnsiConsole.Ask<string>("What is the Name of the Database? (Only used for you to identify it)");
        }
        while (!userAccepted)
        {
            var server = AnsiConsole.Ask<string>("What is the Server of the Database?");
            var port = AnsiConsole.Ask<int>("What is the Port of the Database?", 3306);
            var database = AnsiConsole.Ask<string>("What is the Database?");
            var uid = AnsiConsole.Ask<string>("What is the Username?");
            var password = AnsiConsole.Ask<string>("What is the Password?");
            var useSsl = AnsiConsole.Confirm("Do you want to use SSL? (SSLMode=Preferred)");
            var forceSsl = useSsl && AnsiConsole.Confirm("Do you want to force SSL? (SSLMode=Required)");
            var sslMode = useSsl ? forceSsl ? "Required" : "Preferred" : "None";
            db.ConnectionString = $"Server={server};Port={port};Database={database};Uid={uid};Pwd={password};SSLMode={sslMode};";
            Console.WriteLine("Your connection string looks like this: ");
            Console.WriteLine(db.ConnectionString);
            userAccepted = AnsiConsole.Confirm("Is this correct? (You can change it later anyway)");
        }

        var connectionSuccessful = false;
        AnsiConsole.Status()
            .Start("Checking database connection...", ctx =>
            {
                Console.WriteLine("Connecting to database...");
                if (!db.ConnectToDatabase())
                {
                    AnsiConsole.MarkupLine("[red]Failed to connect to database![/]");
                }
                else
                {
                    AnsiConsole.MarkupLine("[green]Successfully connected to database![/]");
                    connectionSuccessful = true;
                }
            });
        
        if (!connectionSuccessful)
        {
            if (!AnsiConsole.Confirm("We had problems connection to the database. Do you still want to continue?"))
            {
                new Menu().Main();
                return;
            }
        }

        userAccepted = false;
        while (!userAccepted)
        {
            var table = AnsiConsole.Ask<string>("What is the name of the table you want to use?");
            var columns = AnsiConsole.Ask<string>("What are the names of your columns in the Table? Comma Seperated! (Used for INSERT INTO command!)");
            var values = AnsiConsole.Ask<string>("What are the values of your columns in the Table? Comma Seperated! Add %Key% as placeholder where you want to insert the key! (Used for INSERT INTO command!)");
            db.InsertKeyQuery = $"INSERT INTO {table} ({columns}) VALUES ({values})";
            Console.WriteLine("Your INSERT INTO command looks like this: ");
            Console.WriteLine(db.InsertKeyQuery);
            userAccepted = AnsiConsole.Confirm("Is this correct? (You can change it later anyway)");
        }
        
        Summarize(db);
    }

    private void Summarize(IDatabase<DatabaseType> db)
    {
        Console.WriteLine("The setup is now complete. Here is a summary of your settings:");
        Console.WriteLine($"Database Type: {db.DatabaseType}");
        Console.WriteLine($"Database Connection String: {db.ConnectionString}");
        Console.WriteLine($"Database Insert Key Query: {db.InsertKeyQuery}");
        if (AnsiConsole.Confirm("Do you want to save these settings?"))
        {
            SystemHandler.DatabaseManager.Databases.Add(db);
            SystemHandler.SaveDatabaseManager();
            Console.WriteLine("Database saved!");
            new Menu().Main();
        }
        else
        {
            new Menu().Main();
        }
    }
}