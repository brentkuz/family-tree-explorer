using FamilyTreeExplorer.Business.Objects;
using FamilyTreeExplorer.Business.Objects.Interfaces;
using FamilyTreeExplorer.Crosscutting.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTreeExplorer.Business.FactAlgorithms
{
    public abstract class FactAgorithmBase
    {
        protected IFamilyTree tree;
        protected FamilyMember source;
        public FactAgorithmBase(IFamilyTree tree, FamilyMember source)
        {
            if (tree == null || tree.Root == null)
                throw new ArgumentNullException("Tree cannot be null.");
            if (source != null && !tree.MemberExists(source))
                throw new NotInFamilyTreeException(source);

            this.tree = tree;
            if (source != null)
            {
                source.AddFact(FactType.XPosition, 0);
                source.AddFact(FactType.XPosition, 0);
                this.source = source;
            }
            
        }

        //Algorithm to be implemented in derived classes
        public abstract void Execute();
    }

    public class InvalidSourceException : Exception
    {
        public InvalidSourceException() { }
        public InvalidSourceException(FamilyMember member)
            : base(string.Format("Source family member Id:{0} is an invalid starting point for the algorithm.", member.Id)) { }
        public InvalidSourceException(string message) : base(message) { }
    }
}
