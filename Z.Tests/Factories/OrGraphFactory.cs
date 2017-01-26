using System;
using System.Collections.Generic;
using System.Linq;
using Z.Graphs;

namespace Z.Tests.Factories
{
    public class OrGraphFactory
    {
        public OrGraph<TVertexValue, TEdgeValue> CreateFrom<TVertexValue, TEdgeValue>(
            IEnumerable<TVertexValue> vertices,
            Tuple<TVertexValue, TVertexValue, TEdgeValue>[] edges,
            IEqualityComparer<TVertexValue> vertexComparer)
            where TVertexValue : class
        {
            var graph = new OrGraph<TVertexValue, TEdgeValue>();

            foreach (var vertex in vertices)
            {
                graph.AddVertex(vertex);
            }

            foreach (var edge in edges)
            {
                var v1 = graph.Vertices.Single(v => vertexComparer.Equals(v.Key, edge.Item1));
                var v2 = graph.Vertices.Single(v => vertexComparer.Equals(v.Key, edge.Item2));
                graph.AddEdge(v1, v2, edge.Item3);
            }

            return graph;
        }
    }
}
