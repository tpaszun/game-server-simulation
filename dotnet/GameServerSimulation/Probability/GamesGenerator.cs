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
                game.Add(new RewardMove(box.Reward), moveProbability);

                var newBoxes = new Boxes(boxes);
                newBoxes.RemoveBox(box.Reward);

                if (box.Reward == Reward.GameOver && !HasExtraLife(game.Moves))
                {
                    HandleAdditionalRewards(game, boxes, additionalRewards);
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
                game.Add(new AdditionalRewardMove(additionalReward), new Fraction(1, additionalRewards.Count));

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
            var rewards = moves
                .Where(m => m is RewardMove)
                .Cast<RewardMove>()
                .Select(rm => rm.Reward);

            return rewards.Contains(Reward.ExtraLife) && rewards.Count(r => r == Reward.GameOver) < 2;
        }
    }
}