using MultiPrecision;
using System;
using System.Collections.Generic;
using System.IO;

namespace ErfiApproximation {
    internal class Program {
        static void Main(string[] args) {
            using (StreamWriter sw = new("../../../../results/erfi_pade_table_p5_e31.csv")) {
                for (MultiPrecision<Pow2.N16> x = 0.5; x <= 4; x += 0.5) {

                    MultiPrecision<Pow2.N16> ddx = Math.ScaleB(1, -2);

                    MultiPrecision<Pow2.N32>[] ds = FiniteDifference<Pow2.N32>.Diff(
                        x.Convert<Pow2.N32>(), ErfiN32.C, Math.ScaleB(1, -24)
                    );

                    List<MultiPrecision<Pow2.N16>> expecteds = new();

                    for (MultiPrecision<Pow2.N16> dx = -ddx, h = ddx / 4096; dx <= ddx; dx += h) {
                        MultiPrecision<Pow2.N16> expected = ErfiN16.C(x + dx);

                        expecteds.Add(expected);
                    }

                    for (int n = 4; n <= 16; n += 1) {
                        MultiPrecision<Pow2.N16>[] cs = new MultiPrecision<Pow2.N16>[n * 2 + 1];
                        cs[0] = ErfiN16.C(x);
                        for (int i = 0; i < n * 2; i++) {
                            cs[i + 1] = ds[i].Convert<Pow2.N16>() * MultiPrecision<Pow2.N16>.TaylorSequence[i + 1];
                        }

                        (MultiPrecision<Pow2.N16>[] ms, MultiPrecision<Pow2.N16>[] ns) = PadeSolver<Pow2.N16>.Solve(cs, n, n);

                        MultiPrecision<Pow2.N16> err = 0;

                        for ((MultiPrecision<Pow2.N16> dx, MultiPrecision<Pow2.N16> h, int i) = (-ddx, ddx / 4096, 0); i < expecteds.Count; dx += h, i++) {
                            MultiPrecision<Pow2.N16> expected = expecteds[i];
                            MultiPrecision<Pow2.N16> actual = PadeSolver<Pow2.N16>.Approx(dx, ms, ns);

                            if (!expected.IsFinite) {
                                continue;
                            }

                            err = MultiPrecision<Pow2.N16>.Max(err, MultiPrecision<Pow2.N16>.Abs(expected / actual - 1));
                        }

                        Console.WriteLine($"x={x}, n={n}, |dx| = {ddx}");
                        Console.WriteLine($"relative error = {err:e10}");

                        if (err < 2e-31 || n == 16) {
                            sw.WriteLine($"x={x}, n={n}, |dx| = {ddx}");

                            sw.WriteLine($"i,p_i,q_i");
                            for (int i = 0; i <= n; i++) {
                                sw.WriteLine($"{i},{ms[i]:e64},{ns[i]:e64}");
                            }

                            sw.WriteLine($"relative error = {err:e10}\n");

                            sw.Flush();

                            break;
                        }
                    }
                }
            }

            Console.WriteLine("END");
            Console.Read();
        }
    }
}
