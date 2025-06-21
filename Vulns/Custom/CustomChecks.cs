/*

Stewart CLI
/Vulns/Custom/CustomChecks.cs - Used to handle any specific custom checks that can not be run via a powershell cmd or accessing the registry
JohnDavid Abe 

*/

public static class CustomChecks
{
    public static bool RunCustomCheck(string checkName)
    {   
        // Check the name of the custom check and run the appropriate method (name is stored in json, customcheckname property of the vuln)
        switch (checkName)
        {
            case "CheckFirewallStatus":
                return CheckFirewallStatus();
            default:
                throw new Exception("Unknown Custom Check");
        }
    }

    // Checks the state of the firewall (OS-002)
    public static bool CheckFirewallStatus()
    {
        var task = PSRunner.RunAsync("(Get-NetFirewallProfile -Profile Domain,Public,Private).Enabled");
        task.Wait();
        string output = task.Result.Trim();
        return output.Contains("True"); // Firewall enabled if "True" is found
    }
}