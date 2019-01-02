using FamilyTreeExplorer.Business.FactAlgorithms.Interfaces;
using FamilyTreeExplorer.Business.FamilyTree.Interfaces;
using FamilyTreeExplorer.Crosscutting.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTreeExplorer.Business.FactAlgorithms.RelationshipNameResolvers.DirectLineageSubResolvers
{
    public class AuntUncleSubResolver : ChainedSubResolver, IAuntUncleSubResolver
    {
        public AuntUncleSubResolver(ISpouseSubResolver successor) : base(successor, default(int))
        {
        }

        public override string Handle(IFamilyMember source, IFamilyMember target)
        {
            if (target.GetFactValue<int>(FactType.YPosition) == -1)
            {
                return target.Gender == Gender.Female ? RelationshipType.Aunt.ToString() : RelationshipType.Uncle.ToString();
            }
            else
            {
                return successor.Handle(source, target);
            }
        }
    }
}
