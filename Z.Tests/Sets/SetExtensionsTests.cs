using System.Collections.Generic;
using Xunit;
using Z.Sets;

namespace Z.Tests.Sets
{
    public class SetExtensionsTests
    {
        [Theory, MemberData(nameof(DifferenceData), null)]
        public void DifferenceCalculatedCorrectly(string[] baseSetItems, string[] substituteDataItems, string[] expectedResultSetItems)
        {
            // Arrange
            var baseSet = new HashSet<string>(baseSetItems);
            var substituteData = new HashSet<string>(substituteDataItems);
            var expectedResultSet = new HashSet<string>(expectedResultSetItems);

            // Act
            var result = baseSet.Difference(substituteData);

            // Assert
            Assert.Equal<ISet<string>>(expectedResultSet, result);
        }

        public static IEnumerable<object[]> DifferenceData =>
            new List<object[]>
            {
                new object[] { new [] { "a", "b", "c" }, new[] { "a", "b", "c" }, new string[] { } },
                new object[] { new [] { "a", "b" }, new[] { "a", "b", "c" }, new string[] { } },
                new object[] { new [] { "a" }, new[] { "a", "b", "c" }, new string[] { } },
                new object[] { new string[] { }, new[] { "a", "b", "c" }, new string[] { } },
                new object[] { new [] { "a", "b", "c" }, new[] { "a", "b" }, new [] { "c" } },
                new object[] { new [] { "a", "b", "c" }, new[] { "a" }, new [] { "b", "c" } },
                new object[] { new [] { "a", "b", "c" }, new string[] { }, new [] { "a", "b", "c" } }
            };
    }
}
