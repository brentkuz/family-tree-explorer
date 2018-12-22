using FamilyTreeExplorer.Business.FactAlgorithms.Interfaces;
using FamilyTreeExplorer.Business.FamilyTree;
using FamilyTreeExplorer.Business.FamilyTree.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTreeExplorer.Business.FactAlgorithms
{
    public class FactAlgorithmFactory : IFactAlgorithmFactory
    {
        public IFindBasicRelationships CreateFindBasicRelationships(IFamilyTree tree, IFamilyMember source)
        {
            return new FindBasicRelationships(tree, source);
        }

        public IResolveRelationshipNames CreateResolveRelationships(IFamilyTree tree, IFamilyMember source)
        {
            return new ResolveRelationshipNames(tree, source);
        }
    }
}
