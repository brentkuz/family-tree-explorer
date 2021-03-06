﻿using FamilyTreeExplorer.Business.FamilyTree.Interfaces;
using FamilyTreeExplorer.Business.FamilyTree.Relationships;
using FamilyTreeExplorer.Crosscutting.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FamilyTreeExplorer.Business.FamilyTree
{
    public class FamilyTree : IFamilyTree
    {
        private Dictionary<Guid, IFamilyMember> members = new Dictionary<Guid, IFamilyMember>();
        private Dictionary<Guid, IPartnership> partnerships = new Dictionary<Guid, IPartnership>();

        public FamilyTree()
        {
        }

        public IPartnership Root { get; protected set; }
        public int Count { get { return members.Count; } }

        public void SetRoot(IPartnership root, IFamilyMember inlaw)
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

        public void AddInLaw(IFamilyMember inlaw)
        {
            inlaw.AddFact(FactType.InLaw, true);
            AddMember(inlaw);
        }
        public void AddChild(IPartnership partnership, IFamilyMember child)
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
        public void AddNonPartnershipChild(IFamilyMember parent, IFamilyMember child)
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
        public IPartnership AddPartnership(IFamilyMember partner1, IFamilyMember partner2, bool isDivorced = false)
        {
            var p1Exists = MemberExists(partner1);
            var p2Exists = MemberExists(partner2);

            if (!p1Exists || !p2Exists)
                throw new NotInFamilyTreeException(!p1Exists ? partner1 : partner2);

            var partnership = FamilyTreeFactory.CreatePartnership(partner1, partner2, isDivorced);

            IFamilyMember blood = null,
                inlaw = null;

            inlaw = partner1.HasFact(FactType.InLaw) ? partner1 : partner2;
            blood = partnership.OtherPartner(inlaw);
            inlaw.AddFact(FactType.Depth, blood.Facts[FactType.Depth].Value);

            AddPartnership(partnership);

            return partnership;
        }

        public bool MemberExists(IFamilyMember member)
        {
            return members.ContainsValue(member);
        }
        public bool MemberExists(Guid id)
        {
            return members.ContainsKey(id);
        }
        public bool PartnershipExists(IPartnership partnership)
        {
            return partnerships.ContainsValue(partnership);
        }
        public bool SimilarPartnershipExists(IPartnership partnership)
        {
            return partnerships.Values.Where(x => x.Partner1 == partnership.Partner1 && x.Partner2 == partnership.Partner2).SingleOrDefault() != null
                || partnerships.Values.Where(x => x.Partner1 == partnership.Partner2 && x.Partner2 == partnership.Partner1).SingleOrDefault() != null;
        }
        public bool PartnershipExists(Guid id)
        {
            return partnerships.ContainsKey(id);
        }
        public bool InLawAlreadyInPartnership(IFamilyMember inlaw)
        {
            return partnerships.Values.Where(x => x.Partner1 == inlaw || x.Partner2 == inlaw).SingleOrDefault() != null;
        }
        public IFamilyMember GetMemberById(Guid id)
        {
            return members[id];
        }
        public IPartnership GetPartnershipById(Guid id)
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

        private void AddMember(IFamilyMember member)
        {
            if (MemberExists(member))
                throw new DuplicateMemberException(member);
            members.Add(member.Id, member);
        }
        private void AddPartnership(IPartnership partnership)
        {
            if (PartnershipExists(partnership) || SimilarPartnershipExists(partnership))
                throw new DuplicateParntershipException(partnership);

            partnerships.Add(partnership.Id, partnership);
        }
    }

    public class NotInFamilyTreeException : Exception
    {
        public NotInFamilyTreeException() { }
        public NotInFamilyTreeException(IFamilyMember member) 
            : base(string.Format("Partner Id:{0} is not a member of the tree", member.Id)) { }
        public NotInFamilyTreeException(string message) : base(message) { }
    }
    public class DuplicateMemberException : Exception
    {
        public DuplicateMemberException() { }
        public DuplicateMemberException(IFamilyMember member)
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
        public DuplicateParntershipException(IPartnership partnership)
            : base(string.Format("Partnership Id:{0} already exists", partnership.Id)) { }
        public DuplicateParntershipException(string message) : base(message) { }
    }
    public class ExistingParentsReferenceException : Exception
    {
        public ExistingParentsReferenceException() { }
        public ExistingParentsReferenceException(IFamilyMember member)
            : base(string.Format("Member Id:{0} already has parents.", member.Id)) { }
        public ExistingParentsReferenceException(string message) : base(message) { }
    }
}
