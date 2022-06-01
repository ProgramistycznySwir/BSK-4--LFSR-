using Main;
using static System.Console;


// See https://aka.ms/new-console-template for more information
WriteLine();

WriteLine("Podaj kolejne współczynniki wielomianu numerując od potęgi 0. Oddzielaj je przy pomocy spacji. " + 
"Przykład, dla wielomianu f(x) = 1 + 3 * x^2 + 2 * x^3 wprowadź \"1 0 3 2\" i zatwierdź [ENTER]");
WriteLine(":");


string rawInput = ReadLine();
List<float> polynomial = rawInput.Split(' ').Select(x => float.Parse(x));

