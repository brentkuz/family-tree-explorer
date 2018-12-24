using System;
using System.Collections.Generic;
using System.Text;
using FamilyTreeExplorer.Business.FactAlgorithms.Interfaces;
using FamilyTreeExplorer.Business.FamilyTree.Interfaces;
using FamilyTreeExplorer.Crosscutting.Enums;

namespace FamilyTreeExplorer.Business.FactAlgorithms.RelationshipNameResolvers
{
    public class SelfResolver : RelationshipResolverBase, ISelfResolver
    {
        private const int MIN_X_POSITION = 0;
        private const int MAX_X_POSITION = 0;
        private const int MIN_Y_POSITION = 0;
        private const int MAX_Y_POSITION = 0;

        public SelfResolver()
        {
            MinXPosition = MIN_X_POSITION;
            MaxXPosition = MAX_X_POSITION;
            MinYPosition = MIN_Y_POSITION;
            MaxYPosition = MAX_Y_POSITION;
        }
        public override string Execute(IFamilyMember source, IFamilyMember target)
        {
            return RelationshipType.Self.ToString();
        }
    }
}
