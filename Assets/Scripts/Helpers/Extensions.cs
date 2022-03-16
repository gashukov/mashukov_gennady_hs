using System;
using System.Collections.Generic;
using System.Linq;

namespace Helpers
{
    public static class Extensions
    {
        public static T PickRandomOther<T>(this IEnumerable<T> source, T t)
        {
            return source.PickRandom(2).First(e => !e.Equals(t));
        }
    
        public static T PickRandom<T>(this IEnumerable<T> source)
        {
            return source.PickRandom(1).Single();
        }
        public static IEnumerable<T> PickRandom<T>(this IEnumerable<T> source, int count)
        {
            return source.Shuffle().Take(count);
        }
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            return source.OrderBy(x => Guid.NewGuid());
        }
    }
}