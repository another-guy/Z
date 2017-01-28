using System;
using System.Collections.Generic;
using Xunit;
using Z.Trees;

namespace Z.Tests.Trees
{
    public class TrieTests
    {
        private readonly Trie<object> _sut = new Trie<object>();

        [Theory]
        [MemberData(nameof(AddAndContainsWorkTogetherData))]
        public void AddAndContainsWorkTogether(string[] words, int[] values)
        {
            // Arrange
            var knownWords = new List<string>();
            for (var index = 0; index < words.Length; index++)
            {
                var word = words[index];

                // Assume
                Assert.False(_sut.Contains(word));
                knownWords.Add(word);

                // Act
                _sut.Add(word, values[index]);

                // Assert
                foreach (var w in words)
                    Assert.Equal(knownWords.Contains(w), _sut.Contains(w));
            }
        }

        public static IEnumerable<object[]> AddAndContainsWorkTogetherData =>
            new List<object[]>
            {
                new object[] { new [] { "Alpha" }, new [] { 1 } },
                new object[] { new [] { "Alpha", "Beta" }, new [] { 1, 2 } },
                new object[] { new [] { "Alpha", "Beta", "Astra" }, new[] { 1, 2, 3 } }
            };

        [Theory]
        [MemberData(nameof(AddAndIsPrefixWorkTogetherData))]
        public void AddAndIsPrefixWorkTogether(string word, string[] nonPrefixes)
        {
            // Assume
            for (var len = 1; len <= word.Length; len++)
                Assert.False(_sut.IsPrefix(word.Substring(0, len)));

            // Act
            _sut.Add(word, 1);

            // Assert
            Assert.True(_sut.IsPrefix(string.Empty));

            for (var len = 1; len <= word.Length; len++)
                Assert.True(_sut.IsPrefix(word.Substring(0, len)));

            foreach (var nonPrefix in nonPrefixes)
                Assert.False(_sut.IsPrefix(nonPrefix));
        }

        public static IEnumerable<object[]> AddAndIsPrefixWorkTogetherData =>
            new List<object[]>
            {
                new object[] { "Alpha", new [] { "PopWorks", "Y", "Z" } },
                new object[] { "Beta", new [] { "A", "BB" } }
            };

        [Theory]
        [MemberData(nameof(AddAndGetWorkTogetherData))]
        public void AddAndGetWorkTogether(string[] words, int[] values)
        {
            // Arrange
            var knownWords = new List<string>();
            for (var index = 0; index < words.Length; index++)
            {
                var word = words[index];

                // Assume
                knownWords.Add(word);

                // Act
                _sut.Add(word, values[index]);

                // Assert
                Assert.Equal(values[index], _sut.Get(word));
            }
        }

        public static IEnumerable<object[]> AddAndGetWorkTogetherData =>
            new List<object[]>
            {
                new object[] { new [] { "Alpha" }, new [] { 1 } },
                new object[] { new [] { "Alpha", "Beta" }, new [] { 1, 2 } },
                new object[] { new [] { "Alpha", "Beta", "Astra" }, new[] { 1, 2, 3 } }
            };

        [Fact]
        public void EnforceWhitespaceTrimmingWorks()
        {
            // Arrange
            var sut2 = new Trie<object>(enforceWhitespaceTrimming: true);

            // Act
            var caught = Assert.Throws<ArgumentException>(() => sut2.Add("   x", 1));

            // Assert
            Assert.Equal($"searchKey='   x' contains whitespaces that must be trimmed.", caught.Message);
        }
    }
}
