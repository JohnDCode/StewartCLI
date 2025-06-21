/*

Stewart CLI
/Commands/Check.cs - Check command, checks for vulns on machine and/or services
JohnDavid Abe 

*/

using System;
using System.Diagnostics;

public class CheckCommand : ICommand
{

    // Execute method (ran upon command)
    public void Execute()
    {
        // Debug console out
        Console.Write("Running Check Command");

        // Load JSON data
        var vulnerabilities = VulnerabilityRepository.Load(Path.Combine(AppContext.BaseDirectory, "Vulns", "OS", "windows.json"));

        Console.WriteLine(vulnerabilities[0]);

    }
}