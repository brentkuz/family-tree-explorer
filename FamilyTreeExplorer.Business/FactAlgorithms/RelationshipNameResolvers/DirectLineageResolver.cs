using System;
using System.Collections.Generic;
using System.Text;
using FamilyTreeExplorer.Business.Objects;

namespace FamilyTreeExplorer.Business.FactAlgorithms.RelationshipNameResolvers
{
    public class DirectLineageResolver : RelationshipResolverBase
    {
        public DirectLineageResolver(FamilyMember source) : base(source)
        {
            MinXPosition = 0;
            MaxXPosition = 0;
            MinYPosition = 0;
        }

        public override string Execute(FamilyMember target)
        {
            throw new NotImplementedException();
        }
    }
}
