using System;
using System.Collections.Generic;
using System.Linq;

namespace Z.Graphs
{
    public sealed class OrGraph<TVertexValue>
    {
        // TODO Add "weightened" edge marker and use it to enforce weights on edges
        // TODO Add graph id that will be imprinted into vertices and edges

        private readonly ISet<Vertex<TVertexValue>> vertices = new HashSet<Vertex<TVertexValue>>();
        private readonly ISet<Edge<TVertexValue>> edges = new HashSet<Edge<TVertexValue>>();

        // TODO HIGH Clone, protect!
        public ISet<Vertex<TVertexValue>> Vertices => vertices;
        public ISet<Edge<TVertexValue>> Edges => edges;

        public Vertex<TVertexValue> AddVertex(TVertexValue key)
        {
            if (vertices.Any(v => v.Key.Equals(key)))
                throw new InvalidOperationException($"Vertex with key '{key}' already exists");

            var newVertex = new Vertex<TVertexValue>(key);
            vertices.Add(newVertex);
            return newVertex;
        }

        public Edge<TVertexValue> AddEdge(Vertex<TVertexValue> source, Vertex<TVertexValue> destination)
        {
            if (edges.Any(e => e.Source.Equals(source) && e.Destination.Equals(destination)))
                throw new InvalidOperationException($"Edge from '{source.Key}' to '{destination.Key}' already exists");
            if (vertices.Contains(source) == false)
                throw new InvalidOperationException("Source vertex does not exist");
            if (vertices.Contains(destination) == false)
                throw new InvalidOperationException("Destination vertex does not exist");

            var newEdge = new Edge<TVertexValue>(source, destination);
            edges.Add(newEdge);
            return newEdge;
        }
    }

    public sealed class Vertex<TVertexValue>
    {
        private readonly TVertexValue key;

        public TVertexValue Key => key;

        public Vertex(TVertexValue key)
        {
            this.key = key;
        }

        private bool Equals(Vertex<TVertexValue> other)
        {
            return EqualityComparer<TVertexValue>.Default.Equals(key, other.key);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is Vertex<TVertexValue> && Equals((Vertex<TVertexValue>)obj);
        }

        public override int GetHashCode()
        {
            return EqualityComparer<TVertexValue>.Default.GetHashCode(key);
        }
    }

    public sealed class Edge<TVertexValue>
    {
        private readonly Vertex<TVertexValue> source;
        private readonly Vertex<TVertexValue> destination;

        public Vertex<TVertexValue> Source => source;
        public Vertex<TVertexValue> Destination => destination;

        public Edge(Vertex<TVertexValue> source, Vertex<TVertexValue> destination)
        {
            this.source = source;
            this.destination = destination;
        }

        private bool Equals(Edge<TVertexValue> other)
        {
            return Equals(source, other.source) && Equals(destination, other.destination);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is Edge<TVertexValue> && Equals((Edge<TVertexValue>)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((source?.GetHashCode() ?? 0) * 397) ^ (destination?.GetHashCode() ?? 0);
            }
        }
    }
}
