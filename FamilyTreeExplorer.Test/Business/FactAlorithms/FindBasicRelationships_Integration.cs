using FamilyTreeExplorer.Business.FactAlgorithms;
using FamilyTreeExplorer.Business.FamilyTree;
using FamilyTreeExplorer.Business.FamilyTree.Interfaces;
using FamilyTreeExplorer.Business.FamilyTree.Relationships;
using FamilyTreeExplorer.Crosscutting.Enums;
using FamilyTreeExplorer.Test.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTreeExplorer.Test.FactAlorithms
{
    [TestClass]
    [TestCategory("FindBasicRelationships_Integration")]
    public class FindBasicRelationships_Integration
    {
        FamilyMember greg = new FamilyMember("Greg", Gender.Male),
            pam = new FamilyMember("Pam", Gender.Female),
            jeff = new FamilyMember("Jeff", Gender.Male),
            brent = new FamilyMember("Brent", Gender.Male),
            kyle = new FamilyMember("Kyle", Gender.Male),
            jaclyn = new FamilyMember("Jaclyn", Gender.Female),
            roxi = new FamilyMember("Roxi", Gender.Female),
            della = new FamilyMember("Della", Gender.Female),
            aura = new FamilyMember("Aura", Gender.Female),
            ping = new FamilyMember("Ping", Gender.Male),
            guy = new FamilyMember("Guy", Gender.Male),
            nancy = new FamilyMember("Nancy", Gender.Female),
            timmy = new FamilyMember("Timmy", Gender.Male),
            leroy = new FamilyMember("Leroy", Gender.Male);

        IFamilyTree tree = null;

        [TestInitialize]
        public void Init()
        {
            tree = new FamilyTree();
            tree.SetRoot(new Partnership(greg, pam), pam);
            var root = tree.Root;
            tree.AddChild(root, jeff);
            tree.AddChild(root, brent);
            tree.AddChild(root, kyle);
            tree.AddInLaw(jaclyn);
            tree.AddInLaw(aura);
            tree.AddInLaw(guy);
            tree.AddInLaw(nancy);

            var brentJaclyn = tree.AddPartnership(brent, jaclyn);
            tree.AddChild(brentJaclyn, roxi);
            tree.AddChild(brentJaclyn, della);

            var jeffAura = tree.AddPartnership(jeff, aura);

            tree.AddNonPartnershipChild(kyle, ping);

            var roxiGuy = tree.AddPartnership(roxi, guy);
            tree.AddChild(roxiGuy, timmy);

            var pingNancy = tree.AddPartnership(ping, nancy);
            tree.AddChild(pingNancy, leroy);
        }
        [TestMethod]
        public void Execute_MarksAllMembersInTree()
        {
            var tree = TreeHelper.GetTree();

            //case 1 
            var source = tree.Root.Children[0];
            var find = new FindBasicRelationships();
            find.Execute(tree, source);
            Assert.AreEqual(tree.Count, find.MarkedMembers.Count);

            //case 2
            source = tree.Root.Children[1].Partnerships[0].Children[0];
            find = new FindBasicRelationships();
            find.Execute(tree, source);
            Assert.AreEqual(tree.Count, find.MarkedMembers.Count);
            
            //case 3
            source = tree.Root.Partner1;
            find = new FindBasicRelationships();
            find.Execute(tree, source);
            Assert.AreEqual(tree.Count, find.MarkedMembers.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidSourceException))]
        public void Execute_SourceIsNotInLaw()
        {
            var tree = TreeHelper.GetTree();
            
            var source = tree.Root.Partner2;
            var find = new FindBasicRelationships();
            find.Execute(tree, source);
        }

        [TestMethod]
        public void Execute_SiblingHasCorrectRelativePosition()
        {
            var alg = new FindBasicRelationships();
            alg.Execute(tree, della);

            Assert.AreEqual(0, roxi.GetFactValue<int>(FactType.XPosition));
            Assert.AreEqual(0, roxi.GetFactValue<int>(FactType.YPosition));
        }

        [TestMethod]
        public void Execute_ParentHasCorrectRelativePosition()
        {
            var alg = new FindBasicRelationships();
            alg.Execute(tree, roxi);

            Assert.AreEqual(0, brent.GetFactValue<int>(FactType.XPosition));
            Assert.AreEqual(-1, brent.GetFactValue<int>(FactType.YPosition));
        }

        [TestMethod]
        public void Execute_AuntUncleHasCorrectRelativePosition()
        {
            var alg = new FindBasicRelationships();
            alg.Execute(tree, roxi);

            Assert.AreEqual(0, jeff.GetFactValue<int>(FactType.XPosition));
            Assert.AreEqual(-1, jeff.GetFactValue<int>(FactType.YPosition));
        }

        [TestMethod]
        public void Execute_GrandparentHasCorrectRelativePosition()
        {
            var alg = new FindBasicRelationships();
            alg.Execute(tree, roxi);

            Assert.AreEqual(0, greg.GetFactValue<int>(FactType.XPosition));
            Assert.AreEqual(-2, greg.GetFactValue<int>(FactType.YPosition));
        }

        [TestMethod]
        public void Execute_InLawHasSameRelativePositionAsSpouse()
        {
            var alg = new FindBasicRelationships();
            alg.Execute(tree, roxi);

            Assert.AreEqual(jeff.GetFactValue<int>(FactType.XPosition), aura.GetFactValue<int>(FactType.XPosition));
            Assert.AreEqual(jeff.GetFactValue<int>(FactType.YPosition), aura.GetFactValue<int>(FactType.YPosition));
        }

        [TestMethod]
        public void Execute_FirstCousinHasCorrectRelativePosition()
        {
            var alg = new FindBasicRelationships();
            alg.Execute(tree, ping);

            Assert.AreEqual(1, roxi.GetFactValue<int>(FactType.XPosition));
            Assert.AreEqual(0, roxi.GetFactValue<int>(FactType.YPosition));
        }

        [TestMethod]
        public void Execute_SecondCousinHasCorrectRelativePosition()
        {
            var alg = new FindBasicRelationships();
            alg.Execute(tree, timmy);

            Assert.AreEqual(2, leroy.GetFactValue<int>(FactType.XPosition));
            Assert.AreEqual(0, leroy.GetFactValue<int>(FactType.YPosition));
        }

        //First Cousin Once Removed - cousin is lower depth in tree
        [TestMethod]
        public void Execute_FirstCousinOnceRemovedCase1HasCorrectRelativePosition()
        {
            var alg = new FindBasicRelationships();
            alg.Execute(tree, timmy);

            Assert.AreEqual(1, ping.GetFactValue<int>(FactType.XPosition));
            Assert.AreEqual(-1, ping.GetFactValue<int>(FactType.YPosition));
        }

        //First Cousin Once Removed - cousin is higher depth in tree
        [TestMethod]
        public void Execute_FirstCousinOnceRemovedCase2HasCorrectRelativePosition()
        {
            var alg = new FindBasicRelationships();
            alg.Execute(tree, ping);

            Assert.AreEqual(1, timmy.GetFactValue<int>(FactType.XPosition));
            Assert.AreEqual(1, timmy.GetFactValue<int>(FactType.YPosition));
        }

        [TestMethod]
        public void Execute_AncestorsAboveSourceAreMarked()
        {
            var alg = new FindBasicRelationships();
            alg.Execute(tree, roxi);

            Assert.IsTrue(brent.HasFact(FactType.Ancestor));
            Assert.IsTrue(greg.HasFact(FactType.Ancestor));
            Assert.IsTrue(pam.HasFact(FactType.Ancestor));
        }

        [TestMethod]
        public void Execute_AncestorsBelowSourceAreMarked()
        {
            var alg = new FindBasicRelationships();
            alg.Execute(tree, greg);

            Assert.IsTrue(brent.HasFact(FactType.Ancestor));
            Assert.IsTrue(roxi.HasFact(FactType.Ancestor));
            Assert.IsTrue(timmy.HasFact(FactType.Ancestor));
        }
    }
}

