using System;
using System.Collections.Generic;
using System.Text;
using FamilyTreeExplorer.Business.FactAlgorithms.Interfaces;
using FamilyTreeExplorer.Business.FamilyTree;
using FamilyTreeExplorer.Business.FamilyTree.Interfaces;

namespace FamilyTreeExplorer.Business.FactAlgorithms.RelationshipNameResolvers
{
    public class DirectLineageResolver : RelationshipResolverBase, IDirectLineageResolver
    {
        private const int MIN_X_POSITION = 0;
        private const int MAX_X_POSITION = 0;
        private const int MIN_Y_POSITION = 0;

        public DirectLineageResolver()
        {
            MinXPosition = MIN_X_POSITION;
            MaxXPosition = MAX_X_POSITION;
            MinYPosition = MIN_Y_POSITION;
        }

        public override string Execute(IFamilyMember source, IFamilyMember target)
        {
            return "yep";
        }
    }
}
