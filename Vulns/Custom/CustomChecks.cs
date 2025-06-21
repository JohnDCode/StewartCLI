using System.Management;

public static class CustomChecks
{
    public static bool RunCustomCheck(string checkName)
    {
        switch (checkName)
        {
            case "CheckFirewallStatus":
                return CheckFirewallStatus();
            default:
                throw new Exception("Unknown Custom Check");
        }
    }

    public static bool CheckFirewallStatus()
    {
        var task = PSRunner.RunAsync("(Get-NetFirewallProfile -Profile Domain,Public,Private).Enabled");
        task.Wait();
        string output = task.Result.Trim();
        return output.Contains("True"); // Firewall enabled if "True" is found
    }
}