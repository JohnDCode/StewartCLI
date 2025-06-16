/*

Stewart CLI
/Program.cs - Entry point for executable, no additional logic (directs arguments to command router)
JohnDavid Abe 

*/

using System;

class Program
{
    static void Main(string[] args)
    {
        // Route arguments from command to the router
        var router = new Router();
        router.Route(args);
    }
}