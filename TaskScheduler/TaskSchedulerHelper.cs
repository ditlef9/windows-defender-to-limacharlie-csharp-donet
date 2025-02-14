// WinEventLogger/TaskSchedulerHelper.cs
using System;
using System.Diagnostics;

// Checks if the scheduled task exists; creates it if not
public class TaskSchedulerHelper
{
    // Name of the scheduled task
    private static string taskName = "WinEventLoggerTask";

    /// <summary>
    /// Checks if the scheduled task already exists.
    /// </summary>
    public static bool IsTaskExists()
    {
        Process process = new Process();
        process.StartInfo.FileName = "schtasks";
        process.StartInfo.Arguments = $"/Query /TN \"{taskName}\"";
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;

        process.Start();
        string output = process.StandardOutput.ReadToEnd();
        process.WaitForExit();

        return output.Contains(taskName);
    }

    /// <summary>
    /// Creates a new scheduled task to run the program every 10 minutes.
    /// </summary>
    public static void CreateScheduledTask()
    {
        string exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
        string arguments = $"/Create /TN \"{taskName}\" /TR \"{exePath}\" /SC MINUTE /MO 10 /F /RL HIGHEST";

        Process process = new Process();
        process.StartInfo.FileName = "schtasks";
        process.StartInfo.Arguments = arguments;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;

        process.Start();
        process.WaitForExit();
        
        Console.WriteLine("✅ Scheduled Task Created.");
    }
}
