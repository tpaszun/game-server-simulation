using System;
using System.Linq;
using GameServerSimulation.Calculation;
using GameServerSimulation.Probability;
using GameServerSimulation.Simulation;

namespace GameServerSimulation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Average score by probability");
            Console.WriteLine("============================");
            Probability();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Average score by simulation");
            Console.WriteLine("===========================");
            Simulation();
        }

        private static void Simulation()
        {
            var gamesCount = 10000000;

            long scoresSum = 0;

            for (var i = 0; i < gamesCount; i++)
            {
                var game = new Game();
                var score = game.Play();

                scoresSum += score;

                if (i % 500000 == 0) {
                    Console.WriteLine($"Played {i} games, average score: {(double)scoresSum / i}");
                }
            }

            double avg = (double)(scoresSum) / gamesCount;
            Console.WriteLine($"Average score: {avg}");
        }

        private static void Probability()
        {
            var generator = new Probability.GamesGenerator();
            generator.Generate();

            Console.WriteLine($"Total games: {generator.Games.Count()}");

            var totalProbability = generator.Games
                    .Select(g => g.Probability)
                    .SumFractions();

            var averageScore = generator.Games
                    .Select(g => g.Probability.Multiply(new Fraction(g.Moves.GetGameScore(), 1)))
                    .SumFractions();

            Console.WriteLine($"Total probability: {totalProbability} (should be 1)");
            Console.WriteLine($"Average score: {averageScore}");
        }
    }
}
