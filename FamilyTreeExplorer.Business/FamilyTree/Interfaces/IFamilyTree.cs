using System;
using System.Collections;
using FamilyTreeExplorer.Business.FamilyTree.Relationships;

namespace FamilyTreeExplorer.Business.FamilyTree.Interfaces
{
    public interface IFamilyTree : IEnumerable
    {
        int Count { get; }
        IPartnership Root { get; }

        void SetRoot(IPartnership root, IFamilyMember inlaw);
        void AddChild(IPartnership partnership, IFamilyMember child);
        void AddInLaw(IFamilyMember inlaw);
        void AddNonPartnershipChild(IFamilyMember parent, IFamilyMember child);
        IPartnership AddPartnership(IFamilyMember partner1, IFamilyMember partner2, bool isDivorced = false);
        void ClearMemberFacts();
        IFamilyMember GetMemberById(Guid id);
        IPartnership GetPartnershipById(Guid id);
        bool InLawAlreadyInPartnership(IFamilyMember inlaw);
        bool MemberExists(IFamilyMember member);
        bool MemberExists(Guid id);
        bool PartnershipExists(Guid id);
        bool PartnershipExists(IPartnership partnership);
        bool SimilarPartnershipExists(IPartnership partnership);
    }
}