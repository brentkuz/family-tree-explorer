using FamilyTreeExplorer.Business.FamilyTree.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTreeExplorer.Business.FactAlgorithms.Interfaces
{
    public interface IExecutableAlgorithm
    {
        void Execute(IFamilyTree tree, IFamilyMember source = null);
    }
}
