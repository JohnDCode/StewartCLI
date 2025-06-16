using System;

class Program
{
    static void Main(string[] args)
    {
        var router = new CommandRouter();
        router.Route(args);
    }
}