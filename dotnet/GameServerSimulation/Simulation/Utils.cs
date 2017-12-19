using System;
using System.Collections.Generic;

namespace GameServerSimulation.Simulation
{
    internal static class Utils
    {
        public static Queue<T> ShuffledQueue<T>(List<T> list)
        {
            var shuffled = new Queue<T>(list.Count);

            for (var i = list.Count; i > 0; i--)
            {
                var randomElementIdx = StrongRandom.GetNext() % i;
                shuffled.Enqueue(list[randomElementIdx]);
                list.RemoveAt(randomElementIdx);
            }

            return shuffled;
        }
    }
}
