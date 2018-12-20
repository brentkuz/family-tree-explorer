using System;
using System.Collections.Generic;
using System.Text;
using FamilyTreeExplorer.Business.Objects;

namespace FamilyTreeExplorer.Business.FactAlgorithms
{
    public class ResolveRelationshipNames : FactAgorithmBase
    {
        public ResolveRelationshipNames(FamilyTree tree) : base(tree, null)
        {
        }

        protected override void Execute()
        {
            foreach(var memb in tree)
            {

            }
        }
    }
}
