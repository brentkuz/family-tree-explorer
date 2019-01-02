using FamilyTreeExplorer.Business.FactAlgorithms.Interfaces;
using FamilyTreeExplorer.Business.FamilyTree.Interfaces;
using FamilyTreeExplorer.Crosscutting.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTreeExplorer.Business.FactAlgorithms.RelationshipNameResolvers.DirectLineageSubResolvers
{
    public class SpouseSubResolver : ChainedSubResolver, ISpouseSubResolver
    {
        public SpouseSubResolver(ISiblingSubResolver successor) : base(successor, default(int))
        {
        }

        public override string Handle(IFamilyMember source, IFamilyMember target)
        {
            if (target.GetFactValue<int>(FactType.YPosition) == 0 && source.IsMarriedTo(target))
            {
                return string.Format("{0}{1}",
                    source.IsDivorcedFrom(target) ? (RelationshipModifier.Ex.ToString() + "-") : string.Empty, 
                    RelationshipType.Spouse.ToString());                
            }
            else
            {
                return successor.Handle(source, target);
            }
        }
    }
}
