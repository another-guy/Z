namespace Z.Trees
{
    public sealed class RedBlackTree<T> : IBalancedTree<T>
    {
        public Node<T> Root { get; set; }

        public void Add(T value)
        {
            if (Root == null)
                Root = new Node<T> { IsRed = false, Value = value };
            else
                throw new System.NotImplementedException();
        }

        public void Remove(T value)
        {
            throw new System.NotImplementedException();
        }

        public bool Find(T value)
        {
            if (Root.Value.Equals(value))
                return true;
            throw new System.NotImplementedException();
        }

        public sealed class Node<TN>
        {
            public bool IsRed { get; set; }

            public Node<TN> Left { get; set; }

            public Node<TN> Right { get; set; }

            public TN Value { get; set; }
        }
    }
}