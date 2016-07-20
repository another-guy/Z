using System.Collections.Generic;
using System.Linq;
using NuGet.Packaging;
using Xunit;
using Z.Collections;

namespace Z.Tests.Collections
{
    public class CollectionExtensionsTests
    {
        private readonly ICollection<int> sut = new List<int>();

        [Fact]
        public void PopWorks()
        {
            // Arrange
            sut.AddRange(new [] { 1, 2, 3 });

            // Act
            var result = sut.Pop();

            // Assert
            Assert.Equal(1, result);
            Assert.True(new [] { 2, 3 }.SequenceEqual(sut));
        }

        [Fact]
        public void PopWorks2()
        {
            // Arrange
            sut.AddRange(new[] { 1, 1, 2, 3 });

            // Act
            var result = sut.Pop();

            // Assert
            Assert.Equal(1, result);
            Assert.True(new[] { 1, 2, 3 }.SequenceEqual(sut));
        }
    }
}
