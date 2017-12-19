using System;
using System.Collections.Generic;
using System.Linq;
using GameServerSimulation.Probability;
using Xunit;

namespace GameServerSimulation.Tests.Probability
{
    public class GamesGeneratorTests
    {
        private GamesGenerator _generator = new GamesGenerator();

        [Fact]
        public void ShouldGenerate2GamesFromHundredAndGameOverReward()
        {
            _generator.Generate(new Boxes(new List<Box> {
                new Box(Reward.Hundred, 1),
                new Box(Reward.GameOver, 1)
            }), new List<AdditionalReward> {
                AdditionalReward.Twenty
            });

            Assert.All(_generator.Games, g => Assert.Equal(new Fraction(1, 2), g.Probability));
            Assert.Equal(new Fraction(1, 1), _generator.Games.Select(g => g.Probability).SumFractions());
            Assert.Equal(2, _generator.Games.Count());
        }

        [Fact]
        public void ShouldGenerate5GamesFromHundredTwentyAndGameOverReward()
        {
            _generator.Generate(new Boxes(new List<Box> {
                new Box(Reward.Hundred, 1),
                new Box(Reward.Twenty, 1),
                new Box(Reward.GameOver, 1)
            }), new List<AdditionalReward> {
                AdditionalReward.Twenty
            });

            Assert.Equal(new Fraction(1, 1), _generator.Games.Select(g => g.Probability).SumFractions());
            Assert.Equal(5, _generator.Games.Count());
        }

        [Fact]
        public void Generate6GamesFromHundredExtraLifeAndGameOverReward()
        {
            _generator.Generate(new Boxes(new List<Box> {
                new Box(Reward.Hundred, 1),
                new Box(Reward.GameOver, 2),
                new Box(Reward.ExtraLife, 1)
            }), new List<AdditionalReward> {
                AdditionalReward.Twenty
            });

            Assert.Equal(new Fraction(1, 1), _generator.Games.Select(g => g.Probability).SumFractions());
            Assert.Equal(6, _generator.Games.Count());
        }

        [Fact]
        public void ShouldGenerate18GamesFromHundredExtraLifeAndGameOverReward()
        {
            _generator.Generate(new Boxes(new List<Box> {
                new Box(Reward.Hundred, 1),
                new Box(Reward.GameOver, 3),
                new Box(Reward.ExtraLife, 1)
            }), new List<AdditionalReward> {
                AdditionalReward.Twenty,
                AdditionalReward.SecondChance
            });

            Assert.Equal(new Fraction(1, 1), _generator.Games.Select(g => g.Probability).SumFractions());
            Assert.Equal(18, _generator.Games.Count());
        }
    }

    internal static class IEnumerableFractionExt
    {
        public static Fraction SumFractions(this IEnumerable<Fraction> fractions)
        {
            return fractions.Aggregate(new Fraction(0, 1), (f, s) => s.Add(f));
        }
    }
}