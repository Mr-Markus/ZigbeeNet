using System;

namespace ZigbeeNet.CC
{
    public static class ZpiObjectExtensions
    {
        public static T ToSpecificObject<T>(this ZpiObject zpiObject) where T : ZpiObject
        {
            return (T)Activator.CreateInstance(typeof(T), zpiObject);
        }
    }
}
