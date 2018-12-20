using FamilyTreeExplorer.Business.FactAlgorithms;
using FamilyTreeExplorer.Test.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTreeExplorer.Test.FactAlorithms
{
    [TestClass]
    [TestCategory("FindBasicRelationships_Unit")]
    public class FindBasicRelationships_Unit
    {
        [TestMethod]
        public void Execute_MarksAllMembersInTree()
        {
            var tree = TreeHelper.GetTree();

            //case 1 
            var source = tree.Root.Children[0];
            var find = new FindBasicRelationships(tree, source);
            Assert.AreEqual(tree.Count, find.MarkedMembers.Count);

            //case 2
            source = tree.Root.Children[1].Partnerships[0].Children[0];
            find = new FindBasicRelationships(tree, source);
            Assert.AreEqual(tree.Count, find.MarkedMembers.Count);

            //case 3
            source = tree.Root.Partner1;
            find = new FindBasicRelationships(tree, source);
            Assert.AreEqual(tree.Count, find.MarkedMembers.Count);
        }
    }
}
