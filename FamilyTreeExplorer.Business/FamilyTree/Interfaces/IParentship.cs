using FamilyTreeExplorer.Business.FamilyTree.Interfaces;

namespace FamilyTreeExplorer.Business.FamilyTree.Interfaces
{
    public interface IParentship : IChildBearingBase
    {
        IFamilyMember Partner1 { get; set; }
    }
}