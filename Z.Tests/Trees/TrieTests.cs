using System.Collections.Generic;
using System.Linq;
using Xunit;
using Z.Trees;

namespace Z.Tests.Trees
{
    public class TrieTests
    {
        private readonly Trie sut = new Trie();

        [Theory]
        [MemberData(nameof(AddAndContainsWorkTogetherData))]
        public void AddAndContainsWorkTogether(string[] words)
        {
            // Arrange
            var knownWords = new List<string>();
            foreach (var word in words)
            {
                // Assume
                Assert.False(sut.Contains(word));
                knownWords.Add(word);

                // Act
                sut.Add(word);

                // Assert
                foreach (var w in words)
                    Assert.Equal(knownWords.Contains(w), sut.Contains(w));
            }
        }

        public static IEnumerable<object[]> AddAndContainsWorkTogetherData =>
            new List<object[]>
            {
                new object[] { new [] { "Alpha" } },
                new object[] { new [] { "Alpha", "Beta" } },
                new object[] { new [] { "Alpha", "Beta", "Astra" } }
            };

        [Theory]
        [MemberData(nameof(AddAndIsPrefixWorkTogetherData))]
        public void AddAndIsPrefixWorkTogether(string word, string[] nonPrefixes)
        {
            // Assume
            for (var len = 1; len <= word.Length; len++)
                Assert.False(sut.IsPrefix(word.Substring(0, len)));

            // Act
            sut.Add(word);

            // Assert
            Assert.True(sut.IsPrefix(string.Empty));

            for (var len = 1; len <= word.Length; len++)
                Assert.True(sut.IsPrefix(word.Substring(0, len)));

            foreach (var nonPrefix in nonPrefixes)
                Assert.False(sut.IsPrefix(nonPrefix));
        }

        public static IEnumerable<object[]> AddAndIsPrefixWorkTogetherData =>
            new List<object[]>
            {
                new object[] { "Alpha", new [] { "X", "Y", "Z" } },
                new object[] { "Beta", new [] { "A", "BB" } }
            };
    }
}
