/*

Stewart CLI
/Commands/a.cs - Test command "a", for DEBUG use only
JohnDavid Abe 

*/

using System;
using System.Diagnostics;

public class ACommand : ICommand
{

    // Execute method (ran upon command)
    public void Execute()
    {
        // Debug console out
        Console.Write("Running Debug Command 'A'!");
    }
}