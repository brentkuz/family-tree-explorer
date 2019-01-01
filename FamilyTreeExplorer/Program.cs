using FamilyTreeExplorer.Business.FactAlgorithms;
using FamilyTreeExplorer.Business.FactAlgorithms.Interfaces;
using FamilyTreeExplorer.Business.FactAlgorithms.RelationshipNameResolvers;
using FamilyTreeExplorer.Business.FamilyTree;
using FamilyTreeExplorer.Business.FamilyTree.Interfaces;
using FamilyTreeExplorer.Business.FamilyTree.Relationships;
using FamilyTreeExplorer.Crosscutting.ConsoleApp.Utility;
using FamilyTreeExplorer.Crosscutting.Enums;
using System;
using Unity;

namespace FamilyTreeExplorer.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new UnityContainerFactory().GetContainer();
            
            var processor = container.Resolve<IFamilyTreeProcessor>();

            #region tree
            IFamilyMember greg = FamilyTreeFactory.CreateFamilyMember("Greg", Gender.Male),
                pam = FamilyTreeFactory.CreateFamilyMember("Pam", Gender.Female),
                jeff = FamilyTreeFactory.CreateFamilyMember("Jeff", Gender.Male),
                brent = FamilyTreeFactory.CreateFamilyMember("Brent", Gender.Male),
                kyle = FamilyTreeFactory.CreateFamilyMember("Kyle", Gender.Male),
                jaclyn = FamilyTreeFactory.CreateFamilyMember("Jaclyn", Gender.Female),
                roxi = FamilyTreeFactory.CreateFamilyMember("Roxi", Gender.Female),
                della = FamilyTreeFactory.CreateFamilyMember("Della", Gender.Female),
                aura = FamilyTreeFactory.CreateFamilyMember("Aura", Gender.Female),
                ping = FamilyTreeFactory.CreateFamilyMember("Ping", Gender.Male),
                guy = FamilyTreeFactory.CreateFamilyMember("Guy", Gender.Male),
                nancy = FamilyTreeFactory.CreateFamilyMember("Nancy", Gender.Female),
                timmy = FamilyTreeFactory.CreateFamilyMember("Timmy", Gender.Male),
                leroy = FamilyTreeFactory.CreateFamilyMember("Leroy", Gender.Male),
                martha = FamilyTreeFactory.CreateFamilyMember("Martha", Gender.Female);

            var tree = FamilyTreeFactory.CreateFamilyTree();
            tree.SetRoot(FamilyTreeFactory.CreatePartnership(greg, pam), pam);
            var root = tree.Root;
            tree.AddChild(root, jeff);
            tree.AddChild(root, brent);
            tree.AddChild(root, kyle);
            tree.AddInLaw(jaclyn);
            tree.AddInLaw(aura);
            tree.AddInLaw(guy);
            tree.AddInLaw(nancy);
            tree.AddInLaw(martha);

            var brentJaclyn = tree.AddPartnership(brent, jaclyn);
            tree.AddChild(brentJaclyn, roxi);
            tree.AddChild(brentJaclyn, della);

            var jeffAura = tree.AddPartnership(jeff, aura);

            tree.AddPartnership(kyle, martha, true);

            tree.AddNonPartnershipChild(kyle, ping);

            var roxiGuy = tree.AddPartnership(roxi, guy);
            tree.AddChild(roxiGuy, timmy);

            var pingNancy = tree.AddPartnership(ping, nancy);
            tree.AddChild(pingNancy, leroy);

            tree.AddNonPartnershipChild(leroy, FamilyTreeFactory.CreateFamilyMember("Davis", Gender.Male));
            #endregion

            var source = kyle;

            processor.Process(tree, source);

            #region display
            Console.WriteLine("Source: " + source.Name);
            foreach (IFamilyMember fm in tree)
                DisplayMemberWithFacts(fm);
            #endregion

            Console.ReadKey();
        }

        private static void DisplayMemberWithFacts(IFamilyMember fm)
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
