using FamilyTreeExplorer.Business.FactAlgorithms;
using FamilyTreeExplorer.Business.FamilyTree;
using FamilyTreeExplorer.Business.FamilyTree.Interfaces;
using FamilyTreeExplorer.Business.FamilyTree.Relationships;
using FamilyTreeExplorer.Crosscutting.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTreeExplorer.Test.FactAlorithms
{
    [TestClass]
    [TestCategory("ResolveRelationshipNames_Integration")]
    public class ResolveRelationshipNames_Integration
    {
        IFamilyMember greg = new FamilyMember("Greg", Gender.Male),
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
        [ExpectedException(typeof(NoPositionFactsException))]
        public void Execute_NoPositionFacts()
        {
            var alg = new ResolveRelationshipNames();
            alg.Execute(tree, tree.Root.Partner1);
        }
        
        [TestMethod]
        [ExpectedException(typeof(NoRelationshipResolverFoundException))]
        public void Execute_NoRelationshipResolverFound()
        {
            DoFindBasicRelationships(greg);

            var alg = new ResolveRelationshipNames();
            alg.Execute(tree, tree.Root.Partner1);
        }



        private void DoFindBasicRelationships(IFamilyMember source)
        {
            new FindBasicRelationships().Execute(tree, source);
        }
    }
}
