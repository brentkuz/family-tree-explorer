using FamilyTreeExplorer.Business.FactAlgorithms.Interfaces;
using FamilyTreeExplorer.Business.FamilyTree.Interfaces;
using FamilyTreeExplorer.Crosscutting.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTreeExplorer.Business.FactAlgorithms.RelationshipNameResolvers.DirectLineageSubResolvers
{
    public class SiblingSubResolver : ChainedSubResolver, ISiblingSubResolver
    {
        public SiblingSubResolver(IChildSubResolver successor) : base(successor, default(int))
        {
        }

        public override string Handle(IFamilyMember source, IFamilyMember target)
        {
            if (target.GetFactValue<int>(FactType.YPosition) == 0)
            {
                return RelationshipType.Sibling.ToString();
            }
            else
            {
                return successor.Handle(source, target);
            }
        }
    }
}
