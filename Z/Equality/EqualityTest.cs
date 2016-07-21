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
            /*return EqualityComparer<T1>.Default.Equals(t1, other.t1)
                && EqualityComparer<T2>.Default.Equals(t2, other.t2)
                && EqualityComparer<T3>.Default.Equals(t3, other.t3)
                && EqualityComparer<T4>.Default.Equals(t4, other.t4)
                && EqualityComparer<T5>.Default.Equals(t5, other.t5)
                && EqualityComparer<T6>.Default.Equals(t6, other.t6)
                && a == other.a
                && b == other.b
                && c == other.c
                && Equals(d, other.d)
                && e == other.e;*/
            var maxIndex = Math.Max(equalityMembers1.Length, equalityMembers2.Length) - 1;
            for (var index = 0; index < maxIndex; index++)
            {
                var object1 = equalityMembers1[index];
                var object2 = equalityMembers2[index];
                if (Equals(object1, object2) == false)
                    return false;
            }
            return true;
        }
    }
}
