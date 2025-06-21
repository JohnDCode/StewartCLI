/*

Stewart CLI
/Commands/Vuln.cs - Vuln command, acts as vuln lookup to display information on any particular vuln in database
JohnDavid Abe 



using System;
using System.Diagnostics;

public class VulnCommand : ICommand
{

    // Execute method (ran upon command)
    public void Execute()
    {
        // Debug console out
        Console.Write("Running Vuln Command");

        RunSingleCommand().Wait();
    }

    private async Task RunSingleCommand()
    {
        string command = "Get-Date";
        string result = await PowerShellRunner.RunAsync(command);
        Console.WriteLine($"Output:\n{result}");
    }
}


*/