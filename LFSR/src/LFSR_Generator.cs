using System.Collections;
using System.Numerics;

namespace LFSR;

public class LFSR_Generator : IEnumerator<bool>
{
    public readonly bool[] Seed;
	private int[] taps;
	public readonly int taps_Max;
    public bool[] State { get; private set; }

	public LFSR_Generator(bool[] seed, HashSet<int> taps)
	{
        Seed = seed;
		this.taps = taps.ToArray();
        taps_Max = taps.Max();
		Reset();
	}

	object IEnumerator.Current => Current;
	public bool Current => (State & 1u << taps_Max) == 0 ? false : true;

	public void Dispose() { }

	public bool MoveNext()
	{
        string State_string = State.ToBase(2).PadLeft(taps_Max + 2, '0');
		BigInteger bit = 0u;
		foreach (int tap in taps)
			bit ^= State_string[tap + 1];
		// bit &= 1;
		State >>= 1;
		// State &= ~(1u << taps_Max);
		State |= bit << taps_Max;
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
		// State = Seed & ((1u << (taps_Max + 1)) - 1);
		State = Seed;
		// MoveNext((uint)(taps_Max+1));
		// MoveNext(4u);
	}
}