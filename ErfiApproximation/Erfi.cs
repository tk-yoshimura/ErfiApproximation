using MultiPrecision;

namespace ErfiApproximation {
    public static class Erfi<N> where N : struct, IConstant {
        public static MultiPrecision<N> Cfrac(MultiPrecision<N> x, int m){
            if (x.IsZero) {
                return MultiPrecision<N>.Zero;
            }

            MultiPrecision<N> w = x * x;

            MultiPrecision<N> f = 1;

            for (int n = m; n >= 0; n--) {
                f = (4 * n + 1) + (4 * n + 2) * w / ((4 * n + 3) - (4 * n + 4) * w / f);
            }

            MultiPrecision<N> y = f / x;

            return y;
        }

        public static MultiPrecision<N> Value(MultiPrecision<N> x, int m) {
            MultiPrecision<N> c = Cfrac(x, m);

            MultiPrecision<N> y = 2 * MultiPrecision<N>.Exp(x * x) / (c * MultiPrecision<N>.Sqrt(MultiPrecision<N>.PI));

            return y;
        }

        public static (MultiPrecision<N> y, int m) Convergence(MultiPrecision<N> x, int max_m = 1024, int convchecks = 4) {
            MultiPrecision<N> prev_y = Cfrac(x, 0);
            
            for (int m = 1, convtimes = 0; m <= max_m; m++) {
                MultiPrecision<N> y = Cfrac(x, m);

                MultiPrecision<N> err = y - prev_y;

                if (err.IsZero || err.Exponent <= y.Exponent - MultiPrecision<N>.Bits + 1) {
                    convtimes++;
                }
                else {
                    convtimes = 0;
                }
                if (convtimes > convchecks) {
                    return (y, m - convchecks);
                }

                prev_y = y;
            }

            return (MultiPrecision<N>.NaN, int.MaxValue);
        }
    }
}
