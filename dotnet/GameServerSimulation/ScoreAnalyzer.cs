using System.Collections.Generic;
using System.Linq;
using GameServerSimulation.Calculation;
using GameServerSimulation.Probability;

namespace GameServerSimulation
{
    public class ScoreAnalyzer
    {
        private List<ScoreStats> _stats = new List<ScoreStats>();
        public IEnumerable<ScoreStats> Stats => _stats.OrderBy(s => s.Error).ToList();

        public ScoreAnalyzer(
            int totalSimulationGames,
            Dictionary<int, long> simulationScoreStats,
            IEnumerable<GameWithProbability> probabilityResults)
        {
            var pbs = probabilityResults
                        .GroupBy(g => g.Moves.GetGameScore())
                        .Select(g => new
                        {
                            Score = g.Key,
                            Probability = g.Select(game => game.Probability).SumFractions()
                        });

            foreach (var prob in pbs)
            {
                _stats.Add(new ScoreStats
                {
                    Score = prob.Score,
                    Probability = prob.Probability,
                    Simulation = new Fraction(simulationScoreStats.ContainsKey(prob.Score) ?
                                                simulationScoreStats[prob.Score] :
                                                0,
                                              totalSimulationGames),
                    SimulationCount = simulationScoreStats[prob.Score],
                    ProbableCount = (long)prob.Probability.Multiply(new Fraction(totalSimulationGames, 1)).ToDouble()
                });
            }

            var sum = _stats.Select(s => s.Simulation).SumFractions();
            System.Console.WriteLine($"Simulation sum: {sum}");
        }
    }

    public class ScoreStats
    {
        public int Score { get; set; }
        public Fraction Probability { get; set; }
        public Fraction Simulation { get; set; }
        public long SimulationCount { get; set; }
        public long ProbableCount { get; set; }
        public long Difference => SimulationCount - ProbableCount;
        public double Error => Abs(new Fraction(Difference, ProbableCount).ToDouble() * 100);

        private double Abs(double number) => number < 0 ? -number : number;
    }
}
