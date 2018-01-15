using System;
using System.Collections.Generic;
using System.Linq;
using GameServerSimulation.Calculation;

namespace GameServerSimulation.Probability
{
    public class GamesGenerator
    {
        public IEnumerable<GameWithProbability> Games => _games;

        private List<GameWithProbability> _games = new List<GameWithProbability>();

        public Fraction AverageScore => Games
                        .Select(g => g.Probability.Multiply(new Fraction(g.Moves.GetGameScore(), 1)))
                        .SumFractions();

        public void Generate()
        {
            var boxes = new Boxes(new List<Box> {
                new Box(Reward.Hundred, 1),
                new Box(Reward.Twenty, 2),
                new Box(Reward.Five, 5),
                new Box(Reward.ExtraLife, 1),
                new Box(Reward.GameOver, 3)
            });

            var additionalRewards = new List<AdditionalReward> {
                AdditionalReward.Five,
                AdditionalReward.Ten,
                AdditionalReward.Twenty,
                AdditionalReward.SecondChance
            };

            Generate(new GameWithProbability(), boxes, additionalRewards);
        }

        public void Generate(Boxes boxes, List<AdditionalReward> additionalRewards)
        {
            Generate(new GameWithProbability(), boxes, additionalRewards);
        }

        private void Generate(GameWithProbability currentGame, Boxes boxes, List<AdditionalReward> additionalRewards)
        {
            foreach (var box in boxes)
            {
                var game = new GameWithProbability(currentGame);
                var moveProbability = new Fraction(box.Count, boxes.CountBoxes());
                game.Add(new RewardMove(box.Reward, moveProbability));

                var newBoxes = new Boxes(boxes);
                newBoxes.RemoveBox(box.Reward);

                if (box.Reward == Reward.GameOver && !HasExtraLife(game.Moves))
                {
                    HandleAdditionalRewards(game, newBoxes, additionalRewards);
                    continue;
                }

                Generate(game, newBoxes, additionalRewards);
            }
        }

        private void HandleAdditionalRewards(GameWithProbability currentGame, Boxes boxes, List<AdditionalReward> additionalRewards)
        {
            foreach (var additionalReward in additionalRewards)
            {
                var game = new GameWithProbability(currentGame);
                game.Add(new AdditionalRewardMove(additionalReward, new Fraction(1, additionalRewards.Count)));

                if (additionalReward == AdditionalReward.SecondChance)
                {
                    var newAdditionalRewards = new List<AdditionalReward>(additionalRewards);
                    newAdditionalRewards.Remove(additionalReward);

                    Generate(game, boxes, newAdditionalRewards);
                }
                else
                {
                    _games.Add(game);
                }
            }
        }

        private bool HasExtraLife(IEnumerable<Move> moves)
        {
            var extraLife = moves
                .Select(m => {
                    if (m is RewardMove)
                        switch(((RewardMove)m).Reward) {
                            case Reward.ExtraLife:
                                return 1;
                            case Reward.GameOver:
                                return -1;
                        }
                    else if (m is AdditionalRewardMove)
                        switch(((AdditionalRewardMove)m).AdditionalReward) {
                            case AdditionalReward.SecondChance:
                                return 1;
                        }

                    return 0;
                })
                .Sum();

            return extraLife >= 0;
        }
    }
}