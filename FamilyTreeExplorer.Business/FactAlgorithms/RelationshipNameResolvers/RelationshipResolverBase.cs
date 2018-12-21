using FamilyTreeExplorer.Business.Objects;
using FamilyTreeExplorer.Crosscutting.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTreeExplorer.Business.FactAlgorithms.RelationshipNameResolvers
{
    public abstract class RelationshipResolverBase
    {
        private FamilyMember source;

        public RelationshipResolverBase(FamilyMember source)
        {
            this.source = source;
        }

        public int? MinXPosition { get; protected set; }
        public int? MaxXPosition { get; protected set; }
        public int? MinYPosition { get; protected set; }
        public int? MaxYPosition { get; protected set; }

        public abstract string Execute(FamilyMember target);
    }
}
