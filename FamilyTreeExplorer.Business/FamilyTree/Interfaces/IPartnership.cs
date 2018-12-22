using FamilyTreeExplorer.Business.FamilyTree.Interfaces;

namespace FamilyTreeExplorer.Business.FamilyTree.Interfaces
{
    public interface IPartnership : IParentship
    {
        bool IsDivorced { get; set; }
        IFamilyMember Partner2 { get; set; }

        IFamilyMember GetInLaw();
        IFamilyMember OtherPartner(IFamilyMember member);
    }
}