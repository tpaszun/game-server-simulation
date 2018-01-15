using System;
using System.Collections.Generic;
using System.Linq;
using GameServerSimulation.Calculation;
using GameServerSimulation.Probability;
using GameServerSimulation.Simulation;
using Xunit;

namespace GameServerSimulation.Tests.Simulation
{
    public class SimulationDriverTests
    {
        [Fact]
        public void PlayGameWithHundredRewardAndGameOver()
        {
            var boxes = new List<Reward> {
                    Reward.Hundred,
                    Reward.GameOver
                };
            var additionalRewards = new List<AdditionalReward> {
                AdditionalReward.Five
            };
            var simulationDriver = new SimulationDriver(100_000, boxes, additionalRewards);

            var avg = simulationDriver.Run();

            Assert.Equal(55, avg, 0);

            var gamesGenerator = new GamesGenerator();
            gamesGenerator.Generate(RewardListToBoxes(boxes), additionalRewards);

            Assert.Equal(55, gamesGenerator.AverageScore.ToDouble(), 0);
        }

        [Fact]
        public void PlayGameWithTwoHundredRewardsAndGameOver()
        {
            var boxes = new List<Reward> {
                    Reward.Hundred,
                    Reward.Hundred,
                    Reward.GameOver
                };
            var additionalRewards = new List<AdditionalReward> {
                AdditionalReward.Five
            };

            Test(boxes, additionalRewards);
        }

        [Fact]
        public void PlayGameWithHundredAndTwentyAndGameOverRewardsAndTwentyAndFiveAdditionalRewards()
        {
            var boxes = new List<Reward> {
                    Reward.Hundred,
                    Reward.Twenty,
                    Reward.GameOver
                };
            var additionalRewards = new List<AdditionalReward> {
                AdditionalReward.Twenty,
                AdditionalReward.Five
            };

            Test(boxes, additionalRewards);
        }

        [Fact]
        public void PlayGameWithSecondChance()
        {
            var boxes = new List<Reward> {
                    Reward.Hundred,
                    Reward.Twenty,
                    Reward.GameOver,
                    Reward.GameOver
                };
            var additionalRewards = new List<AdditionalReward> {
                AdditionalReward.Twenty,
                AdditionalReward.SecondChance
            };

            Test(boxes, additionalRewards);
        }

        [Fact]
        public void PlayGameWithExtraLife()
        {
            var boxes = new List<Reward> {
                    Reward.Hundred,
                    Reward.GameOver,
                    Reward.GameOver,
                    Reward.ExtraLife
                };
            var additionalRewards = new List<AdditionalReward> {
                AdditionalReward.Twenty
            };

            Test(boxes, additionalRewards);
        }

        [Fact]
        public void PlayGameWithExtraLifeAndSecondChance()
        {
            var boxes = new List<Reward> {
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

            Test(boxes, additionalRewards);
        }

        private void Test(List<Reward> boxes, List<AdditionalReward> additionalRewards)
        {
            var simulationDriver = new SimulationDriver(50_000_000, boxes, additionalRewards);

            var avg = simulationDriver.Run();

            var gamesGenerator = new GamesGenerator();
            gamesGenerator.Generate(RewardListToBoxes(boxes), additionalRewards);

            foreach (var game in gamesGenerator.Games
                .OrderBy(s => s.GameKey))
            {
                // System.Console.WriteLine($"{game.GameKey}, Probability: {game.Probability} ({game.Probability.ToDouble()})");
            }

            System.Console.WriteLine();

            // var stats = simulationDriver.Stats();

            var stat = gamesGenerator.Games.Select(g => g.GameKey)
                .Union(simulationDriver.Statistics.Keys)
                .ToHashSet()
                .Select(k => new Stat
                {
                    GameKey = k
                })
                .ToDictionary(s => s.GameKey);

            foreach (var s in simulationDriver.Statistics)
                stat[s.Key].Count = s.Value;

            foreach (var game in gamesGenerator.Games)
                stat[game.GameKey].Probability = game.Probability;

            System.Console.WriteLine("Stats:");

            // foreach (var s in stat)
            // {
            //     if (!s.Value.Probability.HasValue)
            //     {
            //         Console.WriteLine($"{s.Value.GameKey}  Count: {s.Value.Count} ({(double)s.Value.Count / simulationDriver.Games.Count})");
            //     }
            //     else if (!s.Value.Count.HasValue)
            //     {
            //         Console.WriteLine($"{s.Value.GameKey}  Probability: {s.Value.Probability} ({s.Value.Probability?.ToDouble()})");
            //     }
            //     else
            //     {
            //         // var prob = s.Value.Probability?.ToDouble();
            //         // var sim = (double)s.Value.Count / simulationDriver.Games.Count;
            //         // Console.WriteLine($"{s.Value.GameKey}  Probability: {s.Value.Probability} ({s.Value.Probability?.ToDouble()}), Count: {s.Value.Count} ({(double)s.Value.Count / simulationDriver.Games.Count})");
            //     }
            // }
            System.Console.WriteLine($"All: {stat.Count()}");
            System.Console.WriteLine($"All simulation games: {simulationDriver.Statistics.Values.Sum()}");
            System.Console.WriteLine($"No probability: {stat.Where(x => !x.Value.Probability.HasValue).Count()}");
            System.Console.WriteLine($"No simulation: {stat.Where(x => !x.Value.Count.HasValue).Count()}");

            foreach(var g in stat.Where(x => !x.Value.Count.HasValue))
                System.Console.WriteLine(g.Key);

            System.Console.WriteLine($"Probability: {gamesGenerator.AverageScore.ToDouble()}");
            System.Console.WriteLine($"Simulation: {avg}");

            Assert.InRange(avg - gamesGenerator.AverageScore.ToDouble(), -0.5, 0.5);
        }

        private static Boxes RewardListToBoxes(List<Reward> rewards) =>
            new Boxes(rewards
                .GroupBy(reward => reward)
                .Select((grouping) => new Box(grouping.Key, grouping.Count()))
                .ToList());
    }

    public class Stat
    {
        public string GameKey { get; set; }
        public Fraction? Probability { get; set; }
        public long? Count { get; set; }
    }
}