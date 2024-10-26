using MultiPrecision;

namespace ErfiApproximation {
    public static class ErfiN4 {
        public static MultiPrecision<Pow2.N4> C(MultiPrecision<Pow2.N4> x) {
            if (x.Sign == Sign.Minus) {
                return -C(-x);
            }

            if (MultiPrecision<Pow2.N4>.IsZero(x)) {
                return MultiPrecision<Pow2.N4>.PositiveInfinity;
            }

            if (x <= 9.5) {
                int m = (int)(10 + 6.6709 * x);

                return Erfi<Pow2.N4>.Cfrac(x, m);
            }
            else {
                return Erfi<Pow2.N4>.Limit(x).y;
            }
        }

        public static MultiPrecision<Pow2.N4> Value(MultiPrecision<Pow2.N4> x) {
            MultiPrecision<Pow2.N4> c = C(x);

            MultiPrecision<Pow2.N4> y = 2 * MultiPrecision<Pow2.N4>.Exp(x * x)
                                      / (c * MultiPrecision<Pow2.N4>.Sqrt(MultiPrecision<Pow2.N4>.PI));

            return y;
        }
    }
}
