using System.Collections.Generic;

namespace Z.Trees
{
    public sealed class BinaryTree<T> : BinaryTreeNode<T>
    {
    }

    public class BinaryTreeNode<T>
    {
        public T Value { get; set; }
        public BinaryTreeNode<T> Left { get; private set; }
        public BinaryTreeNode<T> Right { get; private set; }

        public BinaryTreeNode<T> Add(T value)
        {
            if (Value == null)
            {
                Value = value;
                return this;
            }
            else
            {
                var comparer = Comparer<T>.Default;
                var current = this;
                var newTreeNode = new BinaryTreeNode<T> {Value = value};
                do
                {
                    var leftInsert = comparer.Compare(value, current.Value) < 0;
                    if (leftInsert)
                    {
                        if (current.Left != null)
                            current = current.Left;
                        else
                        {
                            current.Left = newTreeNode;
                            return this;
                        }
                    }
                    else
                    {
                        if (current.Right != null)
                            current = current.Right;
                        else
                        {
                            current.Right = newTreeNode;
                            return this;
                        }
                    }
                } while (true);
            }
        }
    }
}
