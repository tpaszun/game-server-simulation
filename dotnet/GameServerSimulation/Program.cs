using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GameServerSimulation.Calculation;
using GameServerSimulation.Probability;
using GameServerSimulation.Simulation;
using Newtonsoft.Json;

namespace GameServerSimulation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Average score by probability");
            Console.WriteLine("============================");
            var probabilityResults = Probability();
            Console.WriteLine();
            Console.WriteLine();
            var gamesCount = 5_000_000;
            Console.WriteLine("Average score by simulation");
            Console.WriteLine("===========================");

            var simulationScoreStats = ScoreSimulation(gamesCount);

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Check simulation results with probability calculation");
            Console.WriteLine("=====================================================");
            var scoreAnalyzer = new ScoreAnalyzer(gamesCount, simulationScoreStats, probabilityResults);
            var maxError = scoreAnalyzer.Stats.Select(s => s.Error).Max();

            Console.WriteLine($"Maximal error: {maxError}%");
        }

        private static Dictionary<int, long> ScoreSimulation(int gamesCount)
        {
            var rewards = new List<Reward> {
                    Reward.Hundred,
                    Reward.Twenty,
                    Reward.Twenty,
                    Reward.Five,
                    Reward.Five,
                    Reward.Five,
                    Reward.Five,
                    Reward.Five,
                    Reward.GameOver,
                    Reward.GameOver,
                    Reward.GameOver,
                    Reward.ExtraLife
                };
            var additionalRewards = new List<AdditionalReward> {
                    AdditionalReward.Twenty,
                    AdditionalReward.Ten,
                    AdditionalReward.Five,
                    AdditionalReward.SecondChance
                };

            var simulationDriver = new SimulationDriver(gamesCount, rewards, additionalRewards);
            double average = simulationDriver.Run(500_000, (i, avg) => Console.WriteLine($"Played {i} games, average score: {avg}"));
            Console.WriteLine($"Average score: {average}");
            return simulationDriver.ScoreStatistics;
        }

        private static IEnumerable<GameWithProbability> Probability()
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
            Console.WriteLine($"Average score: {averageScore} = {averageScore.ToDouble()}");

            return generator.Games;
        }
    }
}
