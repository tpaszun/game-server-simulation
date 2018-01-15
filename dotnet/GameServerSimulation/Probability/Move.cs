using GameServerSimulation.Probability;

namespace GameServerSimulation.Calculation
{
    public abstract class Move
    {
        public Fraction Probability { get; private set; }

        public Move(Fraction probability)
        {
            Probability = probability;
        }

        public abstract string MoveReward { get; }
    }

    public class RewardMove : Move
    {
        public Reward Reward { get; private set; }

        public RewardMove(Reward reward, Fraction probability) : base(probability)
        {
            Reward = reward;
        }

        public override string ToString() => $"{Reward.ToString()} {Probability}";

        public override string MoveReward => Reward.ToString();
    }

    public class AdditionalRewardMove : Move
    {
        public AdditionalReward AdditionalReward { get; private set; }

        public AdditionalRewardMove(AdditionalReward additionalReward, Fraction probability) : base(probability)
        {
            AdditionalReward = additionalReward;
        }

        public override string ToString() => $"{AdditionalReward.ToString()} {Probability}";

        public override string MoveReward => AdditionalReward.ToString();
    }
}