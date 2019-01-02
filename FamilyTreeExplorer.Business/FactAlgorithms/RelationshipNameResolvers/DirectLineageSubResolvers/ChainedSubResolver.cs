using FamilyTreeExplorer.Business.FactAlgorithms.Interfaces;
using FamilyTreeExplorer.Business.FamilyTree.Interfaces;
using FamilyTreeExplorer.Crosscutting.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FamilyTreeExplorer.Business.FactAlgorithms.RelationshipNameResolvers.DirectLineageSubResolvers
{
    public abstract class ChainedSubResolver : IChainedSubResolver
    {
        protected IChainedSubResolver successor;
        protected readonly int greatStartYPosition;

        public ChainedSubResolver(IChainedSubResolver successor, int greatStartYPosition)
        {
            this.successor = successor;
            this.greatStartYPosition = greatStartYPosition;
        }

        public abstract string Handle(IFamilyMember source, IFamilyMember target);

        protected virtual string GetGreatness(int yPosition)
        {
            return GetGreatCount(yPosition - greatStartYPosition);            
        }

        private string GetGreatCount(int count)
        {
            return string.Concat(Enumerable.Repeat(RelationshipModifier.Great.ToString() + " ", Math.Abs(count)));
        }
    }
}
