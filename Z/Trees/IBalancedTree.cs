namespace Z.Trees
{
    public interface IBalancedTree<T>
    {
        void Add(T value);

        void Remove(T value);

        bool Find(T value);
    }
}
