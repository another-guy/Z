using System.Collections.Generic;

namespace Z.Trees
{
    public sealed class TraverseBinaryTree
    {
        public IEnumerable<BinaryTreeNode<T>> BreadthFirst<T>(BinaryTreeNode<T> binaryTree)
        {
            var queue = new Queue<BinaryTreeNode<T>>();
            queue.Enqueue(binaryTree);

            while (queue.Count > 0)
            {
                var currentNode = queue.Dequeue();

                if (currentNode.Left != null)
                    queue.Enqueue(currentNode.Left);
                if (currentNode.Right != null)
                    queue.Enqueue(currentNode.Right);

                yield return currentNode;
            }
        }

        public IEnumerable<BinaryTreeNode<T>> DepthFirst<T>(BinaryTreeNode<T> binaryTree)
        {
            var stack = new Stack<BinaryTreeNode<T>>();
            stack.Push(binaryTree);

            while (stack.Count > 0)
            {
                var currentNode = stack.Pop();

                if (currentNode.Right != null)
                    stack.Push(currentNode.Right);
                if (currentNode.Left != null)
                    stack.Push(currentNode.Left);

                yield return currentNode;
            }
        }
    }
}
