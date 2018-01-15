using GameServerSimulation.Simulation;
using Xunit;

namespace GameServerSimulation.Tests.Simulation
{
    public class GameTests
    {
        [Fact]
        public void PlayThousandGames()
        {
            for (var i = 0; i < 1000; i++)
            {
                var game = new Game();
                var score = game.Play();

                Assert.InRange(score, 0, 185);
            }
        }

    }
}