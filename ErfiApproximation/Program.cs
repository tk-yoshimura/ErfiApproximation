using MultiPrecision;
using System;
using System.IO;

namespace ErfiApproximation {
    internal class Program {
        static void Main(string[] args) {
            using (StreamWriter sw = new("../../../../results_disused/erfi_value_check.csv")) {
                for (double x = 1d / 64; x <= 64; x += 1d / 64) {
                    MultiPrecision<Pow2.N4> y4 = ErfiN4.C(x);
                    MultiPrecision<Pow2.N8> y8 = ErfiN8.C(x);
                    MultiPrecision<Pow2.N16> y16 = ErfiN16.C(x);
                    MultiPrecision<Pow2.N32> y32 = ErfiN32.C(x);

                    Console.WriteLine(x);
                    Console.WriteLine(y4);
                    Console.WriteLine(y8);
                    Console.WriteLine(y16);
                    Console.WriteLine(y32);

                    sw.WriteLine(x);
                    sw.WriteLine(y4);
                    sw.WriteLine(y8);
                    sw.WriteLine(y16);
                    sw.WriteLine(y32);
                }
            }

            Console.WriteLine("END");
            Console.Read();
        }
    }
}
