using FamilyTreeExplorer.Business.FactAlgorithms.Interfaces;
using FamilyTreeExplorer.Business.FamilyTree;
using FamilyTreeExplorer.Business.FamilyTree.Interfaces;
using FamilyTreeExplorer.Crosscutting.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTreeExplorer.Business.FactAlgorithms
{
    public abstract class FactAgorithmBase : IExecutableAlgorithm
    {
        protected IFamilyTree tree;
        protected IFamilyMember source;
        public FactAgorithmBase()
        {            
        }

        //Algorithm to be implemented in derived classes
        public virtual void Execute(IFamilyTree tree, IFamilyMember source = null)
        {
            if (tree == null || tree.Root == null)
                throw new ArgumentNullException("Tree cannot be null.");

            this.tree = tree;

            if (source != null)
            {
                if (!tree.MemberExists(source))
                    throw new NotInFamilyTreeException(source);
                //prevent source from being inlaw. Inlaws do not have parents, which
                //breaks traversal.
                if (source.HasFact(FactType.InLaw))
                    throw new InvalidSourceException(source);

                source.AddFact(FactType.XPosition, 0);
                source.AddFact(FactType.XPosition, 0);
                this.source = source;
            }

            Execute();
        }
        protected abstract void Execute();
    }

    public class InvalidSourceException : Exception
    {
        public InvalidSourceException() { }
        public InvalidSourceException(IFamilyMember member)
            : base(string.Format("Source family member Id:{0} is an invalid starting point for the algorithm.", member.Id)) { }
        public InvalidSourceException(string message) : base(message) { }
    }
}
