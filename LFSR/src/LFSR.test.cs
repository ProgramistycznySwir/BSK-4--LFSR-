using System.Collections;
using System.Numerics;
using System.Text;
using LFSR.Helpers;
using Xunit;

namespace LFSR;


public class LFSR_Test
{
    [Theory]
	[InlineData("0000000000000000000000", "0010", "4 1", "0111101011001000111101")]
	[InlineData("0000000000000000000000", "1000", "4 1", "1110101100100011110101")]
	[InlineData("0000000000000000000000", "1000", "3 1", "1101001110100111010011")]
	// [InlineData("000000000000000000000000000000000000000000000", "0011011110011011001101011110", "32 44 12", "100111101100100111101100100111101100100111101")]
	// [InlineData("000000000000000000000000000000000000000000000", "0011011110011011001101011110", "32 44 12", "110011110110011001111011001100111101100110011")]
	public void ShiftInputUsingGenerator(string input, string seed_string, string polynomial, string output_Expected)
	{
		// Arrange:
		var lfsr = new LFSR(seed_string, polynomial);
		var gen = lfsr.GetEnumerator();
		// gen.Reset();
		// Act:
		var bob = new StringBuilder();
		foreach(bool item in input.Select(e => e is '1'))
			bob.Append((item ^ gen.PopMoveNext()) ? 1 : 0);
		var output = bob.ToString();
		// Assert:
		Assert.Equal(output_Expected, output);
	}
    [Theory]
	[InlineData("0000000000000000000000", "0010", "1001", "0111101011001000111101")]
	[InlineData("0000000000000000000000", "1000", "0101", "0100010100010100010100")]
	[InlineData("0000000000000000000000", "1000", "1010", "1101001110100111010011")]
	[InlineData("000000000000000000000000000000000000000000000", "0011011110011011001101011110", "00000000000100000000000000000001000000000001", "100111101100100111101100100111101100100111101")]
	public void ShiftInputUsingGenerator1(string input, string seed_string, string polynomial_string, string output_Expected)
	{
		// Arrange:
		var lfsr = new LFSR(seed_string, FormatInterOP.Polynomial_BinToMy(polynomial_string));
		var gen = lfsr.GetEnumerator();
		// gen.Reset();
		// Act:
		var bob = new StringBuilder();
		foreach(bool item in input.Select(e => e is '1'))
			bob.Append((item ^ gen.PopMoveNext()) ? 1 : 0);
		var output = bob.ToString();
		// Assert:
		Assert.Equal(output_Expected, output);
	}
}
