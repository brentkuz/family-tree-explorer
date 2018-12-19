using FamilyTreeExplorer.Business.Objects;
using FamilyTreeExplorer.Crosscutting.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTreeExplorer.Test.Helpers
{
    public static class TreeHelper
    {
        public static FamilyTree GetRootTree()
        {
            FamilyMember greg = new FamilyMember("Greg", Gender.Male),
                pam = new FamilyMember("Pam", Gender.Female);

            var tree = new FamilyTree(new Partnership(greg, pam), pam);
            var root = tree.Root;

            return tree;
        }
        public static FamilyTree GetTree()
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

            var tree = new FamilyTree(new Partnership(greg, pam), pam);
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
