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

        public void Add(Move move)
        {
            _moves.Add(move);
            Probability = Probability.Multiply(move.Probability);
        }

        public override string ToString()
        {
            var movesStrings = _moves.Select(move => move.ToString());
            var moves = string.Join(", ", movesStrings);
            return $"Probability: {Probability}, Score: {Moves.GetGameScore()}, Moves: {moves}";
        }

        public string GameKey
        {
            get
            {
                var movesStrings = _moves.Select(move => move.MoveReward);
                var moves = string.Join(", ", movesStrings);
                return $"Score: {Moves.GetGameScore()}, Moves: {moves}";
            }
        }
    }
}