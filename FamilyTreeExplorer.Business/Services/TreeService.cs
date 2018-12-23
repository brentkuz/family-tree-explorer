using FamilyTreeExplorer.Business.FactAlgorithms.Interfaces;
using FamilyTreeExplorer.Business.FamilyTree.Interfaces;
using FamilyTreeExplorer.Business.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTreeExplorer.Business.Services
{
    public class TreeService : ITreeService
    {
        private IFamilyTreeProcessor treeProcessor;
        public TreeService(IFamilyTreeProcessor treeProcessor)
        {
            this.treeProcessor = treeProcessor;
        }

        public void GatherFacts(IFamilyTree tree, IFamilyMember source)
        {
            treeProcessor.Process(tree, source);
        }
    }
}
