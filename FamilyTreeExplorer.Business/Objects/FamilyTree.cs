using FamilyTreeExplorer.Business.Objects.Interfaces;
using FamilyTreeExplorer.Business.Objects.Relationships;
using FamilyTreeExplorer.Crosscutting.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FamilyTreeExplorer.Business.Objects
{
    public class FamilyTree : IFamilyTree
    {
        private Dictionary<Guid, FamilyMember> members = new Dictionary<Guid, FamilyMember>();
        private Dictionary<Guid, Partnership> partnerships = new Dictionary<Guid, Partnership>();

        public FamilyTree()
        {
        }

        public Partnership Root { get; protected set; }
        public int Count { get { return members.Count; } }

        public void SetRoot(Partnership root, FamilyMember inlaw)
        {
            this.Root = root;
            if (root.Partner1 != null)
            {
                root.Partner1.AddFact(FactType.Depth, 0);
                AddMember(root.Partner1);
            }
            if (root.Partner2 != null)
            {
                root.Partner2.AddFact(FactType.Depth, 0);
                AddMember(root.Partner2);
            }

            inlaw.AddFact(FactType.InLaw, true);

            AddPartnership(root);
        }

        public void AddInLaw(FamilyMember inlaw)
        {
            inlaw.AddFact(FactType.InLaw, true);
            AddMember(inlaw);
        }
        public void AddChild(Partnership partnership, FamilyMember child)
        {
            if (partnership != null && !PartnershipExists(partnership))
                throw new InvalidPartnershipException("Cannot add children to a partnership that does not exist.");
            if (child.Parents != null)
                throw new ExistingParentsReferenceException(child);

            AddMember(child);

            child.Parents = partnership;
            partnership.Children.Add(child);

            //Determine depth based on parents
            int depth = (int)(partnership.Partner1.Facts[FactType.Depth]?.Value ?? partnership.Partner2.Facts[FactType.Depth]?.Value) + 1;
            child.AddFact(FactType.Depth, depth);
        }
        public void AddNonPartnershipChild(FamilyMember parent, FamilyMember child)
        {
            if (!MemberExists(parent))
                throw new NotInFamilyTreeException(parent);
            if (child.Parents != null)
                throw new ExistingParentsReferenceException(child);

            child.AddFact(FactType.HasSingleParent, true);
            child.AddFact(FactType.Depth, (int)parent.Facts[FactType.Depth].Value + 1);
            AddMember(child);
            parent.NonPartnership.Children.Add(child);
            child.Parents = parent.NonPartnership;
        }
        public Partnership AddPartnership(FamilyMember partner1, FamilyMember partner2)
        {
            var p1Exists = MemberExists(partner1);
            var p2Exists = MemberExists(partner2);

            if (!p1Exists || !p2Exists)
                throw new NotInFamilyTreeException(!p1Exists ? partner1 : partner2);

            var partnership = new Partnership(partner1, partner2);

            FamilyMember blood = null,
                inlaw = null;

            inlaw = partner1.HasFact(FactType.InLaw) ? partner1 : partner2;
            blood = partnership.OtherPartner(inlaw);
            inlaw.AddFact(FactType.Depth, blood.Facts[FactType.Depth].Value);

            AddPartnership(partnership);

            return partnership;
        }

        public bool MemberExists(FamilyMember member)
        {
            return members.ContainsValue(member);
        }
        public bool MemberExists(Guid id)
        {
            return members.ContainsKey(id);
        }
        public bool PartnershipExists(Partnership partnership)
        {
            return partnerships.ContainsValue(partnership);
        }
        public bool SimilarPartnershipExists(Partnership partnership)
        {
            return partnerships.Values.Where(x => x.Partner1 == partnership.Partner1 && x.Partner2 == partnership.Partner2).SingleOrDefault() != null
                || partnerships.Values.Where(x => x.Partner1 == partnership.Partner2 && x.Partner2 == partnership.Partner1).SingleOrDefault() != null;
        }
        public bool PartnershipExists(Guid id)
        {
            return partnerships.ContainsKey(id);
        }
        public bool InLawAlreadyInPartnership(FamilyMember inlaw)
        {
            return partnerships.Values.Where(x => x.Partner1 == inlaw || x.Partner2 == inlaw).SingleOrDefault() != null;
        }
        public FamilyMember GetMemberById(Guid id)
        {
            return members[id];
        }
        public Partnership GetPartnershipById(Guid id)
        {
            return partnerships[id];
        }
        public void ClearMemberFacts()
        {
            foreach(var memb in members.Values)
            {
                memb.ClearAllFacts();
            }
        }

        public IEnumerator GetEnumerator()
        {
            foreach (var x in members.Values)
                yield return x;
        }

        private void AddMember(FamilyMember member)
        {
            if (MemberExists(member))
                throw new DuplicateMemberException(member);
            members.Add(member.Id, member);
        }
        private void AddPartnership(Partnership partnership)
        {
            if (PartnershipExists(partnership) || SimilarPartnershipExists(partnership))
                throw new DuplicateParntershipException(partnership);

            partnerships.Add(partnership.Id, partnership);
        }
    }

    public class NotInFamilyTreeException : Exception
    {
        public NotInFamilyTreeException() { }
        public NotInFamilyTreeException(FamilyMember member) 
            : base(string.Format("Partner Id:{0} is not a member of the tree", member.Id)) { }
        public NotInFamilyTreeException(string message) : base(message) { }
    }
    public class DuplicateMemberException : Exception
    {
        public DuplicateMemberException() { }
        public DuplicateMemberException(FamilyMember member)
            : base(string.Format("Member Id:{0} is already a member of the tree", member.Id)) { }
        public DuplicateMemberException(string message) : base(message) { }
    }
    public class InvalidPartnershipException : Exception
    {
        public InvalidPartnershipException() { }
        public InvalidPartnershipException(string message) : base(message) { }
    }
    public class DuplicateParntershipException : Exception
    {
        public DuplicateParntershipException() { }
        public DuplicateParntershipException(Partnership partnership)
            : base(string.Format("Partnership Id:{0} already exists", partnership.Id)) { }
        public DuplicateParntershipException(string message) : base(message) { }
    }
    public class ExistingParentsReferenceException : Exception
    {
        public ExistingParentsReferenceException() { }
        public ExistingParentsReferenceException(FamilyMember member)
            : base(string.Format("Member Id:{0} already has parents.", member.Id)) { }
        public ExistingParentsReferenceException(string message) : base(message) { }
    }
}
