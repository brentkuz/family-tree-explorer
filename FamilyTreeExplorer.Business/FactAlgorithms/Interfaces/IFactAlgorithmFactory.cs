using FamilyTreeExplorer.Business.Objects;
using FamilyTreeExplorer.Business.Objects.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTreeExplorer.Business.FactAlgorithms.Interfaces
{
    public interface IFactAlgorithmFactory
    {
        IFindBasicRelationships GetFindBasicRelationshipsAlgorithm(IFamilyTree tree, FamilyMember source);
        IResolveRelationshipNames GetResolveRelationshipsAlgorithm(IFamilyTree tree, FamilyMember source);
    }
}
