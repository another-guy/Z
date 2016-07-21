using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Z.Equality;

namespace Z.Tests.Equality
{
    public class EqualityTest
    {
        const string @string = "test";
        private readonly SampleClass<string> sample = new SampleClass<string>(@string);

        [Fact]
        public void ReturnsSameHashCodesForSameObjects()
        {
            // Arrange
            var baseHashCode = EqualityHelper.CalculateHashCode(sample.EqualityMembers);
            foreach (var objectWithSameHashCode in Same)
            {
                // Act
                var sameHashCode = EqualityHelper.CalculateHashCode(objectWithSameHashCode.EqualityMembers);

                // Assert
                Assert.Equal(baseHashCode, sameHashCode);
            }
        }

        [Fact]
        public void ReturnsDifferentHashCodesForDifferentObjects()
        {
            var baseHashCode = EqualityHelper.CalculateHashCode(sample.EqualityMembers);
            foreach (var objectWithNotSameHashCode in NotSame)
            {
                // Act
                var sameHashCode = EqualityHelper.CalculateHashCode(objectWithNotSameHashCode.EqualityMembers);

                // Assert
                Assert.NotEqual(baseHashCode, sameHashCode);
            }
        }

        [Fact]
        public void ReferenceEqualityCorrectForSameObject()
        {
            Assert.True(EqualityHelper.CalculateReferentialEquals(sample, sample));
        }

        [Fact]
        public void ReferenceEqualityCorrectForDifferentObjects()
        {
            Assert.False(EqualityHelper.CalculateReferentialEquals(null, sample));
            Assert.False(EqualityHelper.CalculateReferentialEquals(sample, null));
            Assert.False(EqualityHelper.CalculateReferentialEquals(sample, 123));
            Assert.False(EqualityHelper.CalculateReferentialEquals(123, sample));
        }

        // TODO Test CalculateEquals

        [Fact]
        public void ValueEqualityCorrectForSameObject()
        {
            // Arrange
            foreach (var equalObject in Same)
            {
                // Act
                var areEqual = EqualityHelper.CalculateEquals(sample.EqualityMembers, equalObject.EqualityMembers);

                // Assert
                Assert.True(areEqual);
            }
        }

        [Fact]
        public void ValueEqualityCorrectForDifferentObjects()
        {
            // Arrange
            foreach (var notEqualObject in NotSame)
            {
                // Act
                var areEqual = EqualityHelper.CalculateEquals(sample.EqualityMembers, notEqualObject.EqualityMembers);

                // Assert
                Assert.False(areEqual);
            }
        }

        private List<SampleClass<string>> Same =>
            new List<SampleClass<string>>
            {
                sample,
                new SampleClass<string>(@string)
            };

        private static List<SampleClass<string>> NotSame =>
            new List<SampleClass<string>>
            {
                new SampleClass<string>("other"), ????
                new SampleClass<string>(@string) { I = 35 },
                new SampleClass<string>(@string) { DC = 234 },
                new SampleClass<string>(@string) { B = true },
                new SampleClass<string>(@string) { O = new object() },
                new SampleClass<string>(@string) { C = 'z' },
                new SampleClass<string>(@string) { BT = 253 },
                new SampleClass<string>(@string) { DB = 0.32434d },
                new SampleClass<string>(@string) { F = 35.213f },
                new SampleClass<string>(@string) { L = 32345L },
            };
    }

    public class SampleClass<T1>
    {
        public T1 T { get; set; }
        public int I { get; set; }
        public decimal DC { get; set; }
        public bool B { get; set; }
        public object O { get; set; }
        public char C { get; set; }
        public byte BT { get; set; }
        public double DB { get; set; }
        public float F { get; set; }
        public long L { get; set; }

        public SampleClass(T1 t)
        {
            T = t;
            I = int.MaxValue;
            DC = decimal.MaxValue;
            B = false;
            O = null;
            C = 'a';
            BT = byte.MaxValue;
            DB = double.MaxValue;
            F = float.MaxValue;
            L = long.MaxValue;
        }

        protected bool Equals(SampleClass<T1> other)
        {
            return EqualityHelper.CalculateEquals(this.EqualityMembers, other.EqualityMembers);
        }

        public override bool Equals(object other)
        {
            var referenceEqualityResult = EqualityHelper.CalculateReferentialEquals(this, other);
            return referenceEqualityResult ??
                EqualityHelper.CalculateEquals(this.EqualityMembers, ((SampleClass<T1>)other).EqualityMembers);
        }

        public override int GetHashCode()
        {
            return EqualityHelper.CalculateHashCode(EqualityMembers);
        }

        public object[] EqualityMembers => new[] { this.T, I, DC, B, O, C, BT, DB, F, L };
    }
}
