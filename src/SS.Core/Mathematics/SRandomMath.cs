﻿using System;

namespace StardustSandbox.Core.Mathematics
{
    public static class SRandomMath
    {
        private static readonly Random _random = new();

        public static double GetDouble()
        {
            return _random.NextDouble();
        }

        public static int Range(int max)
        {
            return _random.Next(max);
        }

        public static int Range(int min, int max)
        {
            return _random.Next(min, max);
        }

        public static bool Chance(int chance, int total)
        {
            return Range(0, total) < chance;
        }
    }
}
