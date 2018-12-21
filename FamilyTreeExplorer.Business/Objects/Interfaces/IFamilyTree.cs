using System;
using System.Collections;
using FamilyTreeExplorer.Business.Objects.Relationships;

namespace FamilyTreeExplorer.Business.Objects.Interfaces
{
    public interface IFamilyTree : IEnumerable
    {
        int Count { get; }
        Partnership Root { get; }

        void SetRoot(Partnership root, FamilyMember inlaw);
        void AddChild(Partnership partnership, FamilyMember child);
        void AddInLaw(FamilyMember inlaw);
        void AddNonPartnershipChild(FamilyMember parent, FamilyMember child);
        Partnership AddPartnership(FamilyMember partner1, FamilyMember partner2);
        void ClearMemberFacts();
        FamilyMember GetMemberById(Guid id);
        Partnership GetPartnershipById(Guid id);
        bool InLawAlreadyInPartnership(FamilyMember inlaw);
        bool MemberExists(FamilyMember member);
        bool MemberExists(Guid id);
        bool PartnershipExists(Guid id);
        bool PartnershipExists(Partnership partnership);
        bool SimilarPartnershipExists(Partnership partnership);
    }
}