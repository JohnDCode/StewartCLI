using System.Text.Json;

public static class VulnerabilityRepository
{
    public static List<Vulnerability> Load(string path)
    {
        string json = File.ReadAllText(path);
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        return JsonSerializer.Deserialize<List<Vulnerability>>(json, options);
    }
}