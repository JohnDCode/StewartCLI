/*

Stewart CLI
/Commands/Restore.cs - Restore command, can restore past system configurations from stewart snap
JohnDavid Abe 

*/

using System;
using System.Diagnostics;

public class RestoreCommand : ICommand
{

    // Execute method (ran upon command)
    public void Execute()
    {
        // Debug console out
        Console.Write("Running Restore Command");

        RunSingleCommand().Wait();
    }

    private async Task RunSingleCommand()
    {
        string command = "Get-Date";
        string result = await PowerShellRunner.RunAsync(command);
        Console.WriteLine($"Output:\n{result}");
    }
}