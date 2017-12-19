using System.Collections.Generic;
using GameServerSimulation.Calculation;
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
            var moves = new List<Move> { new RewardMove(reward) };

            Assert.Equal(reward.Value(), moves.GetGameScore());
        }

        [Theory]
        [InlineData(AdditionalReward.Twenty)]
        [InlineData(AdditionalReward.Ten)]
        [InlineData(AdditionalReward.Five)]
        [InlineData(AdditionalReward.SecondChance)]
        public void AdditionalRewardMoves(AdditionalReward additionalReward)
        {
            var moves = new List<Move> { new AdditionalRewardMove(additionalReward) };

            Assert.Equal(additionalReward.Value(), moves.GetGameScore());
        }

        [Fact]
        public void SumAllMoves()
        {
            var moves = new List<Move> {
                new RewardMove(Reward.Hundred),
                new RewardMove(Reward.Twenty),
                new RewardMove(Reward.Five),
                new RewardMove(Reward.ExtraLife),
                new RewardMove(Reward.GameOver),
                new AdditionalRewardMove(AdditionalReward.Twenty),
                new AdditionalRewardMove(AdditionalReward.Ten),
                new AdditionalRewardMove(AdditionalReward.Five),
                new AdditionalRewardMove(AdditionalReward.SecondChance)
            };

            Assert.Equal(160, moves.GetGameScore());
        }
    }
}