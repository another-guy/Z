using System.Collections.Generic;
using System.Linq;
using NuGet.Packaging;
using Xunit;
using Z.Collections;

namespace Z.Tests.Collections
{
    public class CollectionExtensionsTests
    {
        private readonly ICollection<int> _sut = new List<int>();

        [Fact]
        public void PopWorks()
        {
            // Arrange
            _sut.AddRange(new [] { 1, 2, 3 });

            // Act
            var result = _sut.Pop();

            // Assert
            Assert.Equal(1, result);
            Assert.True(new [] { 2, 3 }.SequenceEqual(_sut));
        }

        [Fact]
        public void PopWorks2()
        {
            // Arrange
            _sut.AddRange(new[] { 1, 1, 2, 3 });

            // Act
            var result = _sut.Pop();

            // Assert
            Assert.Equal(1, result);
            Assert.True(new[] { 1, 2, 3 }.SequenceEqual(_sut));
        }
    }
}
