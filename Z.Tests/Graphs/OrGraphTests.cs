using System;
using System.Collections.Generic;
using System.Linq;
using Z.Graphs;
using Xunit;

namespace Z.Tests.Graphs
{
    public class OrGraphTests
    {
        private readonly OrGraph<string, int> sut = new OrGraph<string, int>();

        [Fact]
        public void VerticesAdditionSucceeds()
        {
            // Arrange
            var list = new List<string> { "a", "z", "b", "c", "x", "e", "f" };

            // Act
            foreach (var key in list)
                sut.AddVertex(key);

            // Assert
            var expected = list.Select(i => new Vertex<string>(i)).ToList();
            Assert.True(expected.SequenceEqual(sut.Vertices));
        }

        [Fact]
        public void VerticesAdditionFailsWhenVertexExists()
        {
            // Arrange
            sut.AddVertex("a");

            // Act
            var caught = Assert.Throws<InvalidOperationException>(() => sut.AddVertex("a"));

            // Assert
            Assert.Equal("Vertex with key 'a' already exists", caught.Message);
        }

        [Fact]
        public void EdgeAddSucceedsWhenSrcAndDestVerticesExist()
        {
            // Arrange
            var a = sut.AddVertex("a");
            var b = sut.AddVertex("b");

            // Act
            var e = sut.AddEdge(a, b, 1);

            // Assert
            Assert.Equal(a, e.Source);
            Assert.Equal(b, e.Destination);
            Assert.True(sut.Edges.Contains(e));
        }

        [Fact]
        public void EdgeAddFailsWhenSrcVertexDoesntExist()
        {
            // Arrange
            var otherGraph = new OrGraph<string, int>();

            var a = otherGraph.AddVertex("a");
            var b = sut.AddVertex("b");

            // Act
            var caught = Assert.Throws<InvalidOperationException>(() => sut.AddEdge(a, b, 1));

            // Assert
            Assert.Equal("Source vertex does not exist", caught.Message);
        }

        [Fact]
        public void EdgeAddFailsWhenDestVertexDoesntExist()
        {
            // Arrange
            var otherGraph = new OrGraph<string, int>();

            var a = sut.AddVertex("a");
            var b = otherGraph.AddVertex("b");

            // Act
            var caught = Assert.Throws<InvalidOperationException>(() => sut.AddEdge(a, b, 1));

            // Assert
            Assert.Equal("Destination vertex does not exist", caught.Message);
        }

        [Fact]
        public void EdgeAddFailsWhenEdgeExists()
        {
            // Arrange
            var a = sut.AddVertex("a");
            var b = sut.AddVertex("b");

            sut.AddEdge(a, b, 1);

            // Act
            var caught = Assert.Throws<InvalidOperationException>(() => sut.AddEdge(a, b, 1));

            // Assert
            Assert.Equal("Edge from 'a' to 'b' already exists", caught.Message);
        }
    }
}
