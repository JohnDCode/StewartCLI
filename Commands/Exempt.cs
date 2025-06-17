/*

Stewart CLI
/Commands/Exempt.cs - Exempt command, handles exempt vulns (such that future commands will ignore presence of such an exemption)
JohnDavid Abe 

*/

using System;
using System.Diagnostics;

public class ExemptCommand : ICommand
{

    // Execute method (ran upon command)
    public void Execute()
    {
        // Debug console out
        Console.Write("Running Exempt Command");

        RunSingleCommand().Wait();
    }

    private async Task RunSingleCommand()
    {
        string command = "Get-Date";
        string result = await PowerShellRunner.RunAsync(command);
        Console.WriteLine($"Output:\n{result}");
    }
}