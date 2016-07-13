using System.Collections.Generic;
using System.Linq;

namespace Z.Graphs
{
    // TODO Add test

    /*
    def run_top_sort(graph, visited, stack):
        while True:
            not_visited = graph['vertices'].difference(visited)
            if (len(not_visited) == 0):
                break
            top_sort(graph, visited, stack, not_visited.pop())

    def top_sort(graph, visited, stack, current):
        visited.add(current)
        for [src, dest] in graph['edges']:
            if src == current and dest not in visited:
                top_sort(graph, visited, stack, dest)
        stack.append(current)
     */

    public sealed class TopSort
    {
        public IList<Vertex<TVertexValue>> Run<TVertexValue>(OrGraph<TVertexValue> graph)
        {
            var visited = new HashSet<Vertex<TVertexValue>>();
            var stack = new Stack<Vertex<TVertexValue>>();
            while (true)
            {
                var notVisited = graph.Vertices.Difference(visited);
                if (notVisited.Count == 0)
                    break;

                // TODO pop() ???
                var current = notVisited.First();
                notVisited.Remove(current);

                Run(graph, visited, stack, current);
            }

            // TODO correct order ???
            return stack.ToList();
        }

        private void Run<TVertexValue>(
            OrGraph<TVertexValue> graph,
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

    public static class SetX
    {
        public static ISet<T> Difference<T>(this ISet<T> baseSet, ISet<T> setToSubstract)
        {
            var result = new HashSet<T>(baseSet);
            foreach (var element in setToSubstract)
                result.Remove(element);
            return result;
        }
    }
}
