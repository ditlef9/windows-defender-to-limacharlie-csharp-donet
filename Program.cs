// Program.cs
using System;
using System.Threading.Tasks;
using AppConfig;
using WinSecurityLogs;

class Program
{
    static async Task Main(string[] args)
    {
        Config config = CommandLineArgsParser.ParseCommandLineArgs(args);

        if (config == null)
        {
            // Load config from file or use default values
            config = ConfigManager.LoadConfig();
            if (config == null)
            {
                // Fallback to default configuration or exit silently
                Console.WriteLine("ERROR: Missing configuration. Exiting...");
                return; // Silent exit if configuration is missing
            }
        }

        // Create an instance of LogSender and send application logs
        LogSender logSender = new LogSender();
        await logSender.SendApplicationLogs(config);

        // Exit gracefully without any additional output
        Environment.Exit(0);
    }
}
