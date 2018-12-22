using FamilyTreeExplorer.Business.FamilyTree.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTreeExplorer.Business.FamilyTree.Relationships
{
    public abstract class ChildBearingBase : IdentityBase, IChildBearingBase
    {
        public ChildBearingBase()
        {
        }
        public List<IFamilyMember> Children { get; set; } = new List<IFamilyMember>();
        public bool HasChildren()
        {
            return Children.Count > 0;
        }
        public bool ChildExists(IFamilyMember member)
        {
            return Children.Contains(member);
        }
        public bool ChildExists(Guid id)
        {
            return Children.Exists(x => x.Id == id);
        }
    }
}
