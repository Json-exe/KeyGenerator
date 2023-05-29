using KeyGenerator.Classes.Database;
using Spectre.Console;

namespace KeyGenerator.Main.DatabaseManager;

public class DatabaseEditor
{
    public void Main(IDatabase<DatabaseType> db)
    {
        var table = new[]
            { "1. Connection String", "2. Insert String", "3. Back" };

        var item = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("What would you like to do?")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                .AddChoices(table));
        
        var selected = Array.IndexOf(table, item);
        switch (selected)
        {
            case 0:
                Console.WriteLine("Current Connection String:");
                Console.WriteLine(db.ConnectionString);
                Console.WriteLine("Enter the new Connection String:");
                db.ConnectionString = Console.ReadLine() ?? string.Empty;
                if (!CheckConnection(db))
                {
                    Console.WriteLine("The new Connection String is invalid. Please check the Connection String!");
                    Main(db);
                }
                break;
            case 1:
                Console.WriteLine("Current Insert String:");
                Console.WriteLine(db.InsertKeyQuery);
                Console.WriteLine("Enter the new Insert String:");
                db.InsertKeyQuery = Console.ReadLine() ?? string.Empty;
                break;
            case 2:
                new DatabaseMenu().Main();
                break;
        }
    }

    private bool CheckConnection(IDatabase<DatabaseType> db)
    {
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
        return connectionSuccessful;
    }
}