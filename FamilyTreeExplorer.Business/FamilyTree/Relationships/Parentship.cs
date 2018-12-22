using FamilyTreeExplorer.Business.FamilyTree.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTreeExplorer.Business.FamilyTree.Relationships
{
    public class Parentship : ChildBearingBase, IParentship
    {
        public Parentship()
        {

        }
        public Parentship(IFamilyMember partner1)
        {
            Partner1 = partner1;
        }
        public IFamilyMember Partner1 { get; set; }
    }
}
