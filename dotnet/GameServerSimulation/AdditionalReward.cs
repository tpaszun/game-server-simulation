namespace GameServerSimulation
{
    public enum AdditionalReward
    {
        Five,
        Ten,
        Twenty,
        SecondChance
    }

    public static class AdditionalRewardExt
    {
        public static int Value(this AdditionalReward additionReward)
        {
            switch (additionReward)
            {
                case AdditionalReward.Twenty:
                    return 20;
                case AdditionalReward.Ten:
                    return 10;
                case AdditionalReward.Five:
                    return 5;
                default:
                    return 0;
            }
        }
    }
}
