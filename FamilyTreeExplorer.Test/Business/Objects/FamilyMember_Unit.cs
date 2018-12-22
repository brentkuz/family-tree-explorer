using FamilyTreeExplorer.Business.FamilyTree;
using FamilyTreeExplorer.Crosscutting.Enums;
using FamilyTreeExplorer.Test.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTreeExplorer.Test.Business.FamilyTree
{
    [TestClass]
    [TestCategory("FamilyMember_Unit")]
    public class FamilyMember_Unit
    {
        [TestMethod]
        public void Enumerable_EnumeratesAllChildBearingBases()
        {
            var tree = TreeHelper.GetRootTree();

            tree.AddChild(tree.Root, new FamilyMember("Timmy", Gender.Male));
            tree.AddChild(tree.Root, new FamilyMember("Nancy", Gender.Female));
            tree.AddNonPartnershipChild(tree.Root.Partner1, new FamilyMember("Homer", Gender.Male));

            int count = 0;
            foreach (var child in tree.Root.Partner1)
                count++;

            Assert.AreEqual(2, count);
        }
    }
}
