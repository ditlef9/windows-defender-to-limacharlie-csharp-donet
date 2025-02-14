using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

class Program
{
    static async Task Main(string[] args)
    {
        // Load configuration or prompt user for details
        Config config = ConfigManager.LoadConfig() ?? ConfigManager.PromptUserForConfig();

        // Send Application Event Logs to LimaCharlie
        await SendApplicationLogs(config);
    }

    /// <summary>
    /// Fetches the last 10 Application Event logs and sends them to LimaCharlie.
    /// </summary>
    static async Task SendApplicationLogs(Config config)
    {
        try
        {
            // Run PowerShell command to get last 10 Security logs
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "powershell",
                Arguments = "-Command \"Get-WinEvent -LogName Application | Where-Object {$_.ProviderName -like 'Microsoft-Windows-Security-Auditing'} -MaxEvents 10 | ConvertTo-Json -Depth 3\"",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };


            using (Process process = new Process { StartInfo = psi })
            {
                process.Start();
                string jsonData = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                // Try to deserialize the output to check if it's JSON
                try
                {
                    // Try to deserialize the output as JSON
                    var jsonObject = JsonConvert.DeserializeObject(jsonData);

                    // If deserialization succeeds, it's JSON
                    Console.WriteLine("Program·SendApplicationLogs()· Logs successfully sent to LimaCharlie.");
                }
                catch (JsonException)
                {
                    // If deserialization fails, it's not JSON (likely plain text)
                    Console.WriteLine("Program·SendApplicationLogs()· ERROR: The output is not valid JSON. Here is the plain text output:");
                    Console.WriteLine(jsonData);
                }

                // Continue with sending the logs (if it's valid JSON)
                using (HttpClient client = new HttpClient())
                {
                    // Add the authentication header
                    client.DefaultRequestHeaders.Add("lc-secret", config.WebhookSecret);

                    // Set the HTTP content type as JSON
                    HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    // Ensure the HookURL doesn't have a duplicate domain part
                    string hookURL = config.HookURL.Contains(".hook.limacharlie.io") 
                        ? config.HookURL 
                        : $"{config.HookURL}.hook.limacharlie.io";

                    // Construct the final webhook URL
                    string webhookUrl = $"https://{hookURL}/{config.OrgId}/{config.WebhookName}";

                    // Send logs as POST request
                    HttpResponseMessage response = await client.PostAsync(webhookUrl, content);

                    // Print the response status
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"Program·SendApplicationLogs()·Logs successfully sent to LimaCharlie.");
                    }
                    else
                    {
                        Console.WriteLine($"Program·SendApplicationLogs()·ERROR: {response.StatusCode} - {response.ReasonPhrase}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Program·SendApplicationLogs()·ERROR: {ex.Message}");
        }
    }

    
}
