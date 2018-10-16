using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTreeExplorer.Business.Objects
{
    public abstract class IdentityBase : IEquatable<IdentityBase>
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
