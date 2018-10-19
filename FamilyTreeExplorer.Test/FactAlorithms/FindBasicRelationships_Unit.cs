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
            var source = tree.Root.Children[0];

            var find = new FindBasicRelationships(tree, source);

            Assert.AreEqual(tree.Count, find.MarkedMembers.Count);
        }
    }
}
