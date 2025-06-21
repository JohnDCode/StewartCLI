/*

Stewart CLI
/Commands/Snap.cs - Snap command, takes snapshot of system policies to later be imported with stewart restore
JohnDavid Abe 



using System;
using System.Diagnostics;

public class SnapCommand : ICommand
{

    // Execute method (ran upon command)
    public void Execute()
    {
        // Debug console out
        Console.Write("Running Snap Command");

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