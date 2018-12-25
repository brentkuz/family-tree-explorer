using System;
using System.Collections.Generic;
using System.Text;
using FamilyTreeExplorer.Business.FactAlgorithms.Interfaces;
using FamilyTreeExplorer.Business.FamilyTree.Interfaces;
using FamilyTreeExplorer.Crosscutting.Enums;

namespace FamilyTreeExplorer.Business.FactAlgorithms.RelationshipNameResolvers
{
    public class CousinResolver : RelationshipResolverBase, ICousinResolver
    {        
        private const int MIN_X_POSITION = 1;

        public CousinResolver()
        {
            MinXPosition = MIN_X_POSITION;
        }

        public override string Execute(IFamilyMember source, IFamilyMember target)
        {
            return string.Format("{0}{1}", 
                GetCousinhoodPart(target.GetFactValue<int>(FactType.XPosition)),
                GetRemovalPart(target.GetFactValue<int>(FactType.YPosition)));
        }

        protected virtual string GetCousinhoodPart(int xPosition)
        {
            return string.Format("{0} {1}", xPosition, RelationshipType.Cousin.ToString());
        }

        protected virtual string GetRemovalPart(int yPosition)
        {
            return yPosition != 0 ? string.Format(" {0} Removed", Math.Abs(yPosition)) : string.Empty;
        }

    }
}
