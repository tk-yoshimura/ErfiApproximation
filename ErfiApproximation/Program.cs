using MultiPrecision;
using System;
using System.IO;

namespace ErfiApproximation {
    internal class Program {
        static void Main(string[] args) {
            using (StreamWriter sw = new("../../../../results_disused/erfi_convergence_n32.csv")) {
                sw.WriteLine("x,c(x),c.frac m,limit k");

                for (MultiPrecision<Pow2.N32> x = 1d / 2; x <= 64; x += 1d / 2) {
                    (MultiPrecision<Pow2.N32> f, int m) = Erfi<Pow2.N32>.CfracConvergence(x);
                    (MultiPrecision<Pow2.N32> l, int k) = Erfi<Pow2.N32>.Limit(x);

                    Console.WriteLine($"{x}\n{f}\n{l}");

                    sw.WriteLine($"{x},{f},{m},{((k >= 0) ? k.ToString() : "N/A")}");
                }
            }

            Console.WriteLine("END");
            Console.Read();
        }
    }
}
