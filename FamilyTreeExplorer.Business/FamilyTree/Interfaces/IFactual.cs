using FamilyTreeExplorer.Crosscutting.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTreeExplorer.Business.FamilyTree.Interfaces
{
    public interface IFactual
    {
        IEnumerable<Fact> GetFacts();
        T GetFactValue<T>(FactType type);
        void AddFact(FactType type, object value);
        void ClearAllFacts();
        void RemoveFact(FactType type);
        bool HasFact(FactType type);
    }
}
