using FamilyTreeExplorer.Business.FactAlgorithms.RelationshipNameResolvers;
using FamilyTreeExplorer.Business.FamilyTree.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTreeExplorer.Test.Fakes
{
    public class TestRelationshipResolver : RelationshipResolverBase
    {
        private const int MAX_X_POSITION = 1;
        private const int MIN_X_POSITION = 1;
        private const int MIN_Y_POSITION = 1;
        private const int MAX_Y_POSITION = 1;

        public TestRelationshipResolver()
        {
            MinXPosition = MIN_X_POSITION;
            MaxXPosition = MAX_X_POSITION;
            MinYPosition = MIN_Y_POSITION;
            MaxYPosition = MAX_Y_POSITION;
        }
        public override string Execute(IFamilyMember source, IFamilyMember target)
        {
            throw new NotImplementedException();
        }
    }
}
