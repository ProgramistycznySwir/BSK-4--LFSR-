

internal static class IEnumerator_Ext
{
    /// <summary>
    /// Returns Enumerator.Current and moves Enumerator
    /// </summary>
    public static T PopMoveNext<T>(this IEnumerator<T> self)
    {
        T temp = self.Current;
        self.MoveNext();
        return temp;
    }
    /// <summary>
    /// Fluent .MoveNext(). Just .MoveNext() that returns enumerator.
    /// </summary>
    public static IEnumerator<T> Init<T>(this IEnumerator<T> self, out bool success)
    {
        success = self.MoveNext();
        return self;
    }
    /// <summary>
    /// Fluent .MoveNext(). Just .MoveNext() that returns enumerator (if you want bool of success there is an override).
    /// </summary>
    public static IEnumerator<T> Init<T>(this IEnumerator<T> self)
    {
        self.MoveNext();
        return self;
    }
}

internal static class IEnumerable_Ext
{
    /// <summary>
    /// Returns Enumerator.Current and moves Enumerator
    /// </summary>
    public static string CollectString(this IEnumerable<char> self)
        => new string(self.Select(x => x).ToArray());
}

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