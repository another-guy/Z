using System.Linq;
using Xunit;
using Z.Trees;

namespace Z.Tests.Trees
{
    public class TraverseBinaryTreeTests
    {
        private readonly TraverseBinaryTree _sut = new TraverseBinaryTree();

        [Fact]
        public void TraverseBreadthFirst()
        {
            // Arrange
            var tree = new BinaryTree<string>()
                .Add("M")
                .Add("C")
                .Add("R")
                .Add("A")
                .Add("E")
                .Add("P")
                .Add("X");

            // Act
            var treeValues = _sut
                .BreadthFirst(tree)
                .Select(node => node.Value);

            // Assert
            Assert.True(treeValues.SequenceEqual(new[] {"M", "C", "R", "A", "E", "P", "X"}));
        }

        [Fact]
        public void TraverseDepthFirst()
        {
            // Arrange
            var tree = new BinaryTree<string>()
                .Add("M")
                .Add("C")
                .Add("R")
                .Add("A")
                .Add("E")
                .Add("P")
                .Add("X");

            // Act
            var treeValues = _sut
                .DepthFirst(tree)
                .Select(node => node.Value);

            // Assert
            Assert.True(treeValues.SequenceEqual(new[] { "M", "C", "A", "E", "R", "P", "X" }));
        }
    }
}
