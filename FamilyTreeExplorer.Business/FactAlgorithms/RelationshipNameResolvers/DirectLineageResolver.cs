using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FamilyTreeExplorer.Business.FactAlgorithms.Interfaces;
using FamilyTreeExplorer.Business.FamilyTree;
using FamilyTreeExplorer.Business.FamilyTree.Interfaces;
using FamilyTreeExplorer.Crosscutting.Enums;

namespace FamilyTreeExplorer.Business.FactAlgorithms.RelationshipNameResolvers
{
    public class DirectLineageResolver : RelationshipResolverBase, IDirectLineageResolver
    {
        private const int MIN_X_POSITION = 0;
        private const int MAX_X_POSITION = 0;        

        //Head of the sub-resolver chain
        protected IChainedSubResolver subResolverHead;

        public DirectLineageResolver(IGrandparentSubResolver subResolverHead)
        {
            MinXPosition = MIN_X_POSITION;
            MaxXPosition = MAX_X_POSITION;

            this.subResolverHead = subResolverHead;
        }

        public override string Execute(IFamilyMember source, IFamilyMember target)
        {
            //traverse resolver chain
            return subResolverHead.Handle(source, target);
        }        
        
    }
}
