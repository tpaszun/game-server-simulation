using System;
using GameServerSimulation.Probability;
using Xunit;

namespace GameServerSimulation.Tests.Probability
{
    public class FractionTests
    {
        [Fact]
        public void ReductionOnCreation()
        {
            var x = new Fraction(3, 6);

            Assert.Equal(new Fraction(1, 2), x);
        }

        [Fact]
        public void Addition()
        {
            var x = new Fraction(1, 2);
            var y = x.Add(new Fraction(1, 3));

            Assert.Equal(new Fraction(5, 6), y);
        }

        [Fact]
        public void AdditionIdentity()
        {
            var x = new Fraction(1, 2);
            var y = x.Add(new Fraction(0, 1));

            Assert.Equal(new Fraction(1, 2), y);

            var z = x.Add(new Fraction(0, 2));
            Assert.Equal(new Fraction(1, 2), z);
        }

        [Fact]
        public void Multiplication()
        {
            var x = new Fraction(1, 2);
            var y = x.Multiply(new Fraction(1, 3));

            Assert.Equal(new Fraction(1, 6), y);
        }

        [Fact]
        public void MultiplicationIdentity()
        {
            var x = new Fraction(1, 2);
            var y = x.Multiply(new Fraction(1, 1));

            Assert.Equal(new Fraction(1, 2), y);

            var z = x.Multiply(new Fraction(2, 2));

            Assert.Equal(new Fraction(1, 2), z);
        }

        [Fact]
        public void Reverse()
        {
            var x = new Fraction(2, 3);

            Assert.Equal(new Fraction(3, 2), x.Reverse());
        }

        [Fact]
        public void Division()
        {
            var x = new Fraction(3, 5);

            Assert.Equal(new Fraction(3, 2), x.Divide(new Fraction(2, 5)));
        }

        [Fact]
        public void ZeroReduction()
        {
            var x = new Fraction(0, 3);
            Assert.Equal(0, x.Numerator);
            Assert.Equal(1, x.Denominator);
        }
    }
}
