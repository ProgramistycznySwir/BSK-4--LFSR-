using System.Numerics;
using System.Text;

internal static class BigInteger_Ext
{
    public static string ToNBase(this BigInteger a, int n)
    {
        StringBuilder bob = new StringBuilder();
        while (a > 0)
        {
            bob.Insert(0, a % n);
            a /= n;
        }

        return bob.ToString();
    }
}