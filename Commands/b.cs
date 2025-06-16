/*

Stewart CLI
/Commands/a.cs - Test command "b", for DEBUG use only
JohnDavid Abe 

*/

using System;
using System.Diagnostics;

public class BCommand : ICommand
{

    // Execute method (ran upon command)
    public void Execute()
    {
        // Debug console out
        Console.Write("Running Debug Command 'B'!");
    }
}