using System.Collections.Generic;
using System.Linq;

namespace GameServerSimulation.Probability
{
    public static class IEnumerableFractionExt {
        public static Fraction SumFractions(this IEnumerable<Fraction> fractions) =>
            fractions.Aggregate(new Fraction(0, 1), (f, s) => f.Add(s));

        public static Fraction MultiplyFractions(this IEnumerable<Fraction> fractions) =>
            fractions.Aggregate(new Fraction(1, 1), (f, s) => f.Multiply(s));
    }
}