using FamilyTreeExplorer.Business.FamilyTree.Interfaces;
using FamilyTreeExplorer.Crosscutting.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTreeExplorer.Business.FamilyTree.Relationships
{
    public class Partnership : Parentship, IPartnership
    {
        public Partnership()
        {

        }
        public Partnership(IFamilyMember partner1, IFamilyMember partner2, bool isDivorced = false) : base(partner1)
        {
            Partner2 = partner2;
            partner1.Partnerships.Add(this);
            partner2.Partnerships.Add(this);
            IsDivorced = isDivorced;
        }

        public bool IsDivorced { get; set; }

        public IFamilyMember Partner2 { get; set; }

        public IFamilyMember OtherPartner(IFamilyMember member)
        {
            if (member == Partner1)
                return Partner2;
            else
                return Partner1;
        }

        public IFamilyMember GetInLaw()
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
