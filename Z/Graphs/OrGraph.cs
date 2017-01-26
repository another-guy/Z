using System;
using System.Collections.Generic;
using System.Linq;

namespace Z.Graphs
{
    public sealed class OrGraph<TVertexValue, TEdgeValue>
    {
        // TODO HIGH Clone, protect!
        public ISet<Vertex<TVertexValue>> Vertices { get; } = new HashSet<Vertex<TVertexValue>>();
        public ISet<Edge<TVertexValue, TEdgeValue>> Edges { get; } = new HashSet<Edge<TVertexValue, TEdgeValue>>();

        public Vertex<TVertexValue> AddVertex(TVertexValue key)
        {
            if (Vertices.Any(v => v.Key.Equals(key)))
                throw new InvalidOperationException($"Vertex with key '{key}' already exists");

            var newVertex = new Vertex<TVertexValue>(key);
            Vertices.Add(newVertex);
            return newVertex;
        }

        public Edge<TVertexValue, TEdgeValue> AddEdge(Vertex<TVertexValue> source, Vertex<TVertexValue> destination, TEdgeValue value)
        {
            if (Edges.Any(e => e.Source.Equals(source) && e.Destination.Equals(destination)))
                throw new InvalidOperationException($"Edge from '{source.Key}' to '{destination.Key}' already exists");
            if (Vertices.Contains(source) == false)
                throw new InvalidOperationException("Source vertex does not exist");
            if (Vertices.Contains(destination) == false)
                throw new InvalidOperationException("Destination vertex does not exist");

            var newEdge = new Edge<TVertexValue, TEdgeValue>(source, destination, value);
            Edges.Add(newEdge);
            return newEdge;
        }
    }

    public sealed class Vertex<TVertexValue>
    {
        public TVertexValue Key { get; }

        public Vertex(TVertexValue key)
        {
            Key = key;
        }

        private bool Equals(Vertex<TVertexValue> other)
        {
            return EqualityComparer<TVertexValue>.Default.Equals(Key, other.Key);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is Vertex<TVertexValue> && Equals((Vertex<TVertexValue>)obj);
        }

        public override int GetHashCode()
        {
            return EqualityComparer<TVertexValue>.Default.GetHashCode(Key);
        }
    }

    public sealed class Edge<TVertexValue, TEdgeValue>
    {
        public Vertex<TVertexValue> Source { get; }
        public Vertex<TVertexValue> Destination { get; }
        public TEdgeValue Value { get; }

        public Edge(Vertex<TVertexValue> source, Vertex<TVertexValue> destination, TEdgeValue value)
        {
            Source = source;
            Destination = destination;
            Value = value;
        }

        private bool Equals(Edge<TVertexValue, TEdgeValue> other)
        {
            return Equals(Source, other.Source) && Equals(Destination, other.Destination) &&
                EqualityComparer<TEdgeValue>.Default.Equals(Value, other.Value);
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
                var hashCode = Source?.GetHashCode() ?? 0;
                hashCode = (hashCode*397) ^ (Destination?.GetHashCode() ?? 0);
                hashCode = (hashCode*397) ^ EqualityComparer<TEdgeValue>.Default.GetHashCode(Value);
                return hashCode;
            }
        }
    }
}
