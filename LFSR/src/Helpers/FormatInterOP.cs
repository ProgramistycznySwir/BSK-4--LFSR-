using System.Text;

namespace LFSR.Helpers
{
    public static class FormatInterOP
    {
        public static string Polynomial_MyToBin(string myPolynomial)
        {
            var polynomial = LFSR.ParsePolynomial(myPolynomial);
            int max = polynomial.Max();
            var bob = new StringBuilder();
            for(int i = 0; i <= max; i++)
                bob.Append(polynomial.Contains(i) ? '1' : '0');
            return bob.ToString();
        }
        public static string Polynomial_BinToMy(string binPolynomial)
        {
            return string.Join(" ", binPolynomial.Select((e, i) => e is '1' ? i+1 : 0).Where(e => e is not 0));
        }
    }
}