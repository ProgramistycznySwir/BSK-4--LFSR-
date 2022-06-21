using System.Collections;
using System.Numerics;
using LFSR.Helpers;

namespace LFSR;

public class LFSR_Generator : IEnumerator<bool>
{
    public readonly bool[] Seed;
	private int[] taps;
	public readonly int taps_Max;
    public RotaryList<bool> State { get; private set; }

	public LFSR_Generator(bool[] seed, HashSet<int> taps)
	{
        Seed = seed;
		this.taps = taps.ToArray();
        taps_Max = taps.Max();
		Reset();
	}

	object IEnumerator.Current => Current;
	public bool Current => State.Last;

	public void Dispose() { }

	public bool MoveNext()
	{
		bool bit = false;
		foreach (int tap in taps)
			bit ^= State[tap + 1];
		State.RemoveLast();
		State.AddFirst(bit);
        return true;
	}
	
	public bool MoveNext(uint amount)
	{
		for(int i = 0; i < amount; i++)
			MoveNext();
        return true;
	}

	public void Reset()
	{
		State = new(Seed);
	}
}