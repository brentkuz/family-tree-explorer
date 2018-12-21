using FamilyTreeExplorer.Business.Objects.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTreeExplorer.Business.Objects.Relationships
{
    public abstract class ChildBearingBase : IdentityBase
    {
        public ChildBearingBase()
        {
        }
        public List<FamilyMember> Children { get; set; } = new List<FamilyMember>();
        public bool HasChildren()
        {
            return Children.Count > 0;
        }
        public bool ChildExists(FamilyMember member)
        {
            return Children.Contains(member);
        }
        public bool ChildExists(Guid id)
        {
            return Children.Exists(x => x.Id == id);
        }
    }
}
