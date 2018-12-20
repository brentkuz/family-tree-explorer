using FamilyTreeExplorer.Business.Objects;
using FamilyTreeExplorer.Crosscutting.Enums;
using FamilyTreeExplorer.Test.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FamilyTreeExplorer.Test.Business.Objects
{   
    [TestClass]
    [TestCategory("FamilyTree_Unit")]
    public class FamilyTree_Unit
    {
        [TestMethod]
        public void AddInLaw_ValidInput()
        {
            var tree = TreeHelper.GetTree();
            var inlaw = new FamilyMember("Olaf", Gender.Male);

            tree.AddInLaw(inlaw);

            Assert.IsNotNull(tree.GetMemberById(inlaw.Id));

        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateMemberException))]
        public void AddInLaw_Duplicate()
        {
            var tree = TreeHelper.GetTree();
            var inlaw = new FamilyMember("Olaf", Gender.Male);

            tree.AddInLaw(inlaw);
            tree.AddInLaw(inlaw);
        }

        [TestMethod]
        public void AddChild_ValidInput()
        {
            var tree = TreeHelper.GetTree();
            var child = new FamilyMember("Olaf", Gender.Male);

            tree.AddChild(tree.Root, child);

            Assert.IsNotNull(tree.GetMemberById(child.Id));
            Assert.IsTrue(tree.Root.ChildExists(child));
            Assert.AreEqual(tree.Root, child.Parents);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidPartnershipException))]
        public void AddChild_InvalidPartnership()
        {
            var tree = TreeHelper.GetTree();
            var child = new FamilyMember("Olaf", Gender.Male);

            tree.AddChild(new Partnership(new FamilyMember("t1", Gender.Male), new FamilyMember("t2", Gender.Female)), child);
        }
        [TestMethod]
        [ExpectedException(typeof(ExistingParentsReferenceException))]
        public void AddChild_ExistingParentsReference()
        {
            var tree = TreeHelper.GetTree();
            var child = new FamilyMember("Olaf", Gender.Male);
            child.Parents = new Parentship();

            tree.AddChild(tree.Root, child);
        }

        [TestMethod]
        public void AddNonPartnershipChild_ValidInput()
        {
            var tree = TreeHelper.GetTree();
            var child = new FamilyMember("Olaf", Gender.Male);
            var parent = tree.Root.Partner1;

            tree.AddNonPartnershipChild(parent, child);

            Assert.IsTrue(parent.NonPartnership.Children.Contains(child));
            Assert.AreEqual(parent.NonPartnership, child.Parents);
        }

        [TestMethod]
        [ExpectedException(typeof(ExistingParentsReferenceException))]
        public void AddNonPartnershipChild_ExistingParentsReference()
        {
            var tree = TreeHelper.GetTree();
            var child = new FamilyMember("Olaf", Gender.Male);
            child.Parents = new Parentship();

            tree.AddNonPartnershipChild(tree.Root.Partner1, child);
        }

        [TestMethod]
        [ExpectedException(typeof(ExistingParentsReferenceException))]
        public void AddNonPartnershipChild_DuplicateChild()
        {
            var tree = TreeHelper.GetTree();
            var child = new FamilyMember("Olaf", Gender.Male);
            var parent = tree.Root.Partner1;

            tree.AddNonPartnershipChild(parent, child);
            tree.AddNonPartnershipChild(parent, child);
        }

        [TestMethod]
        [ExpectedException(typeof(NotInFamilyTreeException))]
        public void AddNonPartnershipChild_InvalidParent()
        {
            var tree = TreeHelper.GetTree();
            var child = new FamilyMember("Olaf", Gender.Male);
            var parent = new FamilyMember("Tim", Gender.Male);
            
            tree.AddNonPartnershipChild(parent, child);
        }

        [TestMethod]
        public void AddPartnership_ValidInput()
        {
            var tree = TreeHelper.GetTree();
            var p1 = new FamilyMember("Olaf", Gender.Male);
            var p2 = new FamilyMember("Nancy", Gender.Female);

            tree.AddChild(tree.Root, p1);
            tree.AddInLaw(p2);

            var partnership = tree.AddPartnership(p1, p2);

            Assert.AreEqual(p1, partnership.Partner1);
            Assert.AreEqual(p2, partnership.Partner2);
            Assert.IsTrue(p1.Partnerships.Contains(partnership));
            Assert.IsTrue(p2.Partnerships.Contains(partnership));
        }

        [TestMethod]
        [ExpectedException(typeof(NotInFamilyTreeException))]
        public void AddPartnership_PartnerDoesNotExist()
        {
            var tree = TreeHelper.GetTree();
            var p1 = new FamilyMember("Olaf", Gender.Male);
            var p2 = new FamilyMember("Nancy", Gender.Female);

            tree.AddChild(tree.Root, p1);

            var partnership = tree.AddPartnership(p1, p2);
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateParntershipException))]
        public void AddPartnership_Duplicate()
        {
            var tree = TreeHelper.GetTree();
            var p1 = new FamilyMember("Olaf", Gender.Male);
            var p2 = new FamilyMember("Nancy", Gender.Female);

            tree.AddChild(tree.Root, p1);
            tree.AddInLaw(p2);

            tree.AddPartnership(p1, p2);
            tree.AddPartnership(p1, p2);
        }

   
    }
}
