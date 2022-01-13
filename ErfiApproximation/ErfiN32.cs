using MultiPrecision;

namespace ErfiApproximation {
    public static class ErfiN32 {
        public static MultiPrecision<Pow2.N32> C(MultiPrecision<Pow2.N32> x) {
            if (x.Sign == Sign.Minus) {
                return -C(-x);
            }

            if (x.IsZero) {
                return MultiPrecision<Pow2.N32>.PositiveInfinity;
            }

            if (x <= 27.5) {
                int m = (int)(68 + 18 * x);

                return Erfi<Pow2.N32>.Cfrac(x, m);
            }
            else {
                return Erfi<Pow2.N32>.Limit(x).y;
            }
        }

        public static MultiPrecision<Pow2.N32> Value(MultiPrecision<Pow2.N32> x) {
            MultiPrecision<Pow2.N32> c = C(x);

            MultiPrecision<Pow2.N32> y = 2 * MultiPrecision<Pow2.N32>.Exp(x * x)
                                      / (c * MultiPrecision<Pow2.N32>.Sqrt(MultiPrecision<Pow2.N32>.PI));

            return y;
        }
    }
}
