using System.Collections;
using System.Numerics;

namespace LFSR;


public class LFSR : IEnumerable<bool>
{
    public readonly bool[] Seed;
    public readonly string Polynomial;
    public readonly HashSet<int> Taps;

	private LFSR_Generator generator;

    public LFSR(string seed, string polynomial)
    {
		Polynomial = polynomial;
        Taps = ParsePolynomial(polynomial);
        Seed = ParseSeed(seed, Taps.Max());
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
			result |= (byte)((generator.PopMoveNext() ? 1 : 0) << i);
		return result;
	}

	internal static bool[] ParseSeed(string seed, int lenght)
		=> seed.Select(e => e is '1').Take(lenght).ToArray();

	// Polynomial format: 32 2 13 => x^32 + x^13 + x^2 + 1
	//	(last constant one is assumed by default)
	//	(order of exponents is arbitrary)
	//	(only primitive polynomials are accepted)
	internal static HashSet<int> ParsePolynomial(string rawPolynomial)
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
