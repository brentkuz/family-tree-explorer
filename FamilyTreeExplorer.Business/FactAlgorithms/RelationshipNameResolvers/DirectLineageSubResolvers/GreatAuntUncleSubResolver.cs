using FamilyTreeExplorer.Business.FactAlgorithms.Interfaces;
using FamilyTreeExplorer.Business.FamilyTree.Interfaces;
using FamilyTreeExplorer.Crosscutting.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTreeExplorer.Business.FactAlgorithms.RelationshipNameResolvers.DirectLineageSubResolvers
{
    public class GreatAuntUncleSubResolver : ChainedSubResolver, IGreatAuntUncleSubResolver
    {
        public GreatAuntUncleSubResolver(IParentSubResolver successor) : base(successor, -1)
        {
        }

        public override string Handle(IFamilyMember source, IFamilyMember target)
        {
            if (target.GetFactValue<int>(FactType.YPosition) < -1 && !target.HasFact(FactType.Ancestor))
            {
                return string.Format("{0}{1}", 
                    GetGreatness(target.GetFactValue<int>(FactType.YPosition)), 
                    target.Gender == Gender.Female ? RelationshipType.Aunt.ToString() : RelationshipType.Uncle.ToString());
            }
            else
            {
                return successor.Handle(source, target);
            }
        }
    }
}
