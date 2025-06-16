/*

Stewart CLI
/Interfaces/ICommand.cs - Interface for each command
JohnDavid Abe 

*/

using System;

public interface ICommand
{
    // Each command must have an execute method (to run the command)
    void Execute();
}