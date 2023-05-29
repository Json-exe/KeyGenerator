using KeyGenerator.Codes;
using Spectre.Console;

namespace KeyGenerator.Main.DatabaseManager;

public class DatabaseMenu
{
    public void Main()
    {
        var table = new[]
            { "1. Create a new Database connection", "2. Show a Database", "3. Remove a Database", "4. Show all Databases", "5. Back" };

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
                new DatabaseSetup().Main();
                break;
            case 1:
                LoadDatabase();
                break;
            case 2:
                RemoveDatabase();
                break;
            case 3:
                ShowAllDatabases();
                break;
            case 4:
                new Menu().Main();
                break;
        }
    }

    private void ShowAllDatabases()
    {
        foreach (var database in SystemHandler.DatabaseManager.Databases)
        {
            Console.WriteLine($"Name: {database.Name} | Type: {database.DatabaseType} | ConnectionString: {database.ConnectionString}");
            Console.WriteLine("--------------------------------------------------------------");
        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Main();
    }

    private void RemoveDatabase()
    {
        var item = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Which Database would you like to remove?")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                .AddChoices(SystemHandler.DatabaseManager.Databases.Select(x => x.Name).ToArray()));

        if (AnsiConsole.Confirm("Do you really want to remove this Database?"))
        {
            SystemHandler.DatabaseManager.Databases.Remove(SystemHandler.DatabaseManager.Databases.FirstOrDefault(x => x.Name == item) ?? throw new InvalidOperationException());
            Console.WriteLine("Database removed!");
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Main();
    }

    private void LoadDatabase()
    {
        var item = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Which Database would you like to load?")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                .AddChoices(SystemHandler.DatabaseManager.Databases.Select(x => x.Name).ToArray()));
        
        var database = SystemHandler.DatabaseManager.Databases.FirstOrDefault(x => x.Name == item);
        if (database != null)
        {
            new DatabaseEditor().Main(database);
        }
        else
        {
            Console.WriteLine("Database not found!");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Main();
        }
    }
}