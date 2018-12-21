using FamilyTreeExplorer.Business.FactAlgorithms.Interfaces;
using FamilyTreeExplorer.Business.Objects;
using FamilyTreeExplorer.Business.Objects.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTreeExplorer.Business.FactAlgorithms
{
    public class FactAlgorithmFactory : IFactAlgorithmFactory
    {
        public IFindBasicRelationships GetFindBasicRelationshipsAlgorithm(IFamilyTree tree, FamilyMember source)
        {
            return new FindBasicRelationships(tree, source);
        }

        public IResolveRelationshipNames GetResolveRelationshipsAlgorithm(IFamilyTree tree, FamilyMember source)
        {
            return new ResolveRelationshipNames(tree, source);
        }
    }
}
