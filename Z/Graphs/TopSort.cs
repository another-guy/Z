using System.Collections.Generic;
using System.Linq;
using Z.Collections;
using Z.Sets;

namespace Z.Graphs
{
    public sealed class TopSort
    {
        public IList<Vertex<TVertexValue>> Run<TVertexValue, TEdgeValue>(OrGraph<TVertexValue, TEdgeValue> graph)
        {
            var visited = new HashSet<Vertex<TVertexValue>>();
            var stack = new Stack<Vertex<TVertexValue>>();
            while (true)
            {
                var notVisited = graph.Vertices.Difference(visited);
                if (notVisited.Count == 0)
                    break;
                
                var current = notVisited.Pop();

                Run(graph, visited, stack, current);
            }
            
            return stack.ToList();
        }

        private void Run<TVertexValue, TEdgeValue>(
            OrGraph<TVertexValue, TEdgeValue> graph,
            ISet<Vertex<TVertexValue>> visited,
            Stack<Vertex<TVertexValue>> stack,
            Vertex<TVertexValue> current)
        {
            visited.Add(current);

            foreach (var edge in graph.Edges)
                if (edge.Source.Equals(current) && visited.Contains(edge.Destination) == false)
                    Run(graph, visited, stack, edge.Destination);

            stack.Push(current);
        }
    }
}
