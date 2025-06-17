/*

Stewart CLI
/Powershell/PowershellRunner.cs - Handles async calls to powershell commands and respective outputs
JohnDavid Abe 

*/

using System;
using System.Management.Automation;
using System.Threading;
using System.Threading.Tasks;

public static class PowerShellRunner
{
    // Limit to 5 concurrent PowerShell executions to avoid overloading system resources
    private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(5);

    public static async Task<string> RunAsync(string command)
    {
        await _semaphore.WaitAsync();
        try
        {
            using var ps = PowerShell.Create();
            ps.AddScript(command);

            var results = await Task.Factory.FromAsync(ps.BeginInvoke(), ps.EndInvoke);

            // Combine all results into a single string output
            string output = "";
            foreach (var result in results)
            {
                output += result?.ToString() + Environment.NewLine;
            }

            return output.Trim();
        }
        catch (Exception ex)
        {
            return $"PowerShell Error: {ex.Message}";
        }
        finally
        {
            _semaphore.Release();
        }
    }
}