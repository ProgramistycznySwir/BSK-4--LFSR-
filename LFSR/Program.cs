using System.IO;
using System.Numerics;
using System.Text;
using LFSR;
using LFSR.Helpers;
using static System.Console;

// Console.WriteLine(FormatInterOP.Polynomial_MyToBin("32 44 12"));
// byte
Console.WriteLine(new BigInteger(~(1u << 3)).ToBase(2).PadLeft(3 + 1, '0'));
return;
void Test() {
	// Arrange:
	var input = "111010011110111";
	var seed = 2;
	var polynomial = "3";
	var output_Expected = "100100110010011";

	var lfsr = new LFSR.LFSR(seed, polynomial);
	var gen = lfsr.GetEnumerator();
	// Act:
	var bob = new StringBuilder();
	foreach(char item in input)
	{
		Console.Write($"{gen.State.ToBase(2).PadLeft(gen.taps_Max + 2, '0')} ->");
		byte bit = Convert.ToByte(gen.PopMoveNext());
		Console.Write(bit);
		bob.Append(((item ^ bit) & 1) is 0 ? 0 : 1);
		Console.WriteLine();
	}
	var output = bob.ToString();
	// Assert:
	Console.WriteLine(output_Expected);
	Console.WriteLine(output);
	Console.WriteLine("000101100001000");
	Console.WriteLine(output_Expected.Equals(output));
	// Assert.Equal(output_Expected, output);
}
Test();

return;

BigInteger DefaultSeed = 1234567890;
const string DefaultPolynomial = "32 44 12";

WriteLine("Podaj wartość seed generatora (dowolną liczbę całkowitą) i zatwierdź [ENTER].\n" + 
$"  możesz także wcisnąć zwyczajnie [ENTER] wtedy zostanie zostawiona wartość domyślna: ({DefaultSeed}).");
Write(": ");
string rawSeed = ReadLine()!;
BigInteger seed;
if(rawSeed is "")
	seed = DefaultSeed;
else
	seed = BigInteger.Parse(rawSeed!);

WriteLine("Podaj kolejne potęgi wielomianu. Oddzielaj je przy pomocy spacji i zatwierdź [ENTER].\n" + 
"  Przykład: dla wielomianu f(x) = x^32 + x^2 + x^14 + 1 wprowadź \"32 2 14 \" (stała jedynka jest dodawana domyślnie)\n" + 
$"  możesz także wcisnąć zwyczajnie [ENTER] wtedy zostanie użyta wartość domyślna ({DefaultPolynomial})");
Write(": ");
string polynomial = ReadLine()!;
if(polynomial is "")
	polynomial = DefaultPolynomial;



Write("Wybierz tryb: 1. Szyfrowanie plików, 2. Wypisywanie wyjścia LFSR w nieskończoność\n:");
char mode = ReadKey().KeyChar;
switch (mode)
{
	case '1': break;
	case '2': {
			foreach(bool bit in new LFSR.LFSR(seed, polynomial))
				Write(bit ? '1' : '0');
			return;
		}
	default: WriteLine("Zły wybór, zamykam program."); return;
}

LFSR_Encryptor encryptor = new(seed, polynomial);

while(true)
{
	Write("Wpisz ścieżkę pliku do zaszyfrowania: ");
	string filePath = ReadLine()!;
	if(File.Exists(filePath) is false)
	{
		Console.ForegroundColor = ConsoleColor.DarkRed;
		Console.WriteLine("Nie ma takiego pliku!");
		Console.ResetColor();
		continue;
	}

	encryptor.ShiftFile(filePath);
	
	Console.ForegroundColor = ConsoleColor.Green;
	Console.WriteLine("Pomyślnie zaszyfrowano plik!");
	Console.WriteLine($"Ścieżka do niego to: {LFSR_Encryptor.GetOutputFilePath(filePath)}");
	Console.ResetColor();
}

