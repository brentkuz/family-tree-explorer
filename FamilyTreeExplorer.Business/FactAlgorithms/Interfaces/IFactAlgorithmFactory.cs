using FamilyTreeExplorer.Business.FamilyTree;
using FamilyTreeExplorer.Business.FamilyTree.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTreeExplorer.Business.FactAlgorithms.Interfaces
{
    public interface IFactAlgorithmFactory
    {
        IFindBasicRelationships CreateFindBasicRelationships(IFamilyTree tree, IFamilyMember source);
        IResolveRelationshipNames CreateResolveRelationships(IFamilyTree tree, IFamilyMember source);
    }
}
