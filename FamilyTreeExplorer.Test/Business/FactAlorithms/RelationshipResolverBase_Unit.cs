using FamilyTreeExplorer.Business.FactAlgorithms.Interfaces;
using FamilyTreeExplorer.Business.FactAlgorithms.RelationshipNameResolvers;
using FamilyTreeExplorer.Test.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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
            var headMock = new Mock<IGrandparentSubResolver>();

            var r1 = new DirectLineageResolver(headMock.Object);
            var r2 = new DirectLineageResolver(headMock.Object);

            Assert.IsTrue(r1.Equals(r2));
        }

        [TestMethod]
        public void Equals_UnequalResolvers()
        {
            var headMock = new Mock<IGrandparentSubResolver>();
            var r1 = new DirectLineageResolver(headMock.Object);
            var r2 = new TestRelationshipResolver();

            Assert.IsFalse(r1.Equals(r2));
        }

        [TestMethod]
        public void InPositionRange_ValidPosition()
        {
            var headMock = new Mock<IGrandparentSubResolver>();
            var r = new DirectLineageResolver(headMock.Object);
            int x = 0,
                y = 1;

            Assert.IsTrue(r.InPositionRange(x, y));
        }

        [TestMethod]
        public void InPositionRange_InvalidPositionX()
        {
            var headMock = new Mock<IGrandparentSubResolver>();
            var r = new DirectLineageResolver(headMock.Object);
            int x = 1,
                y = 1;

            Assert.IsFalse(r.InPositionRange(x, y));
        }
    }
}
