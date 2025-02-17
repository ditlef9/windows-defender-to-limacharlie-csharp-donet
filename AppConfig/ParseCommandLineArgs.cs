// Config/ParseCommandLineArgs.cs

namespace AppConfig
{
    public static class CommandLineArgsParser
    {
        /// <summary>
        /// Parses the command-line arguments to extract the configuration values.
        /// </summary>
        public static Config ParseCommandLineArgs(string[] args)
        {
            string orgId = null;
            string hookURL = null;
            string webhookName = null;
            string webhookSecret = null;

            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i].ToLower())
                {
                    case "--orgid":
                        orgId = args.Length > i + 1 ? args[i + 1] : null;
                        break;
                    case "--hookurl":
                        hookURL = args.Length > i + 1 ? args[i + 1] : null;
                        break;
                    case "--webhookname":
                        webhookName = args.Length > i + 1 ? args[i + 1] : null;
                        break;
                    case "--webhooksecret":
                        webhookSecret = args.Length > i + 1 ? args[i + 1] : null;
                        break;
                }
            }

            if (orgId != null && hookURL != null && webhookName != null && webhookSecret != null)
            {
                return new Config
                {
                    OrgId = orgId,
                    HookURL = hookURL,
                    WebhookName = webhookName,
                    WebhookSecret = webhookSecret
                };
            }

            // Return null if the configuration is incomplete
            return null;
        }
    }
}
