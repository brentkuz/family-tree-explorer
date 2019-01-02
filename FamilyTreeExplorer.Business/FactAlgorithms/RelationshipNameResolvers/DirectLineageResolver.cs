using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FamilyTreeExplorer.Business.FactAlgorithms.Interfaces;
using FamilyTreeExplorer.Business.FamilyTree;
using FamilyTreeExplorer.Business.FamilyTree.Interfaces;
using FamilyTreeExplorer.Crosscutting.Enums;

namespace FamilyTreeExplorer.Business.FactAlgorithms.RelationshipNameResolvers
{
    public class DirectLineageResolver : RelationshipResolverBase, IDirectLineageResolver
    {
        private const int MIN_X_POSITION = 0;
        private const int MAX_X_POSITION = 0;

        private const int AUNT_UNCLE_GREAT_START_Y_POSITION = -1;
        private const int GRANDPARENT_GREAT_START_Y_POSITION = -2;
        private const int GRANDCHILD_GREAT_START_Y_POSITION = 2;
        private const int GRAND_NIECE_NEPHEW_GREAT_START_Y_POSITION = 2;

        protected IChainedSubResolver subResolverHead;

        public DirectLineageResolver(IGrandparentSubResolver subResolverHead)
        {
            MinXPosition = MIN_X_POSITION;
            MaxXPosition = MAX_X_POSITION;

            this.subResolverHead = subResolverHead;
        }

        public override string Execute(IFamilyMember source, IFamilyMember target)
        {
            return subResolverHead.Handle(source, target);
        }


        ////TODO: Needs refactoring into sub resolvers.
        //protected virtual Tuple<RelationshipType, string> GetRelationshipTypeName(IFamilyMember source, IFamilyMember target)
        //{
        //    RelationshipType type = default(RelationshipType);
        //    string typeDisplay = null;

        //    int targetY = target.GetFactValue<int>(FactType.YPosition);

        //    if (targetY < -1)
        //    {
        //        if (target.HasFact(FactType.Ancestor))
        //        {
        //            //Grandparent
        //            type = RelationshipType.Grandparent;
        //        }
        //        else
        //        {
        //            //Great Aunt/Uncle
        //            type = target.Gender == Gender.Female ? RelationshipType.Aunt : RelationshipType.Uncle;
        //        }
        //    }
        //    else if (targetY == -1)
        //    {
        //        if (target.HasFact(FactType.Ancestor))
        //        {
        //            //Parent
        //            type = RelationshipType.Parent;
        //        }
        //        else
        //        {
        //            //Aunt/Uncle
        //            type = target.Gender == Gender.Female ? RelationshipType.Aunt : RelationshipType.Uncle;
        //        }
        //    }
        //    else if (targetY == 0)
        //    {                
        //        var isEx = source.IsDivorcedFrom(target);
        //        if (source.IsMarriedTo(target) || isEx)
        //        {
        //            //Spouse/Ex-Spouse
        //            type = RelationshipType.Spouse;
        //            if (isEx)
        //                typeDisplay = RelationshipModifier.Ex.ToString() + "-" + RelationshipType.Spouse.ToString();
        //        }
        //        else
        //        {
        //            //Sibling
        //            type = RelationshipType.Sibling;
        //        }
        //    }
        //    else if (targetY == 1)
        //    {                
        //        if (target.HasFact(FactType.Ancestor))
        //        {
        //            //Child
        //            type = RelationshipType.Child;
        //        }
        //        else
        //        {
        //            //Niece/Nephew
        //            type = target.Gender == Gender.Female ? RelationshipType.Niece : RelationshipType.Nephew;
        //        }
        //    }
        //    else
        //    {
        //        if (target.HasFact(FactType.Ancestor))
        //        {
        //            //Grandchild
        //            type = RelationshipType.Grandchild;
        //        }
        //        else
        //        {
        //            type = target.Gender == Gender.Female ? RelationshipType.Grandniece : RelationshipType.Grandnephew;
        //        }
        //    }

        //    return new Tuple<RelationshipType, string>(type, typeDisplay ?? type.ToString());
        //}

        //protected virtual string GetGreatness(int yPosition, RelationshipType type)
        //{
        //    string greatness = string.Empty;
        //    switch(type)
        //    {
        //        case RelationshipType.Aunt:
        //        case RelationshipType.Uncle:
        //            greatness = GetGreatCount(yPosition - AUNT_UNCLE_GREAT_START_Y_POSITION);
        //            break;
        //        case RelationshipType.Grandparent:
        //            greatness = GetGreatCount(yPosition - GRANDPARENT_GREAT_START_Y_POSITION);
        //            break;
        //        case RelationshipType.Grandchild:
        //            greatness = GetGreatCount(yPosition - GRANDCHILD_GREAT_START_Y_POSITION);
        //            break;
        //        case RelationshipType.Grandniece:
        //        case RelationshipType.Grandnephew:
        //            greatness = GetGreatCount(yPosition - GRAND_NIECE_NEPHEW_GREAT_START_Y_POSITION);
        //            break;
        //    }

        //    return greatness;
        //}

        //private string GetGreatCount(int count)
        //{
        //    return string.Concat(Enumerable.Repeat(RelationshipModifier.Great.ToString() + " ", Math.Abs(count)));
        //}


        
    }
}
