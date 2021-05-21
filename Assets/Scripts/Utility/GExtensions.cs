using System;

namespace Extensions
{
    public static class GExtensions
    {
        public static T ToEnum<T>(this string value)
        {
            try
            {
                return (T) Enum.Parse(typeof(T), value, true);
            }
            catch (Exception e)
            {
                throw new Exception("Item not found");
            }
        }
   
    }
}