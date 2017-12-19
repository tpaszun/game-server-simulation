namespace GameServerSimulation.Calculation
{
    public abstract class Move { }

    public class RewardMove : Move
    {
        public Reward Reward { get; private set; }

        public RewardMove(Reward reward)
        {
            Reward = reward;
        }
    }

    public class AdditionalRewardMove : Move
    {
        public AdditionalReward AdditionalReward { get; private set; }

        public AdditionalRewardMove(AdditionalReward additionalReward)
        {
            AdditionalReward = additionalReward;
        }
    }
}