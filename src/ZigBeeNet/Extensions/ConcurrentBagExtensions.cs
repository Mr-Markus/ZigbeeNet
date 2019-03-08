using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ZigBeeNet.Extensions
{
    public static class ConcurrentBagExtensions
    {
        public static void AddRange<T>(this ConcurrentBag<T> bag, IEnumerable<T> toAdd)
        {
            foreach (var element in toAdd)
            {
                bag.Add(element);
            }
        }

        public static void Clear<T>(this ConcurrentBag<T> bag)
        {
            Interlocked.Exchange(ref bag, new ConcurrentBag<T>());
        }

    }
}
