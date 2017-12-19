namespace GameServerSimulation
{
    public enum Reward
    {
        Hundred,
        Twenty,
        Five,
        ExtraLife,
        GameOver
    }

    public static class RewardExt
    {
        public static int Value(this Reward reward)
        {
            switch (reward)
            {
                case Reward.Hundred:
                    return 100;
                case Reward.Twenty:
                    return 20;
                case Reward.Five:
                    return 5;
                default:
                    return 0;
            }
        }
    }
}
