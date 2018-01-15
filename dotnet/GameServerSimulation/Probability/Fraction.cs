using System;

namespace GameServerSimulation.Probability
{
    public struct Fraction
    {
        public long Numerator { get; private set; }

        public long Denominator { get; private set; }

        public Fraction(long numerator, long denominator)
        {
            Numerator = numerator;
            Denominator = denominator;
            Reduce();
        }

        public Fraction Add(Fraction second)
        {
            var result = new Fraction((Numerator * second.Denominator) + (second.Numerator * Denominator), Denominator * second.Denominator);
            result.Reduce();
            return result;
        }

        public Fraction Subtract(Fraction second)
        {
            var result = new Fraction((Numerator * second.Denominator) - (second.Numerator * Denominator), Denominator * second.Denominator);
            result.Reduce();
            return result;
        }

        public Fraction Multiply(Fraction second)
        {
            var result = new Fraction(Numerator * second.Numerator, Denominator * second.Denominator);
            result.Reduce();
            return result;
        }

        public Fraction Divide(Fraction second) => Multiply(second.Reverse());

        public Fraction Reverse() => new Fraction(Denominator, Numerator);

        // public override string ToString() => $"{Numerator}/{Denominator} = {(double)Numerator / Denominator}";
        public override string ToString() => $"{Numerator}/{Denominator}";

        public double ToDouble() => (double)Numerator / Denominator;

        private void Reduce()
        {
            long utilGcd(long num1, long num2)
            {
                long tmp;
                num1 = abs(num1);
                num2 = abs(num2);
                while (num1 > 0)
                {
                    tmp = num1;
                    num1 = num2 % num1;
                    num2 = tmp;
                }
                return num2;
            }

            long abs(long x)
            {
                return x > 0 ? x : -x;
            }

            var gcd = utilGcd(Numerator, Denominator);
            Numerator /= gcd;
            Denominator /= gcd;
        }
    }
}