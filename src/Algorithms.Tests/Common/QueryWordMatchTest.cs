using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Algorithms.Tests.Common
{
    public class QueryWordMatchTest
    {
        /*
            Question given a query, find if any word in word list matches
            . char is a wild card for any 1 character (exactly one)
            eg. [ cat, bat, rat, drat, dart, drab ]
            cat -> true
            c.t -> true
            .at -> true
            ..t -> true
            d..t -> true
            dr.. -> true
            ... -> true
            .... -> true
            ..... -> false
            h.t -> false
            c. -> false
        */
        [Theory]
        [InlineData(new string[] { "cat", "bat", "rat", "drat", "dart", "drab" }, "cat", true)]
        [InlineData(new string[] { "cat", "bat", "rat", "drat", "dart", "drab" }, "c.t", true)]
        [InlineData(new string[] { "cat", "bat", "rat", "drat", "dart", "drab" }, ".at", true)]
        [InlineData(new string[] { "cat", "bat", "rat", "drat", "dart", "drab" }, "d..t", true)]
        [InlineData(new string[] { "cat", "bat", "rat", "drat", "dart", "drab" }, "dr..", true)]
        [InlineData(new string[] { "cat", "bat", "rat", "drat", "dart", "drab" }, "...", true)]
        [InlineData(new string[] { "cat", "bat", "rat", "drat", "dart", "drab" }, "....", true)]
        [InlineData(new string[] { "cat", "bat", "rat", "drat", "dart", "drab" }, ".....", false)]
        [InlineData(new string[] { "cat", "bat", "rat", "drat", "dart", "drab" }, "h.t", false)]
        [InlineData(new string[] { "cat", "bat", "rat", "drat", "dart", "drab" }, "c.", false)]
        public void should_query_word(string[] wordList, string query, bool expectedOutput)
        {
            // Algorithm
            //   Setup -> build a trie out of word list
            //   IsMatch -> Use trie to see if query matches, if . encountered then skip a character

            // Arrange
            var trie = new Trie();
            trie.Setup(wordList);

            // Act
            var isMatch = trie.IsMatch(query);

            // Assert
            Assert.Equal(expectedOutput, isMatch);

        }

        private class Trie
        {
            public Dictionary<char, Trie> Dictionary;
            public Trie()
            {
                Dictionary = new Dictionary<char, Trie>();
            }

            public void Setup(string[] wordList)
            {
                foreach(var word in wordList)
                {
                    AddWord(word);
                }
            }

            public void AddWord(string word)
            {
                Trie pointer = this;
                foreach(char c in word)
                {
                    if (!pointer.Dictionary.ContainsKey(c))
                    {
                        pointer.Dictionary.Add(c, new Trie());
                    }
                    pointer = pointer.Dictionary[c];
                }
                pointer.Dictionary.Add('*', null);
            }

            public bool IsMatch(string query)
            {
                return IsMatch(this, query, 0);
            }

            private bool IsMatch(Trie trie, string query, int index)
            {
                // . has to skip
                // exit conditions are 
                //      query.length reached but next char is not * (query is shorter than expected word)
                //      query.length not reached and trie does not contain the current character (trie does not contain matching pattern)
                //      if character . is reached then need to iterate every char at level

                if (index >= query.Length)
                {
                    return trie.Dictionary.ContainsKey('*');
                }
                else if (query[index] == '.')
                {
                    foreach (var c in trie.Dictionary.Keys)
                    {
                        if (c != '*')
                        {
                            if (IsMatch(trie.Dictionary[c], query, index + 1))
                            {
                                return true;
                            }
                        }
                    }
                    return false;
                }
                else if (!trie.Dictionary.ContainsKey(query[index]))
                {
                    return false;
                }

                return IsMatch(trie.Dictionary[query[index]], query, index + 1);
            }
        }
    }
}
