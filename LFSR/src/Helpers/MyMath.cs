

namespace LFSR.Math;

internal static class MyMath
{
    /// <summary>
    /// Modulo function that returns value in range 0-[mod]
    /// </summary>
    public static int ClampMod(int value, int mod)
    {
        value %= mod;
        value += value < 0 ? mod : 0;
        return value;
    }
}