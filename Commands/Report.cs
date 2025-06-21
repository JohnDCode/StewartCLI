/*

Stewart CLI
/Commands/Report.cs - Report command, generates report of most recent action
JohnDavid Abe 



using System;
using System.Diagnostics;

public class ReportCommand : ICommand
{

    // Execute method (ran upon command)
    public void Execute()
    {
        // Debug console out
        Console.Write("Running Report Command");

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