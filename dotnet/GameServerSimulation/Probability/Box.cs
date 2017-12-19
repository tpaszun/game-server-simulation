namespace GameServerSimulation.Probability
{
    public class Box
    {
        public int Count { get; internal set; }

        public Reward Reward { get; private set; }

        public Box(Reward reward, int count)
        {
            Reward = reward;
            Count = count;
        }

        public Box(Box other)
        {
            Reward = other.Reward;
            Count = other.Count;
        }

        public override string ToString() => $"{Reward} {Count}";
    }
}