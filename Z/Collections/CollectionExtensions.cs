using System.Collections.Generic;
using System.Linq;

namespace Z.Collections
{
    public static class CollectionExtensions
    {
        public static T Pop<T>(this ICollection<T> collection)
        {
            var result = collection.First();
            collection.Remove(result);
            return result;
        }
    }
}
