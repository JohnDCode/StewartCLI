using Microsoft.Win32;

public static class RegRunner
{
    public static bool CheckRegistryBit(string path, string name, string expectedBitHex)
    {
        using var key = Registry.LocalMachine.OpenSubKey(path);
        if (key == null) return false;

        byte[] data = key.GetValue(name) as byte[];
        if (data == null || data.Length < 1) return false;

        byte expectedBit = Convert.ToByte(expectedBitHex, 16);
        return (data[0] & expectedBit) == 0; // if bit not set, Guest enabled
    }
}