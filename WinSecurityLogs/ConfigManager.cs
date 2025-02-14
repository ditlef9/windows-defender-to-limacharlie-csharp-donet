// WinSecurityLogs/WinEventLogger

using System;
using System.IO;
using Newtonsoft.Json;

// Handles User Input & Config Storage

public class ConfigManager
{
    // Path to store the config file
    private static string configPath = "config.json";

    /// <summary>
    /// Loads configuration from config.json if it exists.
    /// </summary>
    public static Config LoadConfig()
    {
        if (File.Exists(configPath))
        {
            string json = File.ReadAllText(configPath);
            return JsonConvert.DeserializeObject<Config>(json);
        }

        return null;
    }

    /// <summary>
    /// Saves configuration to config.json.
    /// </summary>
    public static void SaveConfig(Config config)
    {
        string json = JsonConvert.SerializeObject(config, Formatting.Indented);
        File.WriteAllText(configPath, json);
    }

    /// <summary>
    /// Prompts user for LimaCharlie details on first run.
    /// </summary>
    public static Config PromptUserForConfig()
    {
        Console.Write("Enter LimaCharlie Organization ID (example a100a186-8a11-445a-a0cf-643a40c7fb61): ");
        string orgId = Console.ReadLine().Trim();

        Console.Write("Enter LimaCharlie Hook URL (b76093c3662d5b4f.hook.limacharlie.io): ");
        string hookURL = Console.ReadLine().Trim();

        Console.Write("Enter LimaCharlie Webhook Name (example acd-windows-security-webhook): ");
        string webhookName = Console.ReadLine().Trim();

        Console.Write("Enter LimaCharlie Webhook Secret: ");
        string webhookSecret = Console.ReadLine().Trim();

        Config config = new Config
        {
            OrgId = orgId,
            HookURL = hookURL,
            WebhookName = webhookName,
            WebhookSecret = webhookSecret
        };

        SaveConfig(config);
        return config;
    }
}

