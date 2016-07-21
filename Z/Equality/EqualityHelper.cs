using System;
using System.Linq;

namespace Z.Equality
{
    public sealed class EqualityHelper
    {
        public static int CalculateHashCode(object[] fields)
        {
            unchecked
            {
                return fields
                    .Skip(1)
                    .Aggregate(
                        GetItemHashCode(fields.First()),
                        (current, field) => (current * 397) ^ GetItemHashCode(field));
            }
        }

        private static int GetItemHashCode(object field)
        {
            return field?.GetHashCode() ?? 0;
        }

        public static bool? CalculateReferentialEquals(object o1, object o2)
        {
            if (ReferenceEquals(null, o1)) return false;
            if (ReferenceEquals(null, o2)) return false;
            if (ReferenceEquals(o1, o2)) return true;
            if (o1.GetType() != o2.GetType()) return false;
            return null;
        }

        public static bool CalculateEquals(object[] equalityMembers1, object[] equalityMembers2)
        {
            var l1 = equalityMembers1.Length;
            var l2 = equalityMembers2.Length;
            if (l1 != l2) return false;

            var length = Math.Max(l1, l2);
            for (var index = 0; index < length; index++)
            {
                var object1 = equalityMembers1[index];
                var object2 = equalityMembers2[index];
                
                if (ItemsEqual(object1, object2) == false)
                    return false;
            }
            return true;
        }

        public static bool ItemsEqual(object object1, object object2)
        {
            if (object1 == null && object2 == null) return true;
            if (object1 == null || object2 == null) return false;
            return object1.Equals(object2);
        }
    }
}
