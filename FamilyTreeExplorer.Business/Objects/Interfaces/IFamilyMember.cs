using System;
using System.Collections;
using System.Collections.Generic;
using FamilyTreeExplorer.Business.Objects.Interfaces;
using FamilyTreeExplorer.Business.Objects.Relationships;
using FamilyTreeExplorer.Crosscutting.Enums;

namespace FamilyTreeExplorer.Business.Objects.Interfaces
{
    public interface IFamilyMember : IEnumerable, IFactual
    {
        Guid Id { get; }
        Dictionary<FactType, Fact> Facts { get; set; }
        Gender Gender { get; set; }
        string Name { get; set; }
        Parentship NonPartnership { get; set; }
        Parentship Parents { get; set; }
        List<Partnership> Partnerships { get; set; }
        
        bool HasChildren();
        bool HasPartnerships();
    }
}