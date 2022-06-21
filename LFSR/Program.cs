using System.IO;
using System.Numerics;
using System.Text;
using LFSR;
using LFSR.Helpers;
using static System.Console;


Console.WriteLine(FormatInterOP.Polynomial_MyToBin("32 44 12"));
return;
void Test() {
	var seed = "1000";
	var polynomial = "4 1";
	var lfsr = new LFSR.LFSR(seed, polynomial);
	var gen = lfsr.GetEnumerator();
	while(true)
	{
		bool bit = gen.Current;
		Console.WriteLine($"{string.Join("", gen.State.Select(e => e?1:0))} -> {bit}");
		Console.ReadKey(true);
		gen.MoveNext();
	}
}
Test();

return;

BigInteger DefaultSeed = 1234567890;
const string DefaultPolynomial = "32 44 12";

WriteLine("Podaj wartość seed generatora (dowolną liczbę całkowitą) i zatwierdź [ENTER].\n" + 
$"  możesz także wcisnąć zwyczajnie [ENTER] wtedy zostanie zostawiona wartość domyślna: ({DefaultSeed}).");
Write(": ");
string rawSeed = ReadLine()!;
// BigInteger seed;
// if(rawSeed is "")
// 	seed = DefaultSeed;
// else
// 	seed = BigInteger.Parse(rawSeed!);

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
			foreach(bool bit in new LFSR.LFSR(rawSeed, polynomial))
				Write(bit ? '1' : '0');
			return;
		}
	default: WriteLine("Zły wybór, zamykam program."); return;
}

LFSR_Encryptor encryptor = new(rawSeed, polynomial);

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

