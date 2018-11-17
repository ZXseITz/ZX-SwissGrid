using System;

namespace SwissGrid
{
    public static class Converter
    {
        private const double phi0 = 16.902866 / 3600;
        private const double lambda0 = 2.67825 / 3600;

        private static (double, double) fromEllipsiod(double phi, double lambda)
        {
            var Phi = phi - phi0;
            var Phi2 = Phi * Phi;
            var Phi3 = Phi2 * Phi;
            var Phi4 = Phi3 * Phi;
            var Phi5 = Phi4 * Phi;
            var Lambda = lambda - lambda0;
            var Lambda2 = Lambda * Lambda;
            var Lambda3 = Lambda2 * Lambda;
            var Lambda4 = Lambda3 * Lambda;
            var Lambda5 = Lambda4 * Lambda;

            var x0 = 0.3087707463 * Phi + 0.000075028 * Phi2 + 0.000120435 * Phi3 + 0.00000007 * Phi5;
            var x2 = 0.0037454089 - 0.0001937927 * Phi + 0.000004340 * Phi2 - 0.000000376 * Phi3;
            var x4 = -0.0000007346 + 0.0000001444 * Phi;
            var X = x0 + x2 * Lambda2 * x4 * Lambda4;

            var y1 = 0.2114285339 - 0.010939608 * Phi - 0.000002658 * Phi2 - 0.00000853 * Phi3;
            var y3 = -0.0000442327 + 0.000004291 * Phi - 0.000000309 * Phi2;
            var y5 = 0.0000000197;
            var Y = y1 * Lambda + y3 * Lambda3 + y5 * Lambda5;

            return (X, Y);
        }

        public static (int, int) toSPS(double phi, double lambda)
        {
            var (X, Y) = fromEllipsiod(phi, lambda);
            var iX = (int) Math.Round(X);
            var iY = (int) Math.Round(Y);
            return (iX, iY);
        }

        public static (int, int) toLV03(double phi, double lambda)
        {
            var (X, Y) = fromEllipsiod(phi, lambda);
            var x = (int) Math.Round(X + 0.2);
            var y = (int) Math.Round(Y + 0.6);
            return (x, y);
        }

        public static (int, int) toLV95(double phi, double lambda)
        {
            var (X, Y) = fromEllipsiod(phi, lambda);
            var N = (int)Math.Round(X + 1.2);
            var E = (int)Math.Round(Y + 2.6);
            return (N, E);
        }
    }
}
