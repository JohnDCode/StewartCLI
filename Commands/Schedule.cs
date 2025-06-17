/*

Stewart CLI
/Commands/Schedule.cs - Schedule command, schedules the running of stewart commands (on set times or periodically)
JohnDavid Abe 

*/

using System;
using System.Diagnostics;

public class ScheduleCommand : ICommand
{

    // Execute method (ran upon command)
    public void Execute()
    {
        // Debug console out
        Console.Write("Running Schedule Command");

        RunSingleCommand().Wait();
    }

    private async Task RunSingleCommand()
    {
        string command = "Get-Date";
        string result = await PowerShellRunner.RunAsync(command);
        Console.WriteLine($"Output:\n{result}");
    }
}