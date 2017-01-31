using Xunit;
using Z.Trees;

namespace Z.Tests.Trees
{
    public class BinaryTreeTests
    {
        private readonly BinaryTreeNode<string> _sut = new BinaryTree<string>();

        [Fact]
        public void AddToEmptyWorks()
        {
            Assert.Null(_sut.Value);

            _sut.Add("text");

            Assert.Equal("text", _sut.Value);
        }

        [Fact]
        public void AddLeftWorks()
        {
            // Arrange
            _sut.Add("M");
            
            // Act
            _sut.Add("F");

            // Assert
            Assert.Equal("M", _sut.Value);
            Assert.Equal("F", _sut.Left.Value);
        }

        [Fact]
        public void AddRightWorks()
        {
            // Arrange
            _sut.Add("M");

            // Act
            _sut.Add("R");

            // Assert
            Assert.Equal("M", _sut.Value);
            Assert.Equal("R", _sut.Right.Value);
        }

        [Fact]
        public void AddLeftLeftLevelWorks()
        {
            // Arrange
            _sut.Add("M").Add("F");

            // Act
            _sut.Add("A");

            // Assert
            Assert.Equal("M", _sut.Value);
            Assert.Equal("F", _sut.Left.Value);
            Assert.Equal("A", _sut.Left.Left.Value);
        }

        [Fact]
        public void AddLeftRightLevelWorks()
        {
            // Arrange
            _sut.Add("M").Add("F");

            // Act
            _sut.Add("H");

            // Assert
            Assert.Equal("M", _sut.Value);
            Assert.Equal("F", _sut.Left.Value);
            Assert.Equal("H", _sut.Left.Right.Value);
        }

        [Fact]
        public void AddRightLeftLevelWorks()
        {
            // Arrange
            _sut.Add("M").Add("R");

            // Act
            _sut.Add("P");

            // Assert
            Assert.Equal("M", _sut.Value);
            Assert.Equal("R", _sut.Right.Value);
            Assert.Equal("P", _sut.Right.Left.Value);
        }

        [Fact]
        public void AddRightRightLevelWorks()
        {
            // Arrange
            _sut.Add("M").Add("R");

            // Act
            _sut.Add("Z");

            // Assert
            Assert.Equal("M", _sut.Value);
            Assert.Equal("R", _sut.Right.Value);
            Assert.Equal("Z", _sut.Right.Right.Value);
        }
    }
}
