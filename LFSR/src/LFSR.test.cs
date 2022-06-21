using System.Collections;
using System.Numerics;
using System.Text;
using Xunit;

namespace LFSR;


public class LFSR_Test
{
    [Theory]
	[InlineData("111010011110111", "2", "3", "100100110010011")]
	[InlineData("111111111111111", "2", "3", "100001010011011")]
	[InlineData("111111111111111", "4", "3", "110000101001101")]
	[InlineData("000000000000000000000000000000000000000000000", "2", "3", "011110101100100011110101100100011110101100100")]
	[InlineData("000000000000000000000000000000000000000000000", "4", "3", "011110101100100011110101100100011110101100100")]
	[InlineData("000000000000000000000000000000000000000000000", "58307422", "32 44 12", "100010100100011110011100001010001011111001111")]
	public void ShiftInputUsingGenerator(string input, string seed_string, string polynomial, string output_Expected)
	{
		// Arrange:
		BigInteger seed = BigInteger.Parse(seed_string);
		var lfsr = new LFSR(seed, polynomial);
		var gen = lfsr.GetEnumerator();
		// gen.Reset();
		// Act:
		var bob = new StringBuilder();
		foreach(char item in input)
			bob.Append(((item ^ Convert.ToByte(gen.PopMoveNext())) & 1) is 0 ? 0 : 1);
		var output = bob.ToString();
		// Assert:
		Assert.Equal(output_Expected, output);
	}
}
