using KeyGenerator.Classes;
using KeyGenerator.Classes.KeyModules;
using KeyGenerator.Codes;
using Spectre.Console;

namespace KeyGenerator.Main;

public class Menu
{
    public void Main()
    {
        var table = new [] { "1. Create a new Key template", "2. Load a Key", "3. Remove a Key", "4. Show all Keys", "5. Exit" };
        
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
                new Setup().Main();
                break;
            case 1:
                LoadKey();
                break;
            case 2:
                RemoveKey();
                break;
            case 3:
                ShowAllKeys();
                break;
            case 4:
                Console.WriteLine("Goodbye!");
                SystemHandler.SaveKeys();
                Environment.Exit(0);
                break;
        }
    }

    private static void LoadKey()
    {
        var item = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Which key would you like to remove?")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                .AddChoices(SystemHandler.Keys.Select(x => x.Name).ToArray()));
        
        new KeyGenerator().GenerateKeys(SystemHandler.Keys.First(x => x.Name == item));
    }

    private void ShowAllKeys()
    {
        foreach (var key in SystemHandler.Keys)
        {
            var firstModules = key.KeyModules.Take(3).Select(x => x.KeyPatternType).ToArray();
            var firstModulesString = string.Join(", ", firstModules);
            
            Console.WriteLine($"Name: {key.Name}\nPattern Length: {key.PatternLength}\nPattern Size: {key.PatternSize}\nSeparator: {key.Separator}\nFirst 3 Modules: {firstModulesString}");
            Console.WriteLine("--------------------");
        }

        Console.WriteLine("Press any key to continue.");
        Console.ReadKey();
        Main();
    }

    private void RemoveKey()
    {
        var item = AnsiConsole.Prompt(
            new SelectionPrompt<Key>()
                .Title("Which key would you like to remove?")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                .AddChoices(SystemHandler.Keys));

        if (AnsiConsole.Confirm("Are you sure you want to remove this key?"))
        {
            SystemHandler.Keys.Remove(item);
            SystemHandler.SaveKeys();
            Console.WriteLine("Key removed!");
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
            Main();
        }
        else
        {
            Console.WriteLine("Aborted!");
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
            Main();
        }
    }
}