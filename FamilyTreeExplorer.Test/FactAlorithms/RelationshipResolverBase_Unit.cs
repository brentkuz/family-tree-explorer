using FamilyTreeExplorer.Business.FactAlgorithms.RelationshipNameResolvers;
using FamilyTreeExplorer.Test.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTreeExplorer.Test.FactAlorithms
{
    [TestClass]
    [TestCategory("RelationshipResolverBase_Unit")]
    public class RelationshipResolverBase_Unit
    {
        [TestMethod]
        public void Equals_EqualResolvers()
        {
            var r1 = new DirectLineageResolver();
            var r2 = new DirectLineageResolver();

            Assert.IsTrue(r1.Equals(r2));
        }

        [TestMethod]
        public void Equals_UnequalResolvers()
        {
            var r1 = new DirectLineageResolver();
            var r2 = new TestRelationshipResolver();

            Assert.IsFalse(r1.Equals(r2));
        }

        [TestMethod]
        public void InPositionRange_ValidPosition()
        {
            var r = new DirectLineageResolver();
            int x = 0,
                y = 1;

            Assert.IsTrue(r.InPositionRange(x, y));
        }

        [TestMethod]
        public void InPositionRange_InvalidPositionX()
        {
            var r = new DirectLineageResolver();
            int x = 1,
                y = 1;

            Assert.IsFalse(r.InPositionRange(x, y));
        }
        [TestMethod]
        public void InPositionRange_InvalidPositionY()
        {
            var r = new DirectLineageResolver();
            int x = 0,
                y = -1;

            Assert.IsFalse(r.InPositionRange(x, y));
        }
    }
}
