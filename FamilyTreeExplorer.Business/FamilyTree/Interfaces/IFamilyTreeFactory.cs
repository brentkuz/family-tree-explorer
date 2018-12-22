using FamilyTreeExplorer.Business.FamilyTree.Interfaces;
using FamilyTreeExplorer.Crosscutting.Enums;

namespace FamilyTreeExplorer.Business.FamilyTree.Interfaces
{
    public interface IFamilyTreeFactory
    {
        IFamilyMember CreateFamilyMember(string name, Gender gender);
        IFamilyTree CreateFamilyTree();
        IParentship CreateParentship(IFamilyMember partner1);
        IParentship CreatePartnership(IFamilyMember partner1, IFamilyMember partner2, bool isDivorced = false);
    }
}