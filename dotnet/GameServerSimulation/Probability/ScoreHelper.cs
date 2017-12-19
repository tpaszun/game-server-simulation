using System.Collections.Generic;

namespace GameServerSimulation.Calculation
{
    public static class ScoreHelper
    {
        public static int GetGameScore(this IEnumerable<Move> moves)
        {
            var score = 0;

            foreach (var move in moves)
            {
                if (move is RewardMove)
                {
                    var reward = ((RewardMove)move).Reward;
                    score += reward.Value();
                }
                else if (move is AdditionalRewardMove)
                {
                    var additionalReward = ((AdditionalRewardMove)move).AdditionalReward;
                    score += additionalReward.Value();
                }
            }

            return score;
        }
    }
}