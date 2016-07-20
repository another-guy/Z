using System;
using System.Collections.Generic;
using System.Linq;

namespace Z.Graphs
{
    public sealed class OrGraph<TVertexValue, TEdgeValue>
    {
        // TODO Add graph id that will be imprinted into vertices and edges

        private readonly ISet<Vertex<TVertexValue>> vertices = new HashSet<Vertex<TVertexValue>>();
        private readonly ISet<Edge<TVertexValue, TEdgeValue>> edges = new HashSet<Edge<TVertexValue, TEdgeValue>>();

        // TODO HIGH Clone, protect!
        public ISet<Vertex<TVertexValue>> Vertices => vertices;
        public ISet<Edge<TVertexValue, TEdgeValue>> Edges => edges;

        public Vertex<TVertexValue> AddVertex(TVertexValue key)
        {
            if (vertices.Any(v => v.Key.Equals(key)))
                throw new InvalidOperationException($"Vertex with key '{key}' already exists");

            var newVertex = new Vertex<TVertexValue>(key);
            vertices.Add(newVertex);
            return newVertex;
        }

        public Edge<TVertexValue, TEdgeValue> AddEdge(Vertex<TVertexValue> source, Vertex<TVertexValue> destination, TEdgeValue value)
        {
            if (edges.Any(e => e.Source.Equals(source) && e.Destination.Equals(destination)))
                throw new InvalidOperationException($"Edge from '{source.Key}' to '{destination.Key}' already exists");
            if (vertices.Contains(source) == false)
                throw new InvalidOperationException("Source vertex does not exist");
            if (vertices.Contains(destination) == false)
                throw new InvalidOperationException("Destination vertex does not exist");

            var newEdge = new Edge<TVertexValue, TEdgeValue>(source, destination, value);
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

    public sealed class Edge<TVertexValue, TEdgeValue>
    {
        private readonly Vertex<TVertexValue> source;
        private readonly Vertex<TVertexValue> destination;
        private readonly TEdgeValue value;

        public Vertex<TVertexValue> Source => source;
        public Vertex<TVertexValue> Destination => destination;
        public TEdgeValue Value => value;

        public Edge(Vertex<TVertexValue> source, Vertex<TVertexValue> destination, TEdgeValue value)
        {
            this.source = source;
            this.destination = destination;
            this.value = value;
        }

        private bool Equals(Edge<TVertexValue, TEdgeValue> other)
        {
            return Equals(source, other.source) && Equals(destination, other.destination) &&
                EqualityComparer<TEdgeValue>.Default.Equals(value, other.value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is Edge<TVertexValue, TEdgeValue> && Equals((Edge<TVertexValue, TEdgeValue>) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = source?.GetHashCode() ?? 0;
                hashCode = (hashCode*397) ^ (destination?.GetHashCode() ?? 0);
                hashCode = (hashCode*397) ^ EqualityComparer<TEdgeValue>.Default.GetHashCode(value);
                return hashCode;
            }
        }
    }
}
