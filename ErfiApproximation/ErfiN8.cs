using MultiPrecision;

namespace ErfiApproximation {
    public static class ErfiN8 {
        public static MultiPrecision<Pow2.N8> C(MultiPrecision<Pow2.N8> x) {
            if (x.Sign == Sign.Minus) {
                return -C(-x);
            }

            if (x.IsZero) {
                return MultiPrecision<Pow2.N8>.PositiveInfinity;
            }

            if (x <= 13.5) {
                int m = (int)(20 + 9.2080 * x);

                return Erfi<Pow2.N8>.Cfrac(x, m);
            }
            else {
                return Erfi<Pow2.N8>.Limit(x).y;
            }
        }

        public static MultiPrecision<Pow2.N8> Value(MultiPrecision<Pow2.N8> x) {
            MultiPrecision<Pow2.N8> c = C(x);

            MultiPrecision<Pow2.N8> y = 2 * MultiPrecision<Pow2.N8>.Exp(x * x)
                                      / (c * MultiPrecision<Pow2.N8>.Sqrt(MultiPrecision<Pow2.N8>.PI));

            return y;
        }
    }
}
