using System.Collections.Generic;

namespace GameServerSimulation.Simulation
{
    public class Game
    {
        private Queue<Reward> _boxes;
        private Queue<AdditionalReward> _additionRewards;
        private bool _gotExtraLife;
        private int _score;

        public Game()
        {
            _boxes = Utils.ShuffledQueue(new List<Reward> {
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
            });

            _additionRewards = Utils.ShuffledQueue(new List<AdditionalReward> {
                AdditionalReward.Five,
                AdditionalReward.Ten,
                AdditionalReward.Twenty,
                AdditionalReward.SecondChance
            });
        }

        public int Play()
        {
            OpenBoxes();
            SelectAdditionalReward();

            return _score;
        }

        private void OpenBoxes()
        {
            while (_boxes.Count > 0)
            {
                var box = _boxes.Dequeue();

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
            var additionalReward = _additionRewards.Dequeue();

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
}
