using System;
using System.Collections.Generic;
using FamilyTreeExplorer.Business.FamilyTree.Interfaces;

namespace FamilyTreeExplorer.Business.FamilyTree.Interfaces
{
    public interface IChildBearingBase : IIdentityBase
    {
        List<IFamilyMember> Children { get; set; }

        bool ChildExists(Guid id);
        bool ChildExists(IFamilyMember member);
        bool HasChildren();
    }
}