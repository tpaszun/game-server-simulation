using GameServerSimulation.Calculation;
using GameServerSimulation.Probability;
using Xunit;

namespace GameServerSimulation.Tests.Probability
{
    public class GameWithProbabilityTests
    {
        [Fact]
        public void AddTest()
        {
            var game = new GameWithProbability();

            Assert.Empty(game.Moves);
            Assert.Equal(new Fraction(1, 1), game.Probability);

            var firstMove = new RewardMove(Reward.Hundred);
            game.Add(firstMove, new Fraction(1, 12));
            Assert.Equal(new Fraction(1, 12), game.Probability);
            Assert.Contains(firstMove, game.Moves);

            var secondMove = new RewardMove(Reward.Twenty);
            game.Add(secondMove, new Fraction(1, 6));
            Assert.Equal(new Fraction(1, 72), game.Probability);
            Assert.Contains(secondMove, game.Moves);
        }
    }
}