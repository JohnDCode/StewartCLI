/*

Stewart CLI
/Commands/Log.cs - Log command, handles the configuration of all stewart logs
JohnDavid Abe 

*/

using System;
using System.Diagnostics;

public class LogCommand : ICommand
{

    // Execute method (ran upon command)
    public void Execute()
    {
        // Debug console out
        Console.Write("Running Log Command");

        RunSingleCommand().Wait();
    }

    private async Task RunSingleCommand()
    {
        string command = "Get-Date";
        string result = await PowerShellRunner.RunAsync(command);
        Console.WriteLine($"Output:\n{result}");
    }
}