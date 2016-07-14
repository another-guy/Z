using System.Collections.Generic;
using System.Linq;
using Z.Graphs;

namespace Z.Tests.Factories
{
    public class OrGraphFactory
    {
        public OrGraph<T> CreateFrom<T>(IEnumerable<T> vertices, IEnumerable<T[]> edges)
        {
            var graph = new OrGraph<T>();

            foreach (var vertex in vertices)
            {
                graph.AddVertex(vertex);
            }

            foreach (var edge in edges)
            {
                var v1 = graph.Vertices.Single(v => v.Key.Equals(edge[0]));
                var v2 = graph.Vertices.Single(v => v.Key.Equals(edge[1]));
                graph.AddEdge(v1, v2);
            }

            return graph;
        }
    }
}
