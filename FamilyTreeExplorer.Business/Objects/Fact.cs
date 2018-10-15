using FamilyTreeExplorer.Crosscutting.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTreeExplorer.Business.Objects
{
    public class Fact
    {
        public FactType Type { get; set; }
        public object Value { get; set; }
    }
}
