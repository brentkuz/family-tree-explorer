using FamilyTreeExplorer.Business.Objects;
using System;

namespace FamilyTreeExplorer.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            FamilyMember greg = new FamilyMember("Greg"),
                pam = new FamilyMember("Pam"),
                jeff = new FamilyMember("Jeff"),
                brent = new FamilyMember("Brent"),
                kyle = new FamilyMember("Kyle"),
                jaclyn = new FamilyMember("Jaclyn"),
                roxi = new FamilyMember("Roxi"),
                della = new FamilyMember("Della"),
                aura = new FamilyMember("Aura");

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

            

            Console.ReadKey();
        }
    }
}
