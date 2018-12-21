using System;
using System.Collections.Generic;
using System.Text;
using FamilyTreeExplorer.Business.FactAlgorithms.Interfaces;
using FamilyTreeExplorer.Business.Objects;
using FamilyTreeExplorer.Business.Objects.Interfaces;
using FamilyTreeExplorer.Crosscutting.Enums;

namespace FamilyTreeExplorer.Business.FactAlgorithms
{
    public class ResolveRelationshipNames : FactAgorithmBase, IResolveRelationshipNames
    {
        public ResolveRelationshipNames(IFamilyTree tree, IFamilyMember source) : base(tree, source)
        {
        }

        public override void Execute()
        {
            foreach(IFamilyMember memb in tree)
            {
                if (!memb.HasFact(FactType.XPosition) || !memb.HasFact(FactType.YPosition))
                    throw new NoPositionFactsException(memb);


            }
        }

        
    }

    public class NoPositionFactsException : Exception
    {
        public NoPositionFactsException() { }
        public NoPositionFactsException(IFamilyMember member) 
            : base(string.Format(@"Family member Id:{0} cannot be resolved to a relationship name because 
                                    no position facts are associated.", member.Id)) { }
        public NoPositionFactsException(string message) : base(message) { }
    }
}
