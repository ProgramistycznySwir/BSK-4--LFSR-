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
}