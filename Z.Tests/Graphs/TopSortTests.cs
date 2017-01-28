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
        private readonly TopSort _sut = new TopSort();

        [Theory, MemberData(nameof(GraphDataWithStrings))]
        public void TopSortCalculatedCorrectlyForGraphOfStrings(string[] vertices, Tuple<string, string, int>[] edges, object knownDetermenisticResult)
        {
            // Arrange
            var graph = new OrGraphFactory().CreateFrom(vertices, edges, StringComparer.Ordinal);
            var expectedResult = knownDetermenisticResult as List<string>;

            // Act
            var result = _sut.Run(graph);

            // Assert
            if (expectedResult == null)
                Assert.True(IsTopSort(graph, result));
            else
                Assert.True(result.Select(i => i.Key).SequenceEqual(expectedResult));
        }

        [Theory, MemberData(nameof(GraphDataWithAtoms))]
        public void TopSortCalculatedCorrectlyForGraphOfAtoms(Atom[] vertices, Tuple<Atom, Atom, int>[] edges, object knownDetermenisticResult)
        {
            // Arrange
            var graph = new OrGraphFactory().CreateFrom(vertices, edges, new AtomComparer());
            var expectedResult = knownDetermenisticResult as List<Atom>;

            // Act
            var result = _sut.Run(graph);

            // Assert
            if (expectedResult == null)
                Assert.True(IsTopSort(graph, result));
            else
                Assert.True(result.Select(i => i.Key.Value).SequenceEqual(expectedResult.Select(e => e.Value)));
        }

        public static IEnumerable<object[]> GraphDataWithStrings =>
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

        public static IEnumerable<object[]> GraphDataWithAtoms =>
            new List<object[]>
            {
                new object[]
                {
                    new Atom[] { "a", "b", "c" },
                    new []
                    {
                        Tuple.Create<Atom, Atom, int>("a", "b", 1),
                        Tuple.Create<Atom, Atom, int>("b", "c", 1)
                    },
                    new List<Atom> { "a", "b", "c" }
                },

                new object[]
                {
                    new Atom[] { "a", "b", "c", "d", "e" },
                    new[]
                    {
                        Tuple.Create<Atom, Atom, int>("a", "b", 1),
                        Tuple.Create<Atom, Atom, int>("b", "c", 1),
                        Tuple.Create<Atom, Atom, int>("b", "d", 1),
                        Tuple.Create<Atom, Atom, int>("c", "e", 1),
                        Tuple.Create<Atom, Atom, int>("d", "e", 1)
                    },
                    "result is not deterministic"
                }
            };

        private bool IsTopSort<T>(OrGraph<T, int> graph, IList<Vertex<T>> topSort)
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

        public sealed class Atom
        {
            public string Value { get; }
            public Atom(string value)
            {
                Value = value;
            }
            public static implicit operator Atom(string value)
            {
                return new Atom(value);
            }
        }

        public sealed class AtomComparer : IEqualityComparer<Atom>
        {
            public bool Equals(Atom x, Atom y)
            {
                return x.Value == y.Value;
            }
            public int GetHashCode(Atom obj)
            {
                throw new NotImplementedException();
            }
        }
    }
}
