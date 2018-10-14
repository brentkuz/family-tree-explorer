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
                della = new FamilyMember("Della");

            var tree = new FamilyTree(new Partnership(greg, pam));
            var root = tree.Root;
            tree.AddChild(jeff, root);
            tree.AddChild(brent, root);
            tree.AddChild(kyle, root);
            tree.AddChild(jaclyn);
            
            var brentJaclyn = tree.AddPartnership(brent, jaclyn);
            tree.AddChild(roxi, brentJaclyn);
            tree.AddChild(della, brentJaclyn);

            Console.ReadKey();
        }
    }
}
