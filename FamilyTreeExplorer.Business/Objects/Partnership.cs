using FamilyTreeExplorer.Business.Objects.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTreeExplorer.Business.Objects
{
    public class Partnership : Parentship
    {
        public Partnership(FamilyMember partner1, FamilyMember partner2)
        {
            Id = Guid.NewGuid();
            Partner1 = partner1;
            Partner2 = partner2;
            partner1.Partnerships.Add(this);
            partner2.Partnerships.Add(this);
        }
        
        public FamilyMember Partner2 { get; set; }

        public FamilyMember OtherPartner(FamilyMember member)
        {
            if (member == Partner1)
                return Partner2;
            else
                return Partner1;
        }
    }
}
