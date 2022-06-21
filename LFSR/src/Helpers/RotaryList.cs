using System.Collections;
using System.Linq;
using LFSR.Math;

namespace LFSR.Helpers;

public class RotaryList<T> : IList<T>
{
    public const int DefaultContainerSize = 32;
    private T[] _container;
    private int FirstIdx = 0;
    public int Capacity => _container.Length;
    public int Count { get; private set; }

    private int MapIndex(int index) => MyMath.ClampMod(FirstIdx + MyMath.ClampMod(index, Count), Capacity);
    // TODO: In future catch errors related to index out of bound.
	public T this[int index] { get => _container[MapIndex(index)]; set => _container[MapIndex(index)] = value; }
    
    public T First => this[0];
    public T Last => this[-1];

	public bool IsReadOnly => false; // TODO: In future make it also immutable.

    public RotaryList(IEnumerable<T> seed)
    {
        _container = seed.ToArray();
        Count = Capacity;
    }
    public RotaryList(int size = DefaultContainerSize) => _container = new T[size];
    public RotaryList(IEnumerable<T> seed, int size = DefaultContainerSize)
    {
        Count = seed.Count();
        _container = new T[size];
        seed.ToArray().CopyTo(_container.AsSpan());
    }

    /// <summary> Adds as last element </summary>
	public void Add(T item)
	{
        if(Count >= Capacity)
            Expand();
        Count++;
        this[-1] = item;
	}
	public void AddFirst(T item)
	{
        if(Count >= Capacity)
            Expand();
        FirstIdx = FirstIdx is 0 ? Capacity -1 : FirstIdx -1;
        this[0] = item;
        Count++;
	}

	public void Clear()
	{
        FirstIdx = 0;
        Count = 0;
	}

	public bool Contains(T item) => this.Contains(item);

	public void CopyTo(T[] array, int arrayIndex = 0)
	{
        _container.CopyTo(array, arrayIndex + FirstIdx);
        int currIdx = FirstIdx;
        foreach(T item in _container.Take(FirstIdx))
            array[currIdx++] = item;
	}

	public IEnumerator<T> GetEnumerator()
	{
        for(int i = 0; i < Count; i++)
            yield return this[i];
	}
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	public int IndexOf(T item)
	{
        for(int i = 0; i < Count; i++)
            if(this[i].Equals(item))
                return i;
        return -1;
	}

	public void Insert(int index, T item)
	{
		throw new NotImplementedException();
	}

    public void RemoveLast() => Count--;
	public T PopLast()
    {
        var temp = this[-1];
        RemoveLast();
        return temp;
    }
    public void RemoveFirst() => FirstIdx = MapIndex(1);
	public T PopFirst(T item)
	{
		var temp = this[0];
        RemoveFirst();
        return temp;
	}
	public bool Remove(T item)
	{
		throw new NotImplementedException();
	}

	public void RemoveAt(int index)
	{
		throw new NotImplementedException();
	}


    /// <param name="by">By default container's capacity is doubled.</param>
    private void Expand(int? by = null)
    {
        by ??= Capacity;
        if(by <= 0)
            throw new ArgumentOutOfRangeException(nameof(by), by, "Value should be grater than 0");
        int newCapacity = Capacity + by.Value;

        T[] newContainer = new T[newCapacity];
        CopyTo(newContainer);
        _container = newContainer;
    }
}
