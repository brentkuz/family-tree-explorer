using FamilyTreeExplorer.Business.FamilyTree;
using FamilyTreeExplorer.Business.FamilyTree.Interfaces;
using FamilyTreeExplorer.Business.FamilyTree.Relationships;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTreeExplorer.Business.FactAlgorithms.Interfaces
{
    public interface IFindBasicRelationships : IExecutableAlgorithm
    {
        HashSet<IFamilyMember> MarkedMembers { get; set; }
        HashSet<IChildBearingBase> MarkedChildBearingBases { get; set; }
    }
}
