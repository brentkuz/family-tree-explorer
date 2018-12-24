﻿using FamilyTreeExplorer.Business.FactAlgorithms.RelationshipNameResolvers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTreeExplorer.Test.FactAlorithms
{
    [TestClass]
    [TestCategory("SelfResolver_Unit")]
    public class SelfResolver_Unit
    {
        [TestMethod]
        public void InPositionRange_InRange()
        {
            int x = 0,
                y = 0;

            var resolver = new SelfResolver();
            Assert.IsTrue(resolver.InPositionRange(x, y));

        }

        
    }
    [TestClass]
    [TestCategory("DirectLineageResolver_Unit")]
    public class DirectLineageResolver_Unit
    {
        [TestMethod]
        public void InPositionRange_SourceFamilyMemberIsInRange()
        {
            int x = 0,
                y = 0;

            var resolver = new DirectLineageResolver();
            Assert.IsTrue(resolver.InPositionRange(x, y));
        }

        [TestMethod]
        public void InPositionRange_ValidInputWithNegativeYIsInRange()
        {
            int x = 0,
                y = -1;

            var resolver = new DirectLineageResolver();
            Assert.IsTrue(resolver.InPositionRange(x, y));
        }
    }
}
