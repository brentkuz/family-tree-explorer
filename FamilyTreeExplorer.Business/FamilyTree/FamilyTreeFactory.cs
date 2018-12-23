using FamilyTreeExplorer.Business.FamilyTree.Interfaces;
using FamilyTreeExplorer.Business.FamilyTree.Relationships;
using FamilyTreeExplorer.Crosscutting.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTreeExplorer.Business.FamilyTree
{
    public static class FamilyTreeFactory
    {
        public static IFamilyMember CreateFamilyMember(string name, Gender gender)
        {
            return new FamilyMember(name, gender);
        }

        public static IParentship CreateParentship(IFamilyMember partner1)
        {
            return new Parentship(partner1);
        }

        public static IPartnership CreatePartnership(IFamilyMember partner1, IFamilyMember partner2, bool isDivorced = false)
        {
            return new Partnership(partner1, partner2, isDivorced);
        }

        public static IFamilyTree CreateFamilyTree()
        {
            return new FamilyTree();
        }
    }
}
