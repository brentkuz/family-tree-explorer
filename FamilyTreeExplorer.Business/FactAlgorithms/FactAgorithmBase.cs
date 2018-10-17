using FamilyTreeExplorer.Business.Objects;
using FamilyTreeExplorer.Crosscutting.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTreeExplorer.Business.FactAlgorithms
{
    public abstract class FactAgorithmBase
    {
        protected FamilyTree tree;
        protected FamilyMember source;
        public FactAgorithmBase(FamilyTree tree, FamilyMember source)
        {
            if (tree == null || tree.Root == null)
                throw new ArgumentNullException("Tree cannot be null.");
            if (!tree.MemberExists(source))
                throw new NotInFamilyTreeException(source);

            this.tree = tree;
            source.AddFact(FactType.XPosition, 0);
            source.AddFact(FactType.XPosition, 0);
            this.source = source;

            Execute();
        }
        protected abstract void Execute();
    }
}
