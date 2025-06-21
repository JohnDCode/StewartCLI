/*

Stewart CLI
/Commands/Check.cs - Check command, checks for vulns on machine and/or services
JohnDavid Abe 

*/

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

        // Output the results to the console of each check (ID, Description, and State)
        foreach (var result in results)
        {
            Console.WriteLine($"ID: {result.vuln.Id} | Description: {result.vuln.Description} | Vulnerable: {result.isVulnerable}");
        }
    }

    // Asynchronous method to check all vulnerabilities concurrently
    private async Task<List<(Vulnerability vuln, bool isVulnerable)>> CheckAllVulnerabilitiesAsync(List<Vulnerability> vulnerabilities)
    {
        // Assign each vuln a task
        var tasks = new List<Task<(Vulnerability, bool)>>();

        // Add each vuln to the task list
        foreach (var vuln in vulnerabilities)
        {
            tasks.Add(CheckVulnerabilityWithSemaphoreAsync(vuln));
        }

        // Get the reuslts of each task/vuln check, wait until all vulns have been checked
        var results = await Task.WhenAll(tasks);

        // Return the results as a list of tuples containing info on each vuln state
        return results.ToList();
    }

    // Method to check a single vulnerability
    private async Task<(Vulnerability vuln, bool isVulnerable)> CheckVulnerabilityWithSemaphoreAsync(Vulnerability vuln)
    {
        // Wait until the semaphore has a slot available (of the 5 slots)
        await semaphore.WaitAsync();
        try
        {
            // Check the vuln and get the state asynchronously
            bool result = await CheckVulnerabilityAsync(vuln);
            return (vuln, result);
        }
        finally
        {
            // Release the slot so new task with new vuln can run
            semaphore.Release();
        }
    }

    // Method to check a single vulnerability based on its type
    private async Task<bool> CheckVulnerabilityAsync(Vulnerability vuln)
    {
        // Check the type of each vuln (powershell, registry, custom logic)
        switch (vuln.CheckType)
        {
            // Powershell case, use the powershell runner to execute the command and compare output to expected value
            case "PowerShell":
                string output = await PSRunner.RunAsync(vuln.CheckCommand);
                return output.Trim() == vuln.ExpectedValue;

            // Registry case, access the registry and check based on the path, value, and expected bit
            case "Registry":
                return RegRunner.CheckRegistryBit(
                    vuln.RegistryPath,
                    vuln.RegistryValueName,
                    vuln.RegistryExpectedBit
                );

            // Custom case, all custom logic is handled in CustomChecks class based on the name of the check (vuln.CustomCheckName)
            case "Custom":
                return CustomChecks.RunCustomCheck(vuln.CustomCheckName);

            // If unknown check type, throw an exception, as unknown vuln has been added to JSON database
            default:
                throw new Exception($"Unknown CheckType: {vuln.CheckType}");
        }
    }
}