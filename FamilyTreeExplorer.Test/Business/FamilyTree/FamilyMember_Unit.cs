using FamilyTreeExplorer.Business.FamilyTree;
using FamilyTreeExplorer.Business.FamilyTree.Relationships;
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

        [TestMethod]
        public void IsMarriedTo_ReturnsTrueForCurrentWife()
        {            
            var p1 = new FamilyMember("Tom", Gender.Male);
            var p2 = new FamilyMember("Nancy", Gender.Female);
            new Partnership(p1, p2);

            Assert.IsTrue(p1.IsMarriedTo(p2));
        }

        [TestMethod]
        public void IsMarriedTo_ReturnsFalseForNonWife()
        {
            var p1 = new FamilyMember("Tom", Gender.Male);
            var p2 = new FamilyMember("Nancy", Gender.Female);
       
            Assert.IsFalse(p1.IsMarriedTo(p2));
        }

        [TestMethod]
        public void IsDivorcedFrom_ReturnsFalseForCurrentWife()
        {
            var p1 = new FamilyMember("Tom", Gender.Male);
            var p2 = new FamilyMember("Nancy", Gender.Female);
            new Partnership(p1, p2);

            Assert.IsFalse(p1.IsDivorcedFrom(p2));
        }

        [TestMethod]
        public void IsDivorcedFrom_ReturnsFalseForNonWife()
        {
            var p1 = new FamilyMember("Tom", Gender.Male);
            var p2 = new FamilyMember("Nancy", Gender.Female);

            Assert.IsFalse(p1.IsDivorcedFrom(p2));
        }

        [TestMethod]
        public void IsDivorcedFrom_ReturnsTrueForExWife()
        {
            var p1 = new FamilyMember("Tom", Gender.Male);
            var p2 = new FamilyMember("Nancy", Gender.Female);
            var partnership = new Partnership(p1, p2);
            partnership.IsDivorced = true;

            Assert.IsTrue(p1.IsDivorcedFrom(p2));
        }
    }
}
