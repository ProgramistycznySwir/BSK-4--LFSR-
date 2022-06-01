using System.Collections;
using System.Numerics;

namespace LFSR;

public class LFSR_Generator : IEnumerator<bool>
{
    public readonly BigInteger Seed;
	private int[] taps;
	private int taps_Max;
    public BigInteger State { get; private set; }

	public LFSR_Generator(BigInteger seed, HashSet<int> taps)
	{
        Seed = seed;
		this.taps = taps.ToArray();
        taps_Max = taps.Max();
		Reset();
	}

	object IEnumerator.Current => Current;
	public bool Current => Convert.ToBoolean((int)State & 1);

	public void Dispose() { }

	public bool MoveNext()
	{
        string State_string = State.ToBase(2).PadLeft(taps_Max + 1, '0');
		int bit = 0;
		foreach (int tap in taps)
			bit ^= State_string[taps_Max - tap];
		State >>= 1;
		State |= bit << taps_Max;
        return true;
	}

	public void Reset()
		=> State = Seed & ((1 << (taps_Max + 1)) - 1);
}