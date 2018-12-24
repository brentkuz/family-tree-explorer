using FamilyTreeExplorer.Business.FactAlgorithms.Interfaces;
using FamilyTreeExplorer.Business.FamilyTree;
using FamilyTreeExplorer.Business.FamilyTree.Interfaces;
using FamilyTreeExplorer.Crosscutting.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTreeExplorer.Business.FactAlgorithms.RelationshipNameResolvers
{
    public abstract class RelationshipResolverBase : IRelationshipResolver
    {
        public int? MinXPosition { get; protected set; }
        public int? MaxXPosition { get; protected set; }
        public int? MinYPosition { get; protected set; }
        public int? MaxYPosition { get; protected set; }

        public bool Equals(IRelationshipResolver other)
        {
            return MinXPosition == other.MinXPosition
                && MaxXPosition == other.MaxXPosition
                && MinYPosition == other.MinYPosition
                && MaxYPosition == other.MaxYPosition;
        }

        public abstract string Execute(IFamilyMember source, IFamilyMember target);

        public bool InPositionRange(int x, int y)
        {
            return (
                (!MinXPosition.HasValue || MinXPosition <= x)
                && (!MaxXPosition.HasValue || MaxXPosition >= x)
                && (!MinYPosition.HasValue || MinYPosition <= y)
                && (!MaxYPosition.HasValue || MaxYPosition >= y)
                );
        
        }
        
    }
}
