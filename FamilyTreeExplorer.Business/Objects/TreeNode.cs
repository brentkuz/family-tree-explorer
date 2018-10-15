using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTreeExplorer.Business.Objects
{
    public abstract class TreeNode : IEquatable<TreeNode>
    {
        public Guid Id { get; set; }

        public bool Equals(TreeNode other)
        {
            return this.Id == other.Id;
        }
    }
}
