using FamilyTreeExplorer.Business.FamilyTree.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTreeExplorer.Business.Services.Interfaces
{
    public interface ITreeService
    {
        void GatherFacts(IFamilyTree tree, IFamilyMember source);
    }
}
