using MultiPrecision;
using System;
using System.IO;

namespace ErfiApproximation {
    internal class Program {
        static void Main(string[] args) {
            using (StreamWriter sw = new("../../../../results_disused/erfi_limit_n4.csv")) {
                sw.WriteLine("x,y");
                
                for (MultiPrecision<Pow2.N4> x = 10; x >= 0; x -= 1d / 64) {
                    (MultiPrecision<Pow2.N4> f, int m) = Erfi<Pow2.N4>.CfracConvergence(x);
                    (MultiPrecision<Pow2.N4> l, int k) = Erfi<Pow2.N4>.Limit(x);

                    MultiPrecision<Pow2.N4> y = Erfi<Pow2.N4>.Value(x, m);

                    Console.WriteLine($"{x}\t{y}\t{m}");

                    sw.WriteLine($"{x},{y}");
                }
            }

            Console.WriteLine("END");
            Console.Read();
        }
    }
}
