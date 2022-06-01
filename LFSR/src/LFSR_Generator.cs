using System.Collections;
using System.Numerics;
using System.Text;

namespace LFSR;


public class LFSR : IEnumerable<bool>
{
    public readonly BigInteger Seed;
    public readonly string Polynomial;
    public readonly HashSet<int> Taps;

	private LFSR_Generator generator;

    public LFSR(BigInteger seed, string polynomial)
    {
		Polynomial = polynomial;
        Taps = ParsePolynomial(polynomial);
        Seed = seed;
		generator = new LFSR_Generator(Seed, Taps);
    }

	IEnumerator<bool> IEnumerable<bool>.GetEnumerator()
        => GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();

	public LFSR_Generator GetEnumerator()
        => generator;

	public byte GetByte()
	{
		byte result = 0;
		for(int i = 7; i >= 0; i--)
			result |= (byte)((generator.MoveNext() ? 1 : 0) << i);
		return result;
	}

	// Polynomial format: 32 2 13 => x^32 + x^13 + x^2 + 1
	//	(last constant one is assumed by default)
	//	(order of exponents is arbitral)
	//	(only primitive polynomials are accepted)
	public HashSet<int> ParsePolynomial(string rawPolynomial)
	{
		const string ExponentsSeparator = " ";
		var result = new HashSet<int>();
		foreach (string rawExponent in rawPolynomial.Split(ExponentsSeparator))
			result.Add(int.Parse(rawExponent));
		result.Add(0); // Add constant one
		
		if(result.Count < 2)
			throw new ArgumentException($"Couldn't parse polynomial: {rawPolynomial}");
		
		return result;
	}

	public void Reset()
		=> generator.Reset();
}

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
        State = Seed & ((1 << (taps_Max + 1)) - 1);
	}

	object IEnumerator.Current => Current;
	public bool Current => Convert.ToBoolean((int)State & 1);

	public void Dispose() { }

	public bool MoveNext()
	{
        // State = State & ((1 << (taps_Max + 1)) - 1);
        string State_string = State.ToNBase(2).PadLeft(taps_Max + 1, '0');
		int bit = 0;
		foreach (int tap in taps)
			bit ^= State_string[taps_Max - tap];
		State >>= 1;
		State |= bit << taps_Max;
        return Current;
	}

	public void Reset()
	{
        State = Seed;
	}
}
