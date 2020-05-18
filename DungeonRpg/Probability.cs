using System;

namespace DungeonRpg
{
    public static class Probability
    {
        private static Random Random = new Random();

        /// <summary>
        /// Returns an integer between a and b on a closed interval [a, b].
        /// </summary> 
        /// <param name="a">Min</param>
        /// <param name="b">Max</param>
        /// <returns></returns>
        public static int Uniform(int a, int b)
            => Random.Next(a, b + 1);
    }
}
