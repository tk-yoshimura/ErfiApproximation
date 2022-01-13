using MultiPrecision;
using System;
using System.IO;

namespace ErfiApproximation {
    internal class Program {
        static void Main(string[] args) {
            using (StreamWriter sw = new("../../../../results/cfrac_convergence.csv")) {
                sw.WriteLine("x,y,f,m");
                
                for (MultiPrecision<Pow2.N4> x = 1; x <= 64; x += 1d / 64) {
                    (MultiPrecision<Pow2.N4> f, int m) = Erfi<Pow2.N4>.Convergence(x);

                    MultiPrecision<Pow2.N4> y = Erfi<Pow2.N4>.Value(x, m);

                    Console.WriteLine($"{x}\t{y}\t{m}");

                    sw.WriteLine($"{x},{y},{f},{m}");
                }
            }

            Console.WriteLine("END");
            Console.Read();
        }
    }
}
