namespace Garaj.Utility
{
    public static  class Factory
    {
        public static string[,] CreateArray(this string value, int x, int y)
        {
            var result = new string[x, y];
            for (var i = 0; i < x; i++)
            for (var j = 0; j < y; j++)
                result[i, j] = value;
            return result;
        }

        public static T[,] CreateArray<T>(this T value, int x, int y, bool empty = true) where T : new()
        {
            var result = new T[x, y];
            for (var i = 0; i < x; i++)
            for (var j = 0; j < y; j++)
                result[i, j] = empty ? new T() : value;
            return result;
        }
        
        public static T[] CreateArray<T>(this T value, int length, bool empty = true) where T : new()
        {
            var result = new T[length];
            for (var i = 0; i < length; i++)
                result[i] = empty ? new T() : value;
            return result;
        }
        
        
        
    }
}