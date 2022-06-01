using System.IO;
using System.Numerics;
using LFSR;
using static System.Console;


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
	string filePath = ReadLine();
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

