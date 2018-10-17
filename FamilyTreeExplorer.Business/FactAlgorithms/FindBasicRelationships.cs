using System;
using System.Collections.Generic;
using System.Text;
using FamilyTreeExplorer.Business.Objects;
using FamilyTreeExplorer.Crosscutting.Enums;

namespace FamilyTreeExplorer.Business.FactAlgorithms
{
    public class FindBasicRelationships : FactAgorithmBase
    {
        public FindBasicRelationships(FamilyTree tree, FamilyMember source) : base(tree, source)
        {
            this.source = source;
        }

        protected override void Execute()
        {
            Up(source.Parents, source.GetFactValue<int>(FactType.XPosition), source.GetFactValue<int>(FactType.YPosition) - 1);
        }
        private void Up(ChildBearingBase m, int x, int y)
        {
            if (m == null)
                return;


        }
    }
}
