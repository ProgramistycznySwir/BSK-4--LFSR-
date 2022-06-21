using System.Collections;
using System.IO;
using System.Numerics;
using System.Text;
using Xunit;

namespace LFSR;


public class LFSR_Encryptor_Test
{
    // [Theory]
	// [InlineData("111010011110111", "10", "4 1", "100100110010011")]
	public void ShiftFile_TowWayEncryption(string input, string seed_string, string polynomial, string output_Expected)
	{
		//TODO: Implement tests
		// Arrange:
		var lfsr = new LFSR(seed_string, polynomial);
		var gen = lfsr.GetEnumerator();
		// Act:
		var bob = new StringBuilder();
		foreach(char item in input)
			bob.Append(((item ^ Convert.ToByte(gen.PopMoveNext())) & 1) is 0 ? 0 : 1);
		var output = bob.ToString();
		// Assert:
		Assert.Equal(output_Expected, output);
	}
}
