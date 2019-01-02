using System;
using System.Collections.Generic;
using System.Text;
using FamilyTreeExplorer.Business.FactAlgorithms.Interfaces;
using FamilyTreeExplorer.Business.FamilyTree.Interfaces;
using FamilyTreeExplorer.Crosscutting.Enums;

namespace FamilyTreeExplorer.Business.FactAlgorithms.RelationshipNameResolvers.DirectLineageSubResolvers
{
    public class GrandparentSubResolver : ChainedSubResolver, IGrandparentSubResolver
    {
        public GrandparentSubResolver(IGreatAuntUncleSubResolver successor) : base(successor, -2)
        {
        }

        public override string Handle(IFamilyMember source, IFamilyMember target)
        {
            if (target.GetFactValue<int>(FactType.YPosition) < -1 && target.HasFact(FactType.Ancestor))
            {
                return string.Format("{0}{1}", 
                    GetGreatness(target.GetFactValue<int>(FactType.YPosition)), 
                    RelationshipType.Grandparent.ToString());
            }
            else
            {
                return successor.Handle(source, target);
            }
        }
    }
}
