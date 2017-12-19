using System.Collections.Generic;
using System.Linq;
using GameServerSimulation.Calculation;

namespace GameServerSimulation.Probability
{
    public class GameWithProbability
    {
        public Fraction Probability { get; private set; }

        public IEnumerable<Move> Moves => _moves;

        private List<Move> _moves;

        public GameWithProbability()
        {
            Probability = new Fraction(1, 1);
            _moves = new List<Move>();
        }

        public GameWithProbability(GameWithProbability game)
        {
            Probability = game.Probability;
            _moves = new List<Move>(game.Moves);
        }

        public void Add(Move move, Fraction probability)
        {
            _moves.Add(move);
            Probability = Probability.Multiply(probability);
        }

        public override string ToString()
        {
            var movesStrings = _moves.Select(move =>
            {
                if (move is RewardMove)
                    return ((RewardMove)move).Reward.ToString();
                else
                    return ((AdditionalRewardMove)move).AdditionalReward.ToString();
            });
            var moves = string.Join(", ", movesStrings);
            return $"Probability: {Probability}, Moves: {moves}";
        }
    }
}