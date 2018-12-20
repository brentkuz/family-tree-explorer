using FamilyTreeExplorer.Business.FactAlgorithms;
using FamilyTreeExplorer.Business.Objects;
using FamilyTreeExplorer.Crosscutting.Enums;
using System;

namespace FamilyTreeExplorer.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
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
                ping = new FamilyMember("Ping", Gender.Male);

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

            tree.AddNonPartnershipChild(kyle, ping);

            //test
            var alg = new FindBasicRelationships(tree, greg);
            

            foreach (FamilyMember fm in tree)
                DisplayMemberWithFacts(fm);

            Console.ReadKey();
        }

        private static void DisplayMemberWithFacts(FamilyMember fm)
        {
            string facts = "";
            foreach(var f in fm.GetFacts())
            {
                facts += string.Format("{0}: {1}; ", f.Type, f.Value);
            }
            Console.WriteLine(fm.Name + " - " + facts);
        }
    }
}
