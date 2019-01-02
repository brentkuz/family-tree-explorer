using FamilyTreeExplorer.Business.FamilyTree.Interfaces;

namespace FamilyTreeExplorer.Business.FactAlgorithms.Interfaces
{
    public interface IChainedSubResolver
    {
        string Handle(IFamilyMember source, IFamilyMember target);
    }
}