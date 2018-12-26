using System;
using System.Collections.Generic;
using System.Text;
using FamilyTreeExplorer.Business.FactAlgorithms.Interfaces;
using FamilyTreeExplorer.Business.FamilyTree;
using FamilyTreeExplorer.Business.FamilyTree.Interfaces;
using FamilyTreeExplorer.Business.FamilyTree.Relationships;
using FamilyTreeExplorer.Crosscutting.Enums;

namespace FamilyTreeExplorer.Business.FactAlgorithms
{
    public class FindBasicRelationships : FactAgorithmBase, IFindBasicRelationships
    {
        public FindBasicRelationships()
        {
        }

        public HashSet<IFamilyMember> MarkedMembers { get; set; } = new HashSet<IFamilyMember>();
        public HashSet<IChildBearingBase> MarkedChildBearingBases { get; set; } = new HashSet<IChildBearingBase>();

        protected override void Execute()
        {            
            Below(source.Parents ?? tree.Root, 0, source.Parents == null ? 1 : 0, 0);
            Above(source.Parents ?? tree.Root, 0, source.Parents == null ? 0 : -1);
        }

        private void Below(IChildBearingBase n, int x, int y, int ancestorY)
        {
            if (n == null || !n.HasChildren())
                return;

            //nextX is calculated if current member is higher in tree, otherwise we 
            //use the parents x value.
            int nextX = y < 0 ? Math.Abs(ancestorY) - Math.Abs(y) : x, 
                nextY = y + 1;

            MarkedChildBearingBases.Add(n);

            foreach(var ch in n.Children)
            {
                if(!MarkedMembers.Contains(ch))
                {
                    //set relative position
                    ch.AddFact(FactType.XPosition, x);
                    ch.AddFact(FactType.YPosition, y);

                    MarkedMembers.Add(ch);
                    //children
                    foreach (IChildBearingBase p in ch)
                        Below(p, nextX, nextY, ancestorY);
                    //partners and there children
                    foreach(var p in ch.Partnerships)
                    {
                        var partner = p.OtherPartner(ch);
                        if(!MarkedMembers.Contains(partner))
                        {
                            //set relative position
                            partner.AddFact(FactType.XPosition, x);
                            partner.AddFact(FactType.YPosition, y);

                            MarkedMembers.Add(partner);
                            foreach (IChildBearingBase pCh in partner)
                                if (!MarkedChildBearingBases.Contains(pCh))
                                    Below(pCh, nextX, nextY, ancestorY);
                        }
                    }
                }
            }

        }

        private void Above(IChildBearingBase n, int x, int y)
        {
           // MarkedChildBearingBases.Add(n);
            if (n is IParentship)
            {
                IFamilyMember nextParent = ((IParentship)n).Partner1;
                //set relative position
                nextParent.AddFact(FactType.XPosition, x);
                nextParent.AddFact(FactType.YPosition, y);
                nextParent.AddFact(FactType.Ancestor, true);

                MarkedMembers.Add(nextParent);
                //ensure that we recurse over non-inlaw partner
                if (n is IPartnership)
                {
                    var partnership = n as IPartnership;
                    var otherPartner = partnership.OtherPartner(nextParent);
                    //set relative position
                    otherPartner.AddFact(FactType.XPosition, x);
                    otherPartner.AddFact(FactType.YPosition, y);
                    otherPartner.AddFact(FactType.Ancestor, true);

                    MarkedMembers.Add(otherPartner);
                    if (nextParent.HasFact(FactType.InLaw))
                    {
                        nextParent = otherPartner;
                    }
                }

                //recurse through nextParent parentships/partnerships
                foreach(IChildBearingBase p in nextParent)
                {
                    if (!MarkedChildBearingBases.Contains(p))
                        Below(p, x, y+1, y);
                }

                //move up
                if (!MarkedChildBearingBases.Contains(nextParent.Parents))
                {
                    Above(nextParent.Parents, x, y-1);
                }
            }
        }
        
    }
}
