/*

Stewart CLI
/Commands/Check.cs - Check command, checks for vulns on machine and/or services
JohnDavid Abe 

*/

using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


public class CheckCommand : ICommand
{
    // SemaphoreSlim to limit the number of concurrent checks to 5
    private static readonly SemaphoreSlim semaphore = new SemaphoreSlim(5);

    // Synchronous entry point of check command
    public void Execute()
    {
        // Load the OS JSON file containing vulnerabilities
        var vulnerabilities = VulnerabilityRepository.Load(Path.Combine(AppContext.BaseDirectory, "Vulns", "OS", "windows.json"));

        // Run the async method in a blocking way, getting results of each check
        var results = CheckAllVulnerabilitiesAsync(vulnerabilities).GetAwaiter().GetResult();

        // Output the results to the console of each check
        foreach (var result in results)
        {
            Console.WriteLine($"ID: {result.vuln.Id} | Description: {result.vuln.Description} | Vulnerable: {result.isVulnerable}");
        }
    }

    // Asynchronous method to check all vulnerabilities concurrently
    private async Task<List<(Vulnerability vuln, bool isVulnerable)>> CheckAllVulnerabilitiesAsync(List<Vulnerability> vulnerabilities)
    {
        var tasks = new List<Task<(Vulnerability, bool)>>();

        foreach (var vuln in vulnerabilities)
        {
            tasks.Add(CheckVulnerabilityWithSemaphoreAsync(vuln));
        }

        var results = await Task.WhenAll(tasks);
        return results.ToList();
    }

    private async Task<(Vulnerability vuln, bool isVulnerable)> CheckVulnerabilityWithSemaphoreAsync(Vulnerability vuln)
    {
        await semaphore.WaitAsync();
        try
        {
            bool result = await CheckVulnerabilityAsync(vuln);
            return (vuln, result);
        }
        finally
        {
            semaphore.Release();
        }
    }

    private async Task<bool> CheckVulnerabilityAsync(Vulnerability vuln)
    {
        switch (vuln.CheckType)
        {
            case "PowerShell":
                string output = await PSRunner.RunAsync(vuln.CheckCommand);
                return output.Trim() == vuln.ExpectedValue;

            case "Registry":
                return RegRunner.CheckRegistryBit(
                    vuln.RegistryPath,
                    vuln.RegistryValueName,
                    vuln.RegistryExpectedBit
                );

            case "Custom":
                return CustomChecks.RunCustomCheck(vuln.CustomCheckName);

            default:
                throw new Exception($"Unknown CheckType: {vuln.CheckType}");
        }
    }
}