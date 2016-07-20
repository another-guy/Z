using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Z.Graphs;
using Z.Tests.Factories;

namespace Z.Tests.Graphs
{
    public class TopSortTests
    {
        private readonly TopSort sut = new TopSort();

        [Theory, MemberData(nameof(GraphData))]
        public void TopSortCalculatedCorrectly(string[] vertices, Tuple<string, string, int>[] edges, object knownDetermenisticResult)
        {
            // Arrange
            var graph = new OrGraphFactory().CreateFrom(vertices, edges);
            var expectedResult = knownDetermenisticResult as List<string>;

            // Act
            var result = sut.Run(graph);

            // Assert
            if (expectedResult == null)
                Assert.True(IsTopSort(graph, result));
            else
                Assert.True(result.Select(i => i.Key).SequenceEqual(expectedResult));
        }

        public static IEnumerable<object[]> GraphData =>
            new List<object[]>
            {
                new object[]
                {
                    new[] { "a", "b", "c" },
                    new[]
                    {
                        Tuple.Create("a", "b", 1),
                        Tuple.Create("b", "c", 1)
                    },
                    new List<string> { "a", "b", "c" }
                },
                new object[]
                {
                    new[] { "a", "b", "c", "d", "e" },
                    new[]
                    {
                        Tuple.Create("a", "b", 1),
                        Tuple.Create("b", "c", 1),
                        Tuple.Create("b", "d", 1),
                        Tuple.Create("c", "e", 1),
                        Tuple.Create("d", "e", 1)
                    },
                    "result is not deterministic"
                }
            };

        private bool IsTopSort(OrGraph<string, int> graph, IList<Vertex<string>> topSort)
        {
            for (var sourceIndex = 0; sourceIndex < topSort.Count - 1; sourceIndex++)
            {
                var source = topSort[sourceIndex];
                for (var destIndex = sourceIndex + 1; destIndex < topSort.Count; destIndex++)
                {
                    var dest = topSort[destIndex];
                    if (new Path().Exists(from: dest, to: source, inGraph: graph))
                        return false;
                }
            }
            return true;
        }
    }
}
