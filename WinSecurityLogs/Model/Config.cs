// WinSecurityLogs/Model/Config.cs


/// <summary>
/// Stores LimaCharlie Configuration Details.
/// </summary>
public class Config
{
    public string OrgId { get; set; }
    public string HookURL {get; set;}
    public string WebhookName { get; set; }
    public string WebhookSecret { get; set; }
}