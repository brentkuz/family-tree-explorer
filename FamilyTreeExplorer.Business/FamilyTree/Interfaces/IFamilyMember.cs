using System;
using System.Collections;
using System.Collections.Generic;
using FamilyTreeExplorer.Business.FamilyTree.Interfaces;
using FamilyTreeExplorer.Business.FamilyTree.Relationships;
using FamilyTreeExplorer.Crosscutting.Enums;

namespace FamilyTreeExplorer.Business.FamilyTree.Interfaces
{
    public interface IFamilyMember : IEnumerable, IFactual
    {
        Guid Id { get; }
        Dictionary<FactType, Fact> Facts { get; set; }
        Gender Gender { get; set; }
        string Name { get; set; }
        IParentship NonPartnership { get; set; }
        IParentship Parents { get; set; }
        List<IPartnership> Partnerships { get; set; }
        
        bool HasChildren();
        bool HasPartnerships();
    }
}