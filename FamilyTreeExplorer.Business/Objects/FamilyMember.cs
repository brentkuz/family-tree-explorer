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
            this.Name = name;
        }
        public FamilyMember(int id, string name) : this(name)
        {
            this.Id = id;
        }

        public string Name { get; set; }
        public FamilyMember Parent1 { get; set; }
        public FamilyMember Parent2 { get; set; }
        public List<Partnership> Partnerships { get; set; } = new List<Partnership>();

        public bool HasPartnerships()
        {
            return Partnerships.Count > 0;
        }
    }
}
