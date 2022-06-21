using System.Collections;
using System.IO;
using System.Numerics;
using System.Text;
using System.Linq;

namespace LFSR;


public class LFSR_Encryptor
{
    public readonly string Seed;
    public readonly string Polynomial;

	private LFSR _lsfr;

    public LFSR_Encryptor(string seed, string polynomial)
    {
		Polynomial = polynomial;
        Seed = seed;
		_lsfr = new LFSR(Seed, polynomial);
    }

	/// <summary>
	/// This function is both for encryption and decryption.
	/// </summary>
	public void ShiftFile(string inputFilePath, string? outputFilePath = null)
	{
		Reset();
		if(outputFilePath is null)
			outputFilePath = GetOutputFilePath(inputFilePath);
		
		using(var inputFile = new BinaryReader(new FileStream(inputFilePath, FileMode.Open, FileAccess.Read)))
		using(var outputFile = new BinaryWriter(new FileStream(outputFilePath, FileMode.Create, FileAccess.Write)))
			while (inputFile.BaseStream.Position != inputFile.BaseStream.Length)
				outputFile.Write(ShiftByte(inputFile.ReadByte()));
	}
	public byte ShiftByte(byte inputByte)
		=> (byte)(inputByte ^ _lsfr.GetByte());

	public void Reset()
		=> _lsfr.Reset();

	public static string GetOutputFilePath(string inputFilePath)
		=> $"{Path.GetFileNameWithoutExtension(inputFilePath)}_out{Path.GetExtension(inputFilePath)}";
}