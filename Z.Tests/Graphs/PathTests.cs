using System.Collections.Generic;
using System.Linq;
using Xunit;
using Z.Graphs;
using Z.Tests.Factories;

namespace Z.Tests.Graphs
{
    public class PathTests
    {
        private readonly Path sut = new Path();

        [Theory, MemberData(nameof(PathExistsData))]
        public void PathExistsCalculatedCorrectly(string[] vertices, string[][] edges, string[] path, bool pathActiallyExists)
        {
            // Arrange
            var graph = new OrGraphFactory().CreateFrom(vertices, edges);
            var v1 = graph.Vertices.Single(v => v.Key.Equals(path[0]));
            var v2 = graph.Vertices.Single(v => v.Key.Equals(path[1]));

            // Act
            var pathExists = sut.Exists(from: v1, to: v2, inGraph: graph);

            // Assert
            Assert.Equal(pathActiallyExists, pathExists);
        }

        public static IEnumerable<object[]> PathExistsData =>
            new List<object[]>
            {
                new object[]
                {
                    new[] { "a", "b" },
                    new[]
                    {
                        new [] { "a", "b" }
                    },
                    new [] { "a", "b" },
                    true
                },
                new object[]
                {
                    new[] { "a", "b" },
                    new[]
                    {
                        new [] { "b", "a" }
                    },
                    new [] { "a", "b" },
                    false
                },
                new object[]
                {
                    new[] { "a", "b", "c" },
                    new[]
                    {
                        new [] { "a", "b" },
                        new [] { "b", "c" }
                    },
                    new [] { "a", "c" },
                    true
                },
                new object[]
                {
                    new[] { "a", "b", "c" },
                    new[]
                    {
                        new [] { "a", "b" },
                        new [] { "b", "c" }
                    },
                    new [] { "c", "a" },
                    false
                },
                new object[]
                {
                    new[] { "a" },
                    new[]
                    {
                        new [] { "a", "a" }
                    },
                    new [] { "a", "a" },
                    true
                }
            };
    }
}
