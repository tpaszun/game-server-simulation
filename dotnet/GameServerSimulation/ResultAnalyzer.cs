using System.Collections.Generic;
using System.Linq;
using GameServerSimulation.Probability;

namespace GameServerSimulation
{
    public class ResultAnalyzer
    {
        private List<GameStat> _stats = new List<GameStat>();
        public IEnumerable<GameStat> Stats => _stats;

        public ResultAnalyzer(
            int totalSimulationGames,
            Dictionary<string, long> simulationStats,
            IEnumerable<GameWithProbability> probabilityResults)
        {
            foreach (var prob in probabilityResults)
            {
                _stats.Add(new GameStat
                {
                    Key = prob.GameKey,
                    Probability = prob.Probability,
                    Simulation = new Fraction(simulationStats.ContainsKey(prob.GameKey) ?
                                                simulationStats[prob.GameKey] :
                                                0,
                                              totalSimulationGames)
                });
            }
        }
    }

    public class GameStat
    {
        public string Key { get; set; }
        public Fraction Probability { get; set; }
        public Fraction Simulation { get; set; }
        public Fraction Accuracy => Probability.Divide(Simulation);
    }
}
