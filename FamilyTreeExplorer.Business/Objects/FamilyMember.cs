using FamilyTreeExplorer.Business.Objects.Interfaces;
using FamilyTreeExplorer.Crosscutting.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FamilyTreeExplorer.Business.Objects
{
    public class FamilyMember : TreeNode, IFactual, IEnumerable
    {
        public FamilyMember()
        {
            NonPartnership.Partner1 = this;
        }
        public FamilyMember(string name, Gender gender) : this()
        {                    
            this.Name = name;
            this.Gender = gender;
        }

        public string Name { get; set; }
        public Gender Gender { get; set; }
        public Parentship Parents { get; set; }
        public List<Partnership> Partnerships { get; set; } = new List<Partnership>();
        public Parentship NonPartnership { get; set; } = new Parentship();

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
