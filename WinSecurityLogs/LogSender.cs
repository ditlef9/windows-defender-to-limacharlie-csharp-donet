// WinSecurityLogs/LogSender.cs

// LogSender.cs

namespace WinSecurityLogs;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using AppConfig;
public class LogSender
{
    public async Task SendApplicationLogs(Config config)
    {
        try
        {
            // Run PowerShell command to get last 10 Security logs
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "powershell",
                Arguments = "-Command \"Get-WinEvent -LogName 'Microsoft-Windows-Windows Defender/Operational' -MaxEvents 10 | ConvertTo-Json -Depth 3\"",
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
                    var jsonObject = JsonConvert.DeserializeObject(jsonData);

                    LogStorage logStorage = new LogStorage();

                    if (logStorage.HasDataBeenSent(jsonData))
                    {
                        return; // Don't send again
                    }

                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Add("lc-secret", config.WebhookSecret);

                        HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                        string hookURL = config.HookURL.Contains(".hook.limacharlie.io")
                            ? config.HookURL
                            : $"{config.HookURL}.hook.limacharlie.io";

                        string webhookUrl = $"https://{hookURL}/{config.OrgId}/{config.WebhookName}";

                        HttpResponseMessage response = await client.PostAsync(webhookUrl, content);

                        if (response.IsSuccessStatusCode)
                        {
                            Console.WriteLine("Logs successfully sent to LimaCharlie.");
                            logStorage.StoreSentData(jsonData);
                        }
                        else
                        {
                            Console.WriteLine($"ERROR: {response.StatusCode} - {response.ReasonPhrase}");
                        }
                    }
                }
                catch (JsonException)
                {
                    Console.WriteLine("ERROR: The output is not valid JSON. Here is the plain text output:");
                    Console.WriteLine(jsonData);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR: {ex.Message}");
        }
    }
}
