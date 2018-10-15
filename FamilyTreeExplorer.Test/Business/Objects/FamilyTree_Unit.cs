using FamilyTreeExplorer.Business.Objects;
using FamilyTreeExplorer.Crosscutting.Enums;
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
            var tree = GetTree();
            var inlaw = new FamilyMember("Olaf", Gender.Male);

            tree.AddInLaw(inlaw);

            Assert.IsNotNull(tree.GetMemberById(inlaw.Id));

        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateMemberException))]
        public void AddInLaw_Duplicate()
        {
            var tree = GetTree();
            var inlaw = new FamilyMember("Olaf", Gender.Male);

            tree.AddInLaw(inlaw);
            tree.AddInLaw(inlaw);
        }

        [TestMethod]
        public void AddChild_ValidInput()
        {
            var tree = GetTree();
            var child = new FamilyMember("Olaf", Gender.Male);

            tree.AddChild(tree.Root, child);

            Assert.IsNotNull(tree.GetMemberById(child.Id));
            Assert.IsTrue(tree.Root.ChildExists(child));
            var par1 = child.Parent1;
            var par2 = child.Parent2;
            Assert.AreEqual(tree.Root.Partner1, par1);
            Assert.AreEqual(tree.Root.Partner2, par2);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidPartnershipException))]
        public void AddChild_InvalidPartnership()
        {
            var tree = GetTree();
            var child = new FamilyMember("Olaf", Gender.Male);

            tree.AddChild(new Partnership(new FamilyMember("t1", Gender.Male), new FamilyMember("t2", Gender.Female)), child);
        }

        [TestMethod]
        public void AddNonPartnershipChild_ValidInput()
        {
            var tree = GetTree();
            var child = new FamilyMember("Olaf", Gender.Male);
            var parent = tree.Root.Partner1;

            tree.AddNonPartnershipChild(parent, child);

            Assert.IsTrue(parent.NonPartnershipChildren.Contains(child));
            Assert.AreEqual(parent, child.Parent1);
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateMemberException))]
        public void AddNonPartnershipChild_DuplicateChild()
        {
            var tree = GetTree();
            var child = new FamilyMember("Olaf", Gender.Male);
            var parent = tree.Root.Partner1;

            tree.AddNonPartnershipChild(parent, child);
            tree.AddNonPartnershipChild(parent, child);
        }

        [TestMethod]
        [ExpectedException(typeof(NotInFamilyTreeException))]
        public void AddNonPartnershipChild_InvalidParent()
        {
            var tree = GetTree();
            var child = new FamilyMember("Olaf", Gender.Male);
            var parent = new FamilyMember("Tim", Gender.Male);
            
            tree.AddNonPartnershipChild(parent, child);
        }

        [TestMethod]
        public void AddPartnership_ValidInput()
        {
            var tree = GetTree();
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
            var tree = GetTree();
            var p1 = new FamilyMember("Olaf", Gender.Male);
            var p2 = new FamilyMember("Nancy", Gender.Female);

            tree.AddChild(tree.Root, p1);

            var partnership = tree.AddPartnership(p1, p2);
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateParntershipException))]
        public void AddPartnership_Duplcate()
        {
            var tree = GetTree();
            var p1 = new FamilyMember("Olaf", Gender.Male);
            var p2 = new FamilyMember("Nancy", Gender.Female);

            tree.AddChild(tree.Root, p1);
            tree.AddInLaw(p2);

            tree.AddPartnership(p1, p2);
            tree.AddPartnership(p1, p2);
        }

        private FamilyTree GetRootTree()
        {
            FamilyMember greg = new FamilyMember("Greg", Gender.Male),
                pam = new FamilyMember("Pam", Gender.Female);

            var tree = new FamilyTree(new Partnership(greg, pam));
            var root = tree.Root;

            return tree;
        }
        private FamilyTree GetTree()
        {
            FamilyMember greg = new FamilyMember("Greg", Gender.Male),
                pam = new FamilyMember("Pam", Gender.Female),
                jeff = new FamilyMember("Jeff", Gender.Male),
                brent = new FamilyMember("Brent", Gender.Male),
                kyle = new FamilyMember("Kyle", Gender.Male),
                jaclyn = new FamilyMember("Jaclyn", Gender.Female),
                roxi = new FamilyMember("Roxi", Gender.Female),
                della = new FamilyMember("Della", Gender.Female),
                aura = new FamilyMember("Aura", Gender.Female);

            var tree = new FamilyTree(new Partnership(greg, pam));
            var root = tree.Root;
            tree.AddChild(root, jeff);
            tree.AddChild(root, brent);
            tree.AddChild(root, kyle);
            tree.AddInLaw(jaclyn);
            tree.AddInLaw(aura);

            var brentJaclyn = tree.AddPartnership(brent, jaclyn);
            tree.AddChild(brentJaclyn, roxi);
            tree.AddChild(brentJaclyn, della);

            var jeffAura = tree.AddPartnership(jeff, aura);

            return tree;
        }
    }
}
