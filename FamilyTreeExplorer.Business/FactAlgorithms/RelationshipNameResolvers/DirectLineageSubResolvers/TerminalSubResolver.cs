using FamilyTreeExplorer.Business.FactAlgorithms.Interfaces;
using FamilyTreeExplorer.Business.FamilyTree.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTreeExplorer.Business.FactAlgorithms.RelationshipNameResolvers.DirectLineageSubResolvers
{
    public class TerminalSubResolver : ChainedSubResolver, ITerminalSubResolver
    {
        public TerminalSubResolver() : base(null, default(int))
        {
        }

        public override string Handle(IFamilyMember source, IFamilyMember target)
        {
            throw new NoRelationshipResolverFoundException(target);
            
        }
    }
}
