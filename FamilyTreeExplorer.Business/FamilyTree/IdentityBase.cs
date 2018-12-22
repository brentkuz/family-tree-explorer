using FamilyTreeExplorer.Business.FamilyTree.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTreeExplorer.Business.FamilyTree
{
    public abstract class IdentityBase : IEquatable<IdentityBase>, IIdentityBase
    {
        public IdentityBase()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }

        public bool Equals(IdentityBase other)
        {
            return this.Id == other.Id;
        }
    }
}
