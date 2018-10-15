using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTreeExplorer.Business.Objects
{
    public class FamilyMember : TreeNode
    {
        public FamilyMember()
        {

        }
        public FamilyMember(string name)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
        }

        public string Name { get; set; }
        public FamilyMember Parent1 { get; set; }
        public FamilyMember Parent2 { get; set; }
        public List<Partnership> Partnerships { get; set; } = new List<Partnership>();
        public List<FamilyMember> NonPartnershipChildren { get; set; } = new List<FamilyMember>();

        public bool HasPartnerships()
        {
            return Partnerships.Count > 0;
        }
        public bool HasChildren()
        {
            return Partnerships.Exists(x => x.HasChildren()) || NonPartnershipChildren.Count > 0;
        }
    }
}
