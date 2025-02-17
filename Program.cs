// Program.cs

using System;
using System.Threading.Tasks;
using AppConfig;
using WinSecurityLogs;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Program·Main()· Init");

        // Check if configuration is passed through command-line arguments
        Config config = CommandLineArgsParser.ParseCommandLineArgs(args);

        // If config is still null, prompt the user for input
        if (config == null)
        {
            config = ConfigManager.LoadConfig() ?? ConfigManager.PromptUserForConfig();
        }

        // Create an instance of LogSender and send application logs
        LogSender logSender = new LogSender();
        await logSender.SendApplicationLogs(config);

        // Finish
        Console.WriteLine("Program·Main()· Finished");
    }
}
