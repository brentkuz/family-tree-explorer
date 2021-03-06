﻿using FamilyTreeExplorer.Business.FactAlgorithms.Interfaces;
using FamilyTreeExplorer.Business.FactAlgorithms.RelationshipNameResolvers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTreeExplorer.Test.FactAlorithms.RelationshipNameResolvers
{
    [TestClass]
    [TestCategory("DirectLineageResolver_Unit")]
    public class DirectLineageResolver_Unit
    {
        [TestMethod]
        public void InPositionRange_SourceFamilyMemberIsInRange()
        {
            int x = 0,
                y = 0;

            var headMock = new Mock<IGrandparentSubResolver>();

            var resolver = new DirectLineageResolver(headMock.Object);
            Assert.IsTrue(resolver.InPositionRange(x, y));
        }

        [TestMethod]
        public void InPositionRange_ValidInputWithNegativeYIsInRange()
        {
            int x = 0,
                y = -1;

            var headMock = new Mock<IGrandparentSubResolver>();

            var resolver = new DirectLineageResolver(headMock.Object);
            Assert.IsTrue(resolver.InPositionRange(x, y));
        }

        [TestMethod]
        public void InPositionRange_OutOfRange()
        {
            int x = 1,
                y = -1;

            var headMock = new Mock<IGrandparentSubResolver>();

            var resolver = new DirectLineageResolver(headMock.Object);
            Assert.IsFalse(resolver.InPositionRange(x, y));
        }
    }
}
