using System.Management.Automation;
using System.Threading.Tasks;

public static class PSRunner
{
    public static async Task<string> RunAsync(string command)
    {
        using PowerShell ps = PowerShell.Create();
        ps.AddScript(command);
        var results = await Task.Factory.FromAsync(ps.BeginInvoke(), ps.EndInvoke);

        return string.Join("\n", results.Select(r => r.ToString()));
    }
}