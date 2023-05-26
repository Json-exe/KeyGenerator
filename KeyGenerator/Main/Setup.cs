using KeyGenerator.Classes;
using KeyGenerator.Classes.KeyModules;
using Spectre.Console;

namespace KeyGenerator.Main;

public class Setup
{
    public void Main()
    {
        Console.WriteLine(
            "Hello and welcome to KeyGenerator!\nThis is a Setup Wizard, which will guide you to setup your first Key-Pattern!\nIf you want to begin press any key.");
        Console.ReadKey();
        Console.WriteLine(
            "This program will now ask you some questions to setup your first Key-Pattern.\nPress any key to continue.");
        Console.ReadKey();
        var name = AnsiConsole.Ask<string>(
            "What is the name of your Key-Template? This will be used to identify your Key-Pattern for later use.");
        var patternLength =
            AnsiConsole.Ask(
                "How long should the Key-Pattern be? (A part usually contains 5 Numbers/Letters, this is only used in random generators!)",
                5);
        var patternSize =
            AnsiConsole.Ask(
                "How many Numbers/Letters should be in one key part? (A part usually contains 5 Numbers/Letters, this is only used in random generators!)",
                5);
        var separator =
            AnsiConsole.Ask(
                "What should be the separator between the Key-Parts? This will be used to separate your key parts.",
                '-');
        var keys = AnsiConsole.Ask<int>("How many Keys should be generated?");
        var key = new Key
        {
            Name = name,
            PatternLength = patternLength,
            PatternSize = patternSize,
            Keys = keys,
            Separator = separator
        };
        KeyPartsSetup(key);
    }

    private void KeyPartsSetup(Key key)
    {
        Console.WriteLine("Now we create your Key-Pattern.\nPress any key to continue.");
        Console.ReadKey();
        for (var i = 0; i < key.PatternLength; i++)
        {
            Console.WriteLine($"Creating Key-Part {i + 1} of {key.PatternLength}.");
            var selection = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("What should be the content of this Key-Part?")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                    .AddChoices("Static Text", "Random Numbers", "Random Letters",
                        "Random Numbers and Letters (Mixed)"));

            switch (selection)
            {
                case "Static Text":
                    key.KeyModules.Add(new StaticText(AnsiConsole.Ask<string>("What should be the static text?")));
                    break;
                case "Random Numbers":
                    key.KeyModules.Add(new RandomNumbers());
                    break;
                case "Random Letters":
                    var randomLetterSpecific = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("How exactly do you want the random letters to be?")
                            .PageSize(10)
                            .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                            .AddChoices("Uppercase", "Lowercase", "Mixed"));
                    switch (randomLetterSpecific)
                    {
                        case "Uppercase":
                            key.KeyModules.Add(new RandomLetters(LetterType.Uppercase));
                            break;
                        case "Lowercase":
                            key.KeyModules.Add(new RandomLetters(LetterType.Lowercase));
                            break;
                        case "Mixed":
                            key.KeyModules.Add(new RandomLetters(LetterType.Mixed));
                            break;
                        default:
                            throw new ArgumentOutOfRangeException("Invalid selection!", "The selection was not valid.");
                    }

                    break;
                case "Random Numbers and Letters (Mixed)":
                    key.KeyModules.Add(new Mixed());
                    break;
            }
        }

        Console.WriteLine("Alright, we are done with the Key-Part setup.\nPress any key to continue.");
        Console.ReadKey();
        new KeyGenerator().GenerateKeys(key);
    }
}