using System;
using System.Collections.Generic;
using System.Linq;

namespace GameServerSimulation.Simulation
{
    internal static class Utils
    {
        public static Queue<T> ShuffledQueue<T>(List<T> list)
        {
            return new Queue<T>(list.Shuffle());
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> sequence)
        {
            if (sequence == null) throw new ArgumentNullException("sequence");

            return ShuffleImpl(sequence);
        }

        private static Random _random = new Random();

        private static IEnumerable<T> ShuffleImpl<T>(IEnumerable<T> sequence)
        {
            var buffer = sequence.ToArray();

            for (int max = buffer.Length - 1; max > 0; max--)
            {
                yield return ExtractRandomItem(buffer, max);
            }

            yield return (buffer[0]);
        }

        private static T ExtractRandomItem<T>(T[] buffer, int max)
        {
            int random = GetRandomNumber(max + 1);
            SwapOut(buffer, random, max);
            return buffer[max];
        }

        private static int GetRandomNumber(int max)
        {
            lock (_random)
            {
                return _random.Next(max);
            }
        }

        private static void SwapOut<T>(T[] buffer, int randomItem, int swapItem)
        {
            if (randomItem != swapItem)
            {
                T temp = buffer[swapItem];
                buffer[swapItem] = buffer[randomItem];
                buffer[randomItem] = temp;
            }
        }
    }
}
