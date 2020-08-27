using Algorithms.Tests.DataStructures.Node;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Algorithms.Tests.Cracking.LinkedLists
{
    public class _1_RemoveDuplicates
    {
        /// <summary>
        /// Remove duplicates in a linked list
        /// </summary>
        public _1_RemoveDuplicates()
        {

        }

        [Theory]
        [InlineData(new int[] { 1, 2, 1 })]
        [InlineData(new int[] { 2, 2 })]
        [InlineData(new int[0])]
        [InlineData(new int[] { 1 })]
        [InlineData(new int[] { 1, 2, 1, 2, 1 })]
        public void should_contain_no_duplicates(int[] array)
        {
            // Arrange
            var node = Node<int>.Create(array);

            // Act
            RemoveDuplicateNodes(node);

            // Assert
            Assert.False(CheckIfHasDuplicate(node));
        }

        private bool CheckIfHasDuplicate(Node<int> head)
        {
            var hashSet = new HashSet<int>();
            while(head != null)
            {
                if (hashSet.Contains(head.Value))
                {
                    return true;
                }
                else
                {
                    hashSet.Add(head.Value);
                    head = head.Next;
                }
            }
            return false;
        }


        // Test Case 1 : [ 1, 2, 1 ]
        //  First : current =  { 0: 1 }, hashSet { 2 }, previous = { 0: 1}, current = { 1: 2 }
        //  Second : current = { 1: 2 }, hashSet { 2, 1 }, previous = { 1: 2 }, current = { 2: 1 }
        //  Third : current = { 2: 1 }, hashSet contains 1, previous.Next = null, current = null
        //  Fourth : exit loop
        // Test Case 2 : [ 2, 2 ] => 
        //  first pass : current = { 0: 2 } , hashSet = { 2 }, previous = { 0: 2 }, current = { 1: 2}
        //  second pass : current = { 1: 2 }, hashSet = { 2 } which contains 2 
        //                previous.Next null
        //                current = null
        //  third pass : exit loop
        // Test Case 3 : [ ] => nothing happens since loop never entered
        // Test Case 4 : [ 1 ] => current = 1, previous = 1 then current set to null
        // Test Case 5 : [ 1, 2, 1, 2, 1 ]
        //  First : current =  { 0: 1 }, hashSet { 2 }, previous = { 0: 1}, current = { 1: 2 }
        //  Second : current = { 1: 2 }, hashSet { 2, 1 }, previous = { 1: 2 }, current = { 2: 1 }
        //  Third : current = { 2: 1 }, hashSet contains 1, previous.Next = { 3: 2 }, previous = { 1: 2 }, current = { 3: 2 }
        //  Fourth : current = { 3: 2 }, hashSet contains 2, previous.Next = { 4: 1}, previous = { 1: 2 }, current = { 4: 1 }
        //  Fifth : current { 4: 1}, hashSet contains 1, previous.Next = null, previous = { 1: 2 }, current = null
        //  Sixth : exit loop
        private void RemoveDuplicateNodes(Node<int> head)
        {
            var current = head;
            var previous = (Node<int>)null;
            var hashSet = new HashSet<int>();
            
            while(current != null)
            {
                if (hashSet.Contains(current.Value))
                {
                    previous.Next = current.Next;
                    
                }
                else
                {
                    hashSet.Add(current.Value);
                    previous = current;
                }
                current = current.Next;
            }
        }
    }
}
