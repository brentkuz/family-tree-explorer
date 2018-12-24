using System;
using System.Collections.Generic;
using System.Text;
using FamilyTreeExplorer.Business.FactAlgorithms.Interfaces;
using FamilyTreeExplorer.Business.FamilyTree.Interfaces;
using FamilyTreeExplorer.Crosscutting.Enums;

namespace FamilyTreeExplorer.Business.FactAlgorithms.RelationshipNameResolvers
{
    public class CousinResolver : RelationshipResolverBase, ICousinResolver
    {
        private const int MIN_X_POSITION = 1;

        public CousinResolver()
        {
            MinXPosition = MIN_X_POSITION;
        }
        public override string Execute(IFamilyMember source, IFamilyMember target)
        {
            return RelationshipType.Cousin.ToString();
        }
    }
}
