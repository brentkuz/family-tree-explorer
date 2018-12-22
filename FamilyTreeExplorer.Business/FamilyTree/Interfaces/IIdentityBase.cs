using System;

namespace FamilyTreeExplorer.Business.FamilyTree.Interfaces
{
    public interface IIdentityBase
    {
        Guid Id { get; set; }

        bool Equals(IdentityBase other);
    }
}