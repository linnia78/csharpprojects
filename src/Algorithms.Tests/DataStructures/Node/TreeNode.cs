using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.Tests.DataStructures.Node
{
    public class TreeNode<T>
    {
        public T Value;
        public List<TreeNode<T>> Children;
        public TreeNode<T> Left { get; set; }
        public TreeNode<T> Right { get; set; }
        public TreeNode(T value)
        {
            this.Value = value;
        }

        public TreeNode<T> AddChild(T value)
        {
            if (Children == null)
            {
                Children = new List<TreeNode<T>>();
            }

            var child = new TreeNode<T>(value);
            Children.Add(child);
            return child;
        }

        public int GetMaxHeight() => GetMaxHeightHelper(this);
        private int GetMaxHeightHelper(TreeNode<T> node)
        {
            if (node == null) { return 0; }
            return 1 + Math.Max(GetMaxHeightHelper(node.Left), GetMaxHeightHelper(node.Right));
        }
    }
}
