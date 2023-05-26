using KeyGenerator.Classes;
using KeyGenerator.Codes;
using Spectre.Console;

namespace KeyGenerator.Main;

public class KeyGenerator
{
    public void GenerateKeys(Key key)
    {
        if (AnsiConsole.Confirm(
                "Do you want to change, how many keys you want to generate? Currently you want to generate " +
                key.Keys + " keys.")) key.Keys = AnsiConsole.Ask<int>("How many keys do you want to generate?");

        Console.WriteLine("Press any key to start generating.");
        Console.ReadKey();
        var keyList = new List<string>();
        for (var i = 0; i < key.Keys; i++)
        {
            var keyPart = string.Empty;

            foreach (var keyModule in key.KeyModules)
            {
                keyPart += keyModule.GenerateKeyPart(key);
                if (!keyPart.EndsWith(key.Separator) && key.KeyModules.Last() != keyModule) keyPart += key.Separator;
            }

            Console.WriteLine($"Key {i + 1} of {key.Keys}: {keyPart}");
            keyList.Add(keyPart);
        }

        if (AnsiConsole.Confirm("Do you want to save your keys to a file?"))
        {
            var savePath = "";
            while (!Directory.Exists(savePath))
                savePath = AnsiConsole.Ask<string>(
                    "Where do you want to save your keys? (Please enter a valid directory path!)");
            SaveKeys(keyList, savePath);
        }

        if (!SystemHandler.Keys.Any(x => x.Name == key.Name) &&
            AnsiConsole.Confirm(
                "Do you want to save your Key Template for later use? You can easily load it at anytime!"))
        {
            SystemHandler.Keys.Add(key);
            Console.WriteLine("Your Key Template has been saved!");
        }

        SystemHandler.SaveKeys();
        Console.WriteLine("Thank you for using KeyGenerator!\nPress any key to proceed.");
        Console.ReadKey();
        new Menu().Main();
    }

    private static void SaveKeys(IEnumerable<string> keyList, string savePath)
    {
        var fileName = AnsiConsole.Ask<string>("What should be the name of the file?");
        var filePath = Path.Combine(savePath, fileName);
        // If the suffix is not .txt, add it.
        if (!filePath.EndsWith(".txt")) filePath += ".txt";
        File.WriteAllLines(filePath, keyList);
        Console.WriteLine("Your keys have been saved!");
    }
}