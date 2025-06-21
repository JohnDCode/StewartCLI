/*

Stewart CLI
/System/PSRunner.cs - Used to run powershell commands asynchronously and return the result of the command
JohnDavid Abe 

*/

using System.Management.Automation;

public static class PSRunner
{
    public static async Task<string> RunAsync(string command)
    {
        // Create an empty Powershell instance
        using PowerShell ps = PowerShell.Create();

        // Add the command to the new PS Instance
        ps.AddScript(command);

        // Invoke the instance asynchronously and format the results of the command
        var results = await Task.Factory.FromAsync(ps.BeginInvoke(), ps.EndInvoke);

        // Return the results as a formatted string
        return string.Join("\n", results.Select(r => r.ToString()));
    }
}