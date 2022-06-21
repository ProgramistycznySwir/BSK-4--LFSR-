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
            for(int i = max; i >= 0; i--)
                bob.Append(polynomial.Contains(i) ? '1' : '0');
            return bob.ToString();
        }
    }
}