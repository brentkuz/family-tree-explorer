using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FamilyTreeExplorer.Business.FactAlgorithms.Interfaces;
using FamilyTreeExplorer.Business.FamilyTree;
using FamilyTreeExplorer.Business.FamilyTree.Interfaces;
using FamilyTreeExplorer.Crosscutting.Enums;

namespace FamilyTreeExplorer.Business.FactAlgorithms
{
    public class ResolveRelationshipNames : FactAgorithmBase, IResolveRelationshipNames
    {
        private List<IRelationshipResolver> resolvers = new List<IRelationshipResolver>();
        public ResolveRelationshipNames(params IRelationshipResolver[] resolversToUse)
        {
            foreach(var resolver in resolversToUse)
            {
                resolvers.Add(resolver);
            }
        }

        protected override void Execute()
        {
            foreach(IFamilyMember memb in tree)
            {
                if (!memb.HasFact(FactType.Relationship))
                {
                    if(memb == source)
                    {
                        memb.AddFact(FactType.Relationship, RelationshipType.Self);
                        continue;
                    }
                    if (!memb.HasFact(FactType.XPosition) || !memb.HasFact(FactType.YPosition))
                        throw new NoPositionFactsException(memb);

                    int xPosition = memb.GetFactValue<int>(FactType.XPosition),
                        yPosition = memb.GetFactValue<int>(FactType.YPosition);
                   
                    var resolver = resolvers.Where(x => x.InPositionRange(xPosition, yPosition)).FirstOrDefault();

                    if (resolver == null)
                        throw new NoRelationshipResolverFoundException(memb);
                  
                    memb.AddFact(FactType.Relationship, resolver.Execute(source, memb));               
                }
            }
        }

        
    }

    public class NoPositionFactsException : Exception
    {
        public NoPositionFactsException() { }
        public NoPositionFactsException(IFamilyMember member) 
            : base(string.Format(@"Family member Id:{0} cannot be resolved to a relationship name because 
                                    there are missing position facts.", member.Id)) { }
        public NoPositionFactsException(string message) : base(message) { }
    }
    public class NoRelationshipResolverFoundException : Exception
    {
        public NoRelationshipResolverFoundException() { }
        public NoRelationshipResolverFoundException(IFamilyMember member)
            : base(string.Format(@"Family member Id:{0} cannot be resolved to a relationship name because 
                                    their position facts are not in range for any registered resolvers.", member.Id)) { }
        public NoRelationshipResolverFoundException(string message) : base(message) { }
    }
}
