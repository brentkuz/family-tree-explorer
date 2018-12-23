using FamilyTreeExplorer.Business.FamilyTree.Interfaces;

namespace FamilyTreeExplorer.Business.FactAlgorithms.Interfaces
{
    public interface IFamilyTreeProcessor
    {
        void Process(IFamilyTree tree, IFamilyMember source);
    }
}