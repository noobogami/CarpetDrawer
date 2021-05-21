using System;

namespace Garaj.Utility{
    [Serializable]
    public struct RangeFloat{
        public float Min;
        public float Max;
    }

    [Serializable]
    public struct RangeInt{
        public int Min;
        public int Max;
    }
}