using System;
using System.Collections.Generic;
using System.Linq;

namespace GameServerSimulation.Simulation
{
    public class SimulationDriver
    {
        private readonly int _gamesCount;
        private readonly List<Reward> _boxes;
        private readonly List<AdditionalReward> _additionalRewards;

        private Dictionary<string, long> _statistics = new Dictionary<string, long>();
        public Dictionary<string, long> Statistics => _statistics;

        private Dictionary<int, long> _scoreStatistics = new Dictionary<int, long>();
        public Dictionary<int, long> ScoreStatistics => _scoreStatistics;

        public SimulationDriver(int gamesCount, List<Reward> boxes, List<AdditionalReward> additionalRewards)
        {
            _gamesCount = gamesCount;
            _boxes = boxes;
            _additionalRewards = additionalRewards;
        }

        public double Run(int interval, Action<int, double> action)
        {
            long scoresSum = 0;

            for (var i = 0; i < _gamesCount; i++)
            {
                var game = new Game(_boxes, _additionalRewards);
                var score = game.Play();
                scoresSum += score;

                var key = game.ToString();
                if (!_statistics.ContainsKey(key))
                    _statistics.Add(key, 1);
                else
                    _statistics[key] += 1;

                //

                if (!_scoreStatistics.ContainsKey(score))
                    _scoreStatistics.Add(score, 1);
                else
                    _scoreStatistics[score] += 1;

                if (i % interval == 0)
                    action(i, (double)scoresSum / i);
            }

            return (double)(scoresSum) / _gamesCount;
        }
    }
}
