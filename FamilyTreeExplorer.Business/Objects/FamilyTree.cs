using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTreeExplorer.Business.Objects
{
    public class FamilyTree
    {
        private Dictionary<int, FamilyMember> members = new Dictionary<int, FamilyMember>();
        private Dictionary<int, Partnership> partnerships = new Dictionary<int, Partnership>();

        public FamilyTree(Partnership root)
        {
            this.Root = root;
            if (root.Partner1 != null)
            {
                AddMember(root.Partner1);                
            }
            if (root.Partner2 != null)
            {
                AddMember(root.Partner2);
            }

            AddPartnership(root);
        }

        public Partnership Root { get; set; }
        public int Count { get { return members.Count; } }

        public void AddChild(FamilyMember child, Partnership partnership = null)
        {
            if (partnership != null && !PartnershipExists(partnership))
                throw new InvalidPartnershipException("Cannot add children to a partnership that does not exist.");

            if(partnership != null)
                partnership.Children.Add(child);
            AddMember(child);
        }
        public Partnership AddPartnership(FamilyMember partner1, FamilyMember partner2)
        {
            var p1Exists = MemberExists(partner1);
            var p2Exists = MemberExists(partner2);

            if (!p1Exists || !p2Exists)
                throw new NotInFamilyTreeException(!p1Exists ? partner1 : partner2);

            var partnership = new Partnership(partner1, partner2);

            AddPartnership(partnership);

            return partnership;
        }
        public bool MemberExists(FamilyMember member)
        {
            return members.ContainsValue(member);
        }
        public bool MemberExists(int id)
        {
            return members.ContainsKey(id);
        }
        public bool PartnershipExists(Partnership partnership)
        {
            return partnerships.ContainsValue(partnership);
        }
        public bool PartnershipExists(int id)
        {
            return partnerships.ContainsKey(id);
        }
        public FamilyMember GetMemberById(int id)
        {
            return members[id];
        }
        public Partnership GetPartnershipById(int id)
        {
            return partnerships[id];
        }
   
        private void AddMember(FamilyMember member)
        {
            if (MemberExists(member))
                throw new DuplicateMemberException(member);
            if (!member.Id.HasValue)
                member.Id = members.Count;
            members.Add((int)member.Id, member);
        }
        private void AddPartnership(Partnership partnership)
        {
            if (PartnershipExists(partnership))
                throw new DuplicateParntershipException(partnership);
            if (!partnership.Id.HasValue)
                partnership.Id = partnerships.Count;

            partnerships.Add((int)partnership.Id, partnership);
        }
    }

    public class NotInFamilyTreeException : Exception
    {
        public NotInFamilyTreeException() { }
        public NotInFamilyTreeException(FamilyMember partner) 
            : base(string.Format("Partner is not a member of the tree", partner.Id)) { }
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
}
