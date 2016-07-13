using System.Collections.Generic;

namespace Z.Sets
{
    // TODO Unit test
    public static class SetExtensions
    {
        public static ISet<T> Difference<T>(this IEnumerable<T> baseSet, IEnumerable<T> setToSubstract)
        {
            var result = new HashSet<T>(baseSet);
            foreach (var element in setToSubstract)
                result.Remove(element);
            return result;
        }
    }
}