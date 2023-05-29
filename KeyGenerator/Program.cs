using System.Reflection;
using KeyGenerator.Codes;
using KeyGenerator.Main;
using NLog;

namespace KeyGenerator;

internal class Program
{
    private static readonly Logger Log = LogManager.GetCurrentClassLogger();

    private static void Main(string[] args)
    {
        new Program().Startup(args);
    }

    private void Startup(string[] args)
    {
        Log.Info("Starting KeyGenerator...");
        Console.WriteLine($"""
  _____ _____ _____ _____ _____ _____ _____ _____ _____ _____ _____ _____ _____ _____ _____ _____ _____ 
 |_____|_____|_____|_____|_____|_____|_____|_____|_____|_____|_____|_____|_____|_____|_____|_____|_____|
                  _  __           ____                           _             
                 | |/ /___ _   _ / ___| ___ _ __   ___ _ __ __ _| |_ ___  _ __ 
                 | ' // _ \ | | | |  _ / _ \ '_ \ / _ \ '__/ _` | __/ _ \| '__|
                 | . \  __/ |_| | |_| |  __/ | | |  __/ | | (_| | || (_) | |   
                 |_|\_\___|\__, |\____|\___|_| |_|\___|_|  \__,_|\__\___/|_|   
                           |___/
  _____ _____ _____ _____ _____ _____ _____ _____ _____ _____ _____ _____ _____ _____ _____ _____ _____ 
 |_____|_____|_____|_____|_____|_____|_____|_____|_____|_____|_____|_____|_____|_____|_____|_____|_____|
 Version: {Assembly.GetExecutingAssembly().GetName().Version}
 Created by: Jason
 Released by: JDS - JasonDevelopmentStudios
 GitHub: https://github.com/Json-exe/KeyGenerator
""");
        Console.WriteLine("STARTING UP...");
        if (!Directory.Exists(SystemHandler.DataPath))
        {
            Directory.CreateDirectory(SystemHandler.DataPath);
            new Setup().Main();
        }
        else
        {
            SystemHandler.LoadData();
            if (SystemHandler.Keys.Any())
                new Menu().Main();
            else
                new Setup().Main();
        }
    }
}