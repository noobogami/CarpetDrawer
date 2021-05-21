using System;

namespace Garaj.Utility{
    public static class RandomEx{
        private static readonly System.Random R = new System.Random();
        public static int Random(int max) => R.Next(max);

        // ReSharper disable once InconsistentNaming
        public static string GetUniqID() => Guid.NewGuid().ToString("N");

        public static float RandomRange(this (float, float) rangeFloat) {
            return UnityEngine.Random.Range(rangeFloat.Item1, rangeFloat.Item2);
        }

        public static float RandomRange(this RangeFloat rangeFloat) {
            return UnityEngine.Random.Range(rangeFloat.Min, rangeFloat.Max);
        }

        public static int RandomRange(this (int, int) rangeFloat) {
            return UnityEngine.Random.Range(rangeFloat.Item1, rangeFloat.Item2);
        }

        public static int RandomRange(this RangeInt rangeInt) {
            return UnityEngine.Random.Range(rangeInt.Min, rangeInt.Max);
        }
    }
}