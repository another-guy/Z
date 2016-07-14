using System.Collections.Generic;
using System.Linq;

namespace Z.Graphs
{
    public sealed class Path
    {
        public bool Exists<TVertexValue>(Vertex<TVertexValue> from, Vertex<TVertexValue> to, OrGraph<TVertexValue> inGraph)
        {
            var edgesToGo = new Queue<Edge<TVertexValue>>(inGraph.Edges.Where(e => e.Source.Equals(from)));
            while (edgesToGo.Count > 0)
            {
                var currentEdge = edgesToGo.Dequeue();
                if (currentEdge.Destination.Equals(to))
                    return true;

                var newEdges = inGraph.Edges.Where(e => e.Source.Equals(currentEdge.Destination));
                foreach (var edge in newEdges)
                    edgesToGo.Enqueue(edge);
            }
            return false;
        }
    }
}
