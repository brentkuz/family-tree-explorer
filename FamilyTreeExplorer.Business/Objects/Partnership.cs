using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTreeExplorer.Business.Objects
{
    public class Partnership : IEquatable<Partnership>
    {
        public Partnership(FamilyMember partner1, FamilyMember partner2)
        {
            Partner1 = partner1;
            Partner2 = partner2;
            partner1.Partnerships.Add(this);
            partner2.Partnerships.Add(this);
        }
        public int? Id { get; set; }
        public FamilyMember Partner1 { get; set; }
        public FamilyMember Partner2 { get; set; }
        public List<FamilyMember> Children { get; set; } = new List<FamilyMember>();

        public bool HasChildren()
        {
            return Children.Count > 0;
        }
        public bool Equals(Partnership other)
        {
            return this.Id == other.Id;
        }
    }
}
