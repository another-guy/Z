using System;
using System.Collections.Generic;

namespace Z.Trees
{
    public sealed class Trie<T> : TrieNode<T>
    {
        private readonly bool _caseSensitive;
        private readonly bool _enforceWhitespaceTrimming;

        public Trie(bool caseSensitive = false, bool enforceWhitespaceTrimming = false)
        {
            _caseSensitive = caseSensitive;
            _enforceWhitespaceTrimming = enforceWhitespaceTrimming;
        }

        public void Add(string word, T relatedValue)
        {
            word = AdjustSearchKeyBasedOnCaseSensivity(word);
            base.Add(word, relatedValue);
        }

        public T Get(string word)
        {
            word = AdjustSearchKeyBasedOnCaseSensivity(word);
            return base.Get(word);
        }

        public bool Contains(string word)
        {
            word = AdjustSearchKeyBasedOnCaseSensivity(word);
            return base.Contains(word);
        }

        public bool IsPrefix(string prefix)
        {
            prefix = AdjustSearchKeyBasedOnCaseSensivity(prefix);
            return base.IsPrefix(prefix);
        }

        private string AdjustSearchKeyBasedOnCaseSensivity(string searchKey)
        {
            var containsWhitespacesToTrim = searchKey.Trim().Length != searchKey.Length;
            if (_enforceWhitespaceTrimming && containsWhitespacesToTrim)
                throw new ArgumentException($"searchKey='{searchKey}' contains whitespaces that must be trimmed.");

            return _caseSensitive ? searchKey : searchKey.ToLower();
        }
    }

    public class TrieNode<T>
    {
        private readonly IDictionary<char, TrieNode<T>> _inner = new Dictionary<char, TrieNode<T>>();
        private bool _isWord = false;
        private T _value;

        internal TrieNode() { }
        
        protected void Add(string word, T relatedValue, int charIndex = 0)
        {
            if (charIndex >= word.Length)
            {
                _isWord = true;
                return;
            }

            var chr = word[charIndex];

            TrieNode<T> suffix;
            if (_inner.TryGetValue(chr, out suffix) == false)
            {
                suffix = new TrieNode<T>
                {
                    _value = relatedValue
                };
                _inner[chr] = suffix;
            }
            
            suffix.Add(word, relatedValue, charIndex + 1);
        }

        protected T Get(string word, int charIndex = 0)
        {
            if (charIndex >= word.Length)
            {
                if (_isWord)
                    return _value;
                throw CreateWordIsNotPresentException(word);
            }

            var chr = word[charIndex];

            TrieNode<T> suffix;
            if (_inner.TryGetValue(chr, out suffix) == false)
                throw CreateWordIsNotPresentException(word);

            return suffix.Get(word, charIndex + 1);
        }

        protected bool Contains(string word, int charIndex = 0)
        {
            if (charIndex >= word.Length)
            {
                return _isWord;
            }

            var chr = word[charIndex];

            TrieNode<T> suffix;
            if (_inner.TryGetValue(chr, out suffix) == false)
                return false;

            return suffix.Contains(word, charIndex + 1);
        }

        protected bool IsPrefix(string prefix, int charIndex = 0)
        {
            if (charIndex >= prefix.Length)
                return true;

            var chr = prefix[charIndex];

            TrieNode<T> suffix;
            if (_inner.TryGetValue(chr, out suffix) == false)
                return false;

            return suffix.IsPrefix(prefix, charIndex + 1);
        }

        private static InvalidOperationException CreateWordIsNotPresentException(string word)
        {
            return new InvalidOperationException($"Word '{word}' is not present in the trie");
        }
    }
}
