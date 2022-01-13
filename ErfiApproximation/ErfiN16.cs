using MultiPrecision;

namespace ErfiApproximation {
    public static class ErfiN16 {
        public static MultiPrecision<Pow2.N16> C(MultiPrecision<Pow2.N16> x) {
            if (x.Sign == Sign.Minus) {
                return -C(-x);
            }

            if (x.IsZero) {
                return MultiPrecision<Pow2.N16>.PositiveInfinity;
            }

            if (x <= 19.5) {
                int m = (int)(37 + 12.8300 * x);

                return Erfi<Pow2.N16>.Cfrac(x, m);
            }
            else {
                return Erfi<Pow2.N16>.Limit(x).y;
            }
        }

        public static MultiPrecision<Pow2.N16> Value(MultiPrecision<Pow2.N16> x) {
            MultiPrecision<Pow2.N16> c = C(x);

            MultiPrecision<Pow2.N16> y = 2 * MultiPrecision<Pow2.N16>.Exp(x * x)
                                      / (c * MultiPrecision<Pow2.N16>.Sqrt(MultiPrecision<Pow2.N16>.PI));

            return y;
        }
    }
}
