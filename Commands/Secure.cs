/*

Stewart CLI
/Commands/Secure.cs - Secure command, attempts to secure any vulns identified in most recent check command
JohnDavid Abe 

*/

using System;
using System.Diagnostics;

public class SecureCommand : ICommand
{

    // Execute method (ran upon command)
    public void Execute()
    {
        // Debug console out
        Console.Write("Running Secure Command");

        RunSingleCommand().Wait();
    }

    private async Task RunSingleCommand()
    {
        string command = "Get-Date";
        string result = await PowerShellRunner.RunAsync(command);
        Console.WriteLine($"Output:\n{result}");
    }
}