using System.Collections;
using System.Numerics;
using System.Text;
using Xunit;

namespace LFSR;


public class LFSR_Generator_Test
{
    [Theory]
    [InlineData(58307422, "6 4 2", "")]
	public void MoveNextAndCurrent_ShouldProduceSameSequencesOf32BitsAsOnlineGenerator(BigInteger seed, string polynomial, string output_Expected)
	{
		//TODO: Implement tests
	}
}
