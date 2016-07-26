using Xunit;
using Z.Trees;

namespace Z.Tests.Trees
{
    public class RedBlackTreeTests
    {
        private readonly RedBlackTree<int> sut = new RedBlackTree<int>();

        [Fact]
        public void AddFirst()
        {
            // Assume
            Assert.Null(sut.Root);

            // Arrange
            var value = 10;

            // Act
            sut.Add(value);

            // Assert
            Assert.NotNull(sut.Root);
            Assert.True(sut.Find(value));
        }

        [Fact]
        public void AddSecondLeft()
        {
            // Assume
            Assert.Null(sut.Root);

            // Arrange
            sut.Add(10);
            var leftValue = 5;

            // Act
            sut.Add(leftValue);

            // Assert
            Assert.Equal(5, sut.Root.Left.Value);
            Assert.True(sut.Find(5));
        }
    }
}
