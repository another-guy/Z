using System;
using System.Linq;

namespace Z.Graphs
{
    // TODO https://en.wikipedia.org/wiki/Johnson%27s_algorithm
    public class Dijkstra
    {
        public void Run<TVertexValue, TEdgeValue>(
            OrGraph<TVertexValue, TEdgeValue> graph,
            Func<Edge<TVertexValue, TEdgeValue>, double> weight)
        {
            CheckNoNegativeWeightEdgeExist(graph, weight);

            // TODO Requires a priority queue...
        }

        private void CheckNoNegativeWeightEdgeExist<TVertexValue, TEdgeValue>(
            OrGraph<TVertexValue, TEdgeValue> graph,
            Func<Edge<TVertexValue, TEdgeValue>, double> weight)
        {
            var edges = graph.Edges.Where(e => weight(e) < 0.0d).ToArray();
            if (edges.Length > 0)
            {
                var message =
                    $"{edges.Length} edges have negative weights. " +
                    "All weights must be non-negative in Dijkstra's algorithm. " +
                    "Consider using Johnson's algorithm.";
                throw new ArgumentException(message);
            }
        }
    }
}
