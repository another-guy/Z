using System.Collections.Generic;
using Xunit;
using Z.Trees;

namespace Z.Tests.Trees
{
    public class TrieTests
    {
        private readonly Trie<object> sut = new Trie<object>();

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
                Assert.False(sut.Contains(word));
                knownWords.Add(word);

                // Act
                sut.Add(word, values[index]);

                // Assert
                foreach (var w in words)
                    Assert.Equal(knownWords.Contains(w), sut.Contains(w));
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
                Assert.False(sut.IsPrefix(word.Substring(0, len)));

            // Act
            sut.Add(word, 1);

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
                sut.Add(word, values[index]);

                // Assert
                Assert.Equal(values[index], sut.Get(word));
            }
        }

        public static IEnumerable<object[]> AddAndGetWorkTogetherData =>
            new List<object[]>
            {
                new object[] { new [] { "Alpha" }, new [] { 1 } },
                new object[] { new [] { "Alpha", "Beta" }, new [] { 1, 2 } },
                new object[] { new [] { "Alpha", "Beta", "Astra" }, new[] { 1, 2, 3 } }
            };
    }
}
