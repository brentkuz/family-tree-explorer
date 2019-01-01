using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTreeExplorer.Crosscutting.Enums
{
    public enum RelationshipType
    {
        Self,
        Parent,
        Sibling,
        Aunt,
        Uncle,
        Grandparent,
        Niece,
        Nephew,
        Grandniece,
        Grandnephew,
        Child,
        Grandchild,
        Cousin,
        Spouse
    }

    public enum RelationshipModifier
    {
        Great,
        Ex
    }
}
