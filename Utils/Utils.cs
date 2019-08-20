using System;
using System.Collections.Generic;
using System.Text;

namespace MeowType.Chaoite.Utils
{
    public static class Utils
    {
        public static IEnumerable<T> PopFor<T>(this Stack<T> self)
        {
            while (self.TryPop(out var outv))
            {
                yield return outv;
            }
        }
    }
}
