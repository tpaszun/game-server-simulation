using System.Collections.Generic;
using GameServerSimulation.Calculation;
using GameServerSimulation.Probability;
using Xunit;

namespace GameServerSimulation.Tests.Probability
{
    public class ScoreHelperTests
    {
        [Theory]
        [InlineData(Reward.Hundred)]
        [InlineData(Reward.Twenty)]
        [InlineData(Reward.Five)]
        [InlineData(Reward.ExtraLife)]
        [InlineData(Reward.GameOver)]
        public void RewardMoves(Reward reward)
        {
            var moves = new List<Move> { new RewardMove(reward, new Fraction(1, 1)) };

            Assert.Equal(reward.Value(), moves.GetGameScore());
        }

        [Theory]
        [InlineData(AdditionalReward.Twenty)]
        [InlineData(AdditionalReward.Ten)]
        [InlineData(AdditionalReward.Five)]
        [InlineData(AdditionalReward.SecondChance)]
        public void AdditionalRewardMoves(AdditionalReward additionalReward)
        {
            var moves = new List<Move> { new AdditionalRewardMove(additionalReward, new Fraction(1, 1)) };

            Assert.Equal(additionalReward.Value(), moves.GetGameScore());
        }

        [Fact]
        public void SumAllMoves()
        {
            var moves = new List<Move> {
                new RewardMove(Reward.Hundred, new Fraction(1, 1)),
                new RewardMove(Reward.Twenty, new Fraction(1, 1)),
                new RewardMove(Reward.Five, new Fraction(1, 1)),
                new RewardMove(Reward.ExtraLife, new Fraction(1, 1)),
                new RewardMove(Reward.GameOver, new Fraction(1, 1)),
                new AdditionalRewardMove(AdditionalReward.Twenty, new Fraction(1, 1)),
                new AdditionalRewardMove(AdditionalReward.Ten, new Fraction(1, 1)),
                new AdditionalRewardMove(AdditionalReward.Five, new Fraction(1, 1)),
                new AdditionalRewardMove(AdditionalReward.SecondChance, new Fraction(1, 1))
            };

            Assert.Equal(160, moves.GetGameScore());
        }
    }
}