using FamilyTreeExplorer.Business.FamilyTree.Interfaces;
using FamilyTreeExplorer.Business.FamilyTree.Relationships;
using FamilyTreeExplorer.Crosscutting.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FamilyTreeExplorer.Business.FamilyTree
{
    public class FamilyMember : TreeNode, IFamilyMember
    {
        public FamilyMember()
        {
            NonPartnership = FamilyTreeFactory.CreateParentship(this);
        }
        public FamilyMember(string name, Gender gender) : this()
        {                    
            this.Name = name;
            this.Gender = gender;
            
        }

        public string Name { get; set; }
        public Gender Gender { get; set; }
        public IParentship Parents { get; set; }
        public List<IPartnership> Partnerships { get; set; } = new List<IPartnership>();
        public IParentship NonPartnership { get; set; } 

        public Dictionary<FactType, Fact> Facts { get; set; } = new Dictionary<FactType, Fact>();

        public bool HasPartnerships()
        {
            return Partnerships.Count > 0;
        }
        public bool HasChildren()
        {
            return Partnerships.Exists(x => x.HasChildren()) || NonPartnership.Children.Count > 0;
        }
        
        public IEnumerable<Fact> GetFacts()
        {
            return Facts.Values.OrderBy(x => x.Type);
        }

        public T GetFactValue<T>(FactType type)
        {
            if (!HasFact(type))
                throw new ArgumentException(string.Format("Fact {0} does not exist", type));
            return (T)Facts[type].Value;
        }

        public void AddFact(FactType type, object value)
        {
            var fact = new Fact()
            {
                Type = type,
                Value = value
            };
            if (!Facts.ContainsKey(type))
                Facts.Add(type, fact);
            else
                Facts[type] = fact;
        }

        public bool IsMarriedTo(IFamilyMember member)
        {
            return Partnerships.Exists(x => x.OtherPartner(this) == member && x.IsDivorced == false);
        }

        public bool IsDivorcedFrom(IFamilyMember member)
        {
            return Partnerships.Exists(x => x.OtherPartner(this) == member && x.IsDivorced == true);
        }

        public void ClearAllFacts()
        {
            Facts = new Dictionary<FactType, Fact>();
        }

        public void RemoveFact(FactType type)
        {
            if (Facts.ContainsKey(type))
                Facts.Remove(type);
        }

        public bool HasFact(FactType type)
        {
            return Facts.ContainsKey(type);
        }

        public IEnumerator GetEnumerator()
        {
            foreach (var part in Partnerships)
                 yield return part;
            yield return NonPartnership;
        }

     
    }
}
