/*

Stewart CLI
/System/RegRunner.cs - Used to access the registry asynchronously and check for specific bits in a specific key/value
JohnDavid Abe 

*/

using Microsoft.Win32;

public static class RegRunner
{
    public static bool CheckRegistryBit(string path, string name, string expectedBitHex)
    {
        // Open the registry at the specifified reg key path
        using var key = Registry.LocalMachine.OpenSubKey(path);

        // If the key does not exist, then the specific bit is not set, thus return false
        if (key == null) return false;

        // Get the data from the specific key/value pair
        byte[] data = key.GetValue(name) as byte[];

        // Once again, if the value does not exist, then the specific bit is not set, thus return false
        if (data == null || data.Length < 1) return false;

        // Convert the expected bit from hex to byte and check if the specific bit is set
        byte expectedBit = Convert.ToByte(expectedBitHex, 16);

        // Check if the specific bit is set, if not it is not set, thus return false
        return (data[0] & expectedBit) == 0;
    }
}