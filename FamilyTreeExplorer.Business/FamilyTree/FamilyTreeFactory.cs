using FamilyTreeExplorer.Business.FamilyTree.Interfaces;
using FamilyTreeExplorer.Business.FamilyTree.Relationships;
using FamilyTreeExplorer.Crosscutting.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTreeExplorer.Business.FamilyTree
{
    public class FamilyTreeFactory : IFamilyTreeFactory
    {
        public IFamilyMember CreateFamilyMember(string name, Gender gender)
        {
            return new FamilyMember(name, gender);
        }

        public IParentship CreateParentship(IFamilyMember partner1)
        {
            return new Parentship(partner1);
        }

        public IParentship CreatePartnership(IFamilyMember partner1, IFamilyMember partner2, bool isDivorced = false)
        {
            return new Partnership(partner1, partner2, isDivorced);
        }

        public IFamilyTree CreateFamilyTree()
        {
            return new FamilyTree();
        }
    }
}
