using System;
using System.Collections.Generic;
using System.Linq;

namespace GameServerSimulation.Simulation
{
    public class Game
    {
        private Queue<Reward> _boxes;
        private Queue<AdditionalReward> _additionRewards;
        private bool _gotExtraLife;
        private int _score;
        private List<GameMove> _moves;

        public int Score => _score;

        public List<GameMove> Moves => _moves;

        public Game() : this(new List<Reward> {
                Reward.Hundred,
                Reward.Twenty,
                Reward.Twenty,
                Reward.Five,
                Reward.Five,
                Reward.Five,
                Reward.Five,
                Reward.Five,
                Reward.ExtraLife,
                Reward.GameOver,
                Reward.GameOver,
                Reward.GameOver
            }, new List<AdditionalReward> {
                AdditionalReward.Five,
                AdditionalReward.Ten,
                AdditionalReward.Twenty,
                AdditionalReward.SecondChance
            })
        { }

        public Game(List<Reward> boxes, List<AdditionalReward> additionalRewards)
        {
            _boxes = Utils.ShuffledQueue(boxes);
            _additionRewards = Utils.ShuffledQueue(additionalRewards);
            _moves = new List<GameMove>();
        }

        public int Play()
        {
            OpenBoxes();
            SelectAdditionalReward();

            return _score;
        }

        public override string ToString()
        {
            var movesStrings = _moves.Select(move => move.ToString());
            var moves = string.Join(", ", movesStrings);
            return $"Score: {Score}, Moves: {moves}";
        }

        private void OpenBoxes()
        {
            while (_boxes.Count > 0)
            {
                var box = _boxes.Dequeue();
                _moves.Add(new RewardGameMove(box));

                if (box == Reward.GameOver)
                {
                    if (_gotExtraLife)
                    {
                        _gotExtraLife = false;
                        continue;
                    }
                    else
                        break;
                }

                switch (box)
                {
                    case Reward.Hundred:
                    case Reward.Twenty:
                    case Reward.Five:
                        _score += box.Value();
                        break;
                    case Reward.ExtraLife:
                        _gotExtraLife = true;
                        break;
                }
            }
        }

        private void SelectAdditionalReward()
        {
            if (_additionRewards.Count == 0)
                return;

            var additionalReward = _additionRewards.Dequeue();
            _moves.Add(new AdditionalRewardGameMove(additionalReward));

            switch (additionalReward)
            {
                case AdditionalReward.Five:
                case AdditionalReward.Ten:
                case AdditionalReward.Twenty:
                    _score += additionalReward.Value();
                    break;
                case AdditionalReward.SecondChance:
                    Play();
                    break;
            }
        }
    }

    public class GameMove
    {

    }

    public class RewardGameMove : GameMove
    {
        public Reward Reward { get; private set; }

        public RewardGameMove(Reward reward)
        {
            Reward = reward;
        }

        public override string ToString() => Reward.ToString();
    }

    public class AdditionalRewardGameMove : GameMove
    {
        public AdditionalReward AdditionalReward { get; private set; }

        public AdditionalRewardGameMove(AdditionalReward additionalReward)
        {
            AdditionalReward = additionalReward;
        }

        public override string ToString() => AdditionalReward.ToString();
    }
}
