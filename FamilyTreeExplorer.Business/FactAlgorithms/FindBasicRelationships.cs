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

        public HashSet<FamilyMember> MarkedMembers { get; set; } = new HashSet<FamilyMember>();
        public HashSet<ChildBearingBase> MarkedChildBearingBases { get; set; } = new HashSet<ChildBearingBase>();

        protected override void Execute()
        {
            source.AddFact(FactType.XPosition, 0);
            source.AddFact(FactType.YPosition, 0);
            Below(source.Parents, 0, 0);
            Above(source.Parents, 0, 0);
        }

        private void Below(ChildBearingBase n, int x, int y)
        {
            if (n == null)
                return;

            MarkedChildBearingBases.Add(n);

            foreach(var ch in n.Children)
            {
                if(!MarkedMembers.Contains(ch))
                {
                    MarkedMembers.Add(ch);
                    //children
                    foreach (ChildBearingBase p in ch)
                        Below(p, x, y);
                    //partners and there children
                    foreach(var p in ch.Partnerships)
                    {
                        var partner = p.OtherPartner(ch);
                        if(!MarkedMembers.Contains(partner))
                        {
                            MarkedMembers.Add(partner);
                            foreach (ChildBearingBase pCh in partner)
                                if (!MarkedChildBearingBases.Contains(pCh))
                                    Below(pCh, x, y);
                        }
                    }
                }
            }

        }

        private void Above(ChildBearingBase n, int x, int y)
        {

        }
    }
}
