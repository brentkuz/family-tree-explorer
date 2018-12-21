using FamilyTreeExplorer.Business.Objects.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTreeExplorer.Business.Objects.Relationships
{
    public class Parentship : ChildBearingBase
    {
        public IFamilyMember Partner1 { get; set; }
    }
}
