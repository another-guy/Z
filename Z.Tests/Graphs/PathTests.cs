using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Z.Graphs;
using Z.Tests.Factories;

namespace Z.Tests.Graphs
{
    public class PathTests
    {
        private readonly Path _sut = new Path();

        [Theory, MemberData(nameof(PathExistsData))]
        public void PathExistsCalculatedCorrectly(string[] vertices, Tuple<string, string, int>[] edges, string[] path, bool pathActiallyExists)
        {
            // Arrange
            var graph = new OrGraphFactory().CreateFrom(vertices, edges, StringComparer.Ordinal);
            var v1 = graph.Vertices.Single(v => v.Key.Equals(path[0]));
            var v2 = graph.Vertices.Single(v => v.Key.Equals(path[1]));

            // Act
            var pathExists = _sut.Exists(from: v1, to: v2, inGraph: graph);

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
                        Tuple.Create("a", "b", 1)
                    },
                    new [] { "a", "b" },
                    true
                },
                new object[]
                {
                    new[] { "a", "b" },
                    new[]
                    {
                        Tuple.Create("b", "a", 1)
                    },
                    new [] { "a", "b" },
                    false
                },
                new object[]
                {
                    new[] { "a", "b", "c" },
                    new[]
                    {
                        Tuple.Create("a", "b", 1),
                        Tuple.Create("b", "c", 1)
                    },
                    new [] { "a", "c" },
                    true
                },
                new object[]
                {
                    new[] { "a", "b", "c" },
                    new[]
                    {
                        Tuple.Create("a", "b", 1),
                        Tuple.Create("b", "c", 1)
                    },
                    new [] { "c", "a" },
                    false
                },
                new object[]
                {
                    new[] { "a" },
                    new[]
                    {
                        Tuple.Create("a", "a", 1)
                    },
                    new [] { "a", "a" },
                    true
                }
            };
    }
}
