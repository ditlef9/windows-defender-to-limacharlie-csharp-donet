// WinSecurityLogs/LogStorage.cs

namespace WinSecurityLogs;
using Newtonsoft.Json;

/// <summary>
/// Class for handling sent logs data.
/// </summary>
public class LogStorage
{
    private readonly string filePath = "sentLogs.json";

    /// <summary>
    /// Checks if the data has already been sent by comparing it with stored logs.
    /// </summary>
    public bool HasDataBeenSent(string jsonData)
    {
        if (File.Exists(filePath))
        {
            string existingData = File.ReadAllText(filePath);
            var sentLogs = JsonConvert.DeserializeObject<string[]>(existingData);
            foreach (var log in sentLogs)
            {
                if (log == jsonData)
                {
                    return true; // Data already sent
                }
            }
        }
        return false;
    }

    /// <summary>
    /// Stores sent data in a local JSON file to avoid resending.
    /// </summary>
    public void StoreSentData(string jsonData)
    {
        // Check if the file exists
        string[] existingLogs = File.Exists(filePath)
            ? JsonConvert.DeserializeObject<string[]>(File.ReadAllText(filePath))
            : new string[0];

        // Append the new log data
        var updatedLogs = new string[existingLogs.Length + 1];
        existingLogs.CopyTo(updatedLogs, 0);
        updatedLogs[existingLogs.Length] = jsonData;

        // Write back to the file
        File.WriteAllText(filePath, JsonConvert.SerializeObject(updatedLogs, Formatting.Indented));
    }
}