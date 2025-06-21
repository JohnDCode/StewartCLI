public class Vulnerability
{
    public required string Id { get; set; }
    public required string Description { get; set; }
    public required string CheckType { get; set; }

    // For PowerShell
    public string? CheckCommand { get; set; }
    public string? ExpectedValue { get; set; }
    public string? FixCommand { get; set; }

    // For Custom
    public string? CustomCheckName { get; set; }
    public string? CustomFixName { get; set; }

    // For Registry
    public string? RegistryPath { get; set; }
    public string? RegistryValueName { get; set; }
    public string? RegistryExpectedBit { get; set; } // e.g. "0x10"
    public string? RegistryFixValue { get; set; }
    public string? RegistryFixType { get; set; } // e.g. "BitMaskSet"
}