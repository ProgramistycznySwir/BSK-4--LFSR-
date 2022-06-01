using System.Collections;
using System.IO;
using System.Numerics;
using System.Text;
using Xunit;

namespace LFSR;


public class LFSR_Encryptor_Test
{
    [Theory]
    [InlineData(58307422, "6 4 2")]
    [InlineData(1234567890, "32 44 12")]
	public void ShiftFile_TowWayEncryption(BigInteger seed, string polynomial)
	{
		//TODO: Implement tests
		// Arrange:
		// Act:
		// Assert:
		// // Clean Up:
	}
}
