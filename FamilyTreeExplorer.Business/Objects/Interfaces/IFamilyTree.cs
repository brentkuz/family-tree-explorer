using System;
using System.Collections;
using FamilyTreeExplorer.Business.Objects.Relationships;

namespace FamilyTreeExplorer.Business.Objects.Interfaces
{
    public interface IFamilyTree : IEnumerable
    {
        int Count { get; }
        Partnership Root { get; }

        void SetRoot(Partnership root, IFamilyMember inlaw);
        void AddChild(Partnership partnership, IFamilyMember child);
        void AddInLaw(IFamilyMember inlaw);
        void AddNonPartnershipChild(IFamilyMember parent, IFamilyMember child);
        Partnership AddPartnership(IFamilyMember partner1, IFamilyMember partner2);
        void ClearMemberFacts();
        IFamilyMember GetMemberById(Guid id);
        Partnership GetPartnershipById(Guid id);
        bool InLawAlreadyInPartnership(IFamilyMember inlaw);
        bool MemberExists(IFamilyMember member);
        bool MemberExists(Guid id);
        bool PartnershipExists(Guid id);
        bool PartnershipExists(Partnership partnership);
        bool SimilarPartnershipExists(Partnership partnership);
    }
}