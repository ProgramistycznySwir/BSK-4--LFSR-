using System.Collections;
using System.Numerics;
using System.Text;

namespace LFSR;


public class LFSR : IEnumerable<bool>
{
    public readonly BigInteger Seed;
    public readonly int[] Taps;

    public LFSR(BigInteger seed, int[] _taps)
    {
        Taps = _taps;
        Seed = seed;
    }

    // public void DecToBin(string text)
    // {
    //     int[] numbers = text.Cast<int>().ToArray();
    //     int maxNumber = numbers.Max();
    //     var result = numbers.
    // }

	// function* lfsr (taps: Set<number>, startingState: number)
	// {
	//     const max = Math.max(...taps)

	//     const tapsArray = [...taps]

	//     // NOTE: Limit startingState to only max + 1 bits
	//     let lfsr = startingState & ((1 << (max + 1)) - 1)

	//     while (true) {
	//         yield lfsr & 1

	//         // NOTE: Add missing zeros to the beginning of the binary string
	//         const binaryString = lfsr.toString(2).padStart(max + 1, '0')

	//         let bit = +binaryString[max - tapsArray[1]] ^ +binaryString[max - tapsArray[0]]
	//         for (const tap of tapsArray.slice(2)) {
	//         bit ^= +binaryString[max - tap]
	//         }

	//         lfsr >>= 1
	//         lfsr |= bit << max
	//     }
	//     }

	//     export default (polynomial: string, startingState: number = 0xdeadbeef) => {
	//     const taps = compilePolynomial(polynomial)
	//     return lfsr(taps, startingState)
	// }
	IEnumerator<bool> IEnumerable<bool>.GetEnumerator()
        => GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();

	public LFSR_Generator GetEnumerator()
        => new LFSR_Generator(Seed, Taps);
}

public class LFSR_Generator : IEnumerator<bool>
{
    public readonly BigInteger Seed;
	private int[] taps;
	private int taps_Max;
    public BigInteger State { get; private set; }

	public LFSR_Generator(BigInteger seed, int[] taps)
	{
        Seed = seed;
		this.taps = taps;
        taps_Max = taps.Max();
        State = Seed & ((1 << (taps_Max + 1)) - 1);
	}

	object IEnumerator.Current => Current;
	public bool Current => Convert.ToBoolean(State & 1);

	public void Dispose() { }

	public bool MoveNext()
	{
        State = State & ((1 << (taps_Max + 1)) - 1);
        ToNBase()
        return Current;
	}

	public void Reset()
	{
        State = Seed;
	}
}

internal static class BigIntegerExtensions
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