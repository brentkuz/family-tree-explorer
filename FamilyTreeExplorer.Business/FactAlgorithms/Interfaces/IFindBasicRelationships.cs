using FamilyTreeExplorer.Business.Objects;
using FamilyTreeExplorer.Business.Objects.Interfaces;
using FamilyTreeExplorer.Business.Objects.Relationships;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTreeExplorer.Business.FactAlgorithms.Interfaces
{
    public interface IFindBasicRelationships : IExecutable
    {
        HashSet<IFamilyMember> MarkedMembers { get; set; }
        HashSet<ChildBearingBase> MarkedChildBearingBases { get; set; }
    }
}
