using FamilyTreeExplorer.Business.Objects.Interfaces;
using FamilyTreeExplorer.Crosscutting.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTreeExplorer.Business.Objects
{
    public class Partnership : Parentship
    {
        public Partnership(FamilyMember partner1, FamilyMember partner2, bool isDivorced = false)
        {
            Id = Guid.NewGuid();
            Partner1 = partner1;
            Partner2 = partner2;
            partner1.Partnerships.Add(this);
            partner2.Partnerships.Add(this);
            IsDivorced = IsDivorced;
        }

        public bool IsDivorced { get; set; }

        public FamilyMember Partner2 { get; set; }

        public FamilyMember OtherPartner(FamilyMember member)
        {
            if (member == Partner1)
                return Partner2;
            else
                return Partner1;
        }

        public FamilyMember GetInLaw()
        {
            if (Partner1.HasFact(FactType.InLaw))
                return Partner1;
            else if (Partner2.HasFact(FactType.InLaw))
                return Partner2;
            else
                return null;
        }
    }
}
