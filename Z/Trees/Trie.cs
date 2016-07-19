using System;
using System.Collections.Generic;

namespace Z.Trees
{
    // TODO Consider making it a Collection and update tests then with Assert.Contains()
    public sealed class Trie : TrieNode
    {
        private bool caseSensitive;

        public Trie(bool caseSensitive = false)
        {
            // TODO Whitespaces check on both ends
            this.caseSensitive = caseSensitive;
        }

        public void Add(string word)
        {
            if (caseSensitive == false)
                word = word.ToLower();

            base.Add(word);
        }

        public bool Contains(string word)
        {
            if (caseSensitive == false)
                word = word.ToLower();

            return base.Contains(word);
        }

        public bool IsPrefix(string prefix)
        {
            if (caseSensitive == false)
                prefix = prefix.ToLower();

            return base.IsPrefix(prefix);
        }
    }

    public class TrieNode
    {
        private readonly IDictionary<char, TrieNode> inner = new Dictionary<char, TrieNode>();
        private bool isWord = false;

        internal TrieNode() { }
        
        protected void Add(string word, int charIndex = 0)
        {
            if (charIndex >= word.Length)
            {
                isWord = true;
                return;
            }

            var chr = word[charIndex];

            TrieNode suffix;
            if (inner.TryGetValue(chr, out suffix) == false)
            {
                suffix = new TrieNode();
                inner[chr] = suffix;
            }
            
            suffix.Add(word, charIndex + 1);
        }

        protected bool Contains(string word, int charIndex = 0)
        {
            if (charIndex >= word.Length)
            {
                return isWord;
            }

            var chr = word[charIndex];

            TrieNode suffix;
            if (inner.TryGetValue(chr, out suffix) == false)
            {
                return false;
            }

            return suffix.Contains(word, charIndex + 1);
        }

        protected bool IsPrefix(string prefix, int charIndex = 0)
        {
            if (charIndex >= prefix.Length)
            {
                return true;
            }

            var chr = prefix[charIndex];

            TrieNode suffix;
            if (inner.TryGetValue(chr, out suffix) == false)
            {
                return false;
            }

            return suffix.IsPrefix(prefix, charIndex + 1);
        }
    }
}
