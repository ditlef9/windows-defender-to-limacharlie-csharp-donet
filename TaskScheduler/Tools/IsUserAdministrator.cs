using System;
using System.Security.Principal;

namespace Tools;

/// <summary>
/// Utility class to check if the current user has administrator privileges.
/// </summary>
public static class IsUserAdministrator
{
    /// <summary>
    /// Checks if the current user is a member of the Administrators group.
    /// </summary>
    /// <returns>True if the current user is an administrator, false otherwise.</returns>
    public static bool Check()
    {
        try
        {
            // Get the current user's identity and principal
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);

            // Check if the user is in the Administrators role
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR: Could not determine if user is an administrator. {ex.Message}");
            return false;
        }
    }
}

