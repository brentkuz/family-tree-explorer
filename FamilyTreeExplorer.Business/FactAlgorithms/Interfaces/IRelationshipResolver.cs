using FamilyTreeExplorer.Business.FamilyTree.Interfaces;
using System;

namespace FamilyTreeExplorer.Business.FactAlgorithms.Interfaces
{
    public interface IRelationshipResolver : IEquatable<IRelationshipResolver>
    {
        int? MaxXPosition { get; }
        int? MaxYPosition { get; }
        int? MinXPosition { get; }
        int? MinYPosition { get; }

        string Execute(IFamilyMember source, IFamilyMember target);

        bool InPositionRange(int x, int y);
    }
}