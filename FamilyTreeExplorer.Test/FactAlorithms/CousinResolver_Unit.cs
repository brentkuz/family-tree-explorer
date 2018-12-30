using FamilyTreeExplorer.Business.FactAlgorithms.RelationshipNameResolvers;
using FamilyTreeExplorer.Business.FamilyTree.Interfaces;
using FamilyTreeExplorer.Crosscutting.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTreeExplorer.Test.FactAlorithms
{
    [TestClass]
    [TestCategory("CousinResolver_Unit")]
    public class CousinResolver_Unit
    {
        [TestMethod]
        public void InPositionRange_InRange()
        {
            int x = 1,
                y = 0;

            var resolver = new CousinResolver();
            Assert.IsTrue(resolver.InPositionRange(x, y));

        }
        [TestMethod]
        public void InPositionRange_OutOfRange()
        {
            int x = 0,
                y = 0;

            var resolver = new CousinResolver();
            Assert.IsFalse(resolver.InPositionRange(x, y));

        }

        [TestMethod]
        public void Execute_FirstCousin()
        {
            var resolver = new CousinResolver();

            var sourceMock = GetSourceMock();
            var targetMock = new Mock<IFamilyMember>();
            targetMock.Setup(x => x.GetFactValue<int>(FactType.XPosition)).Returns(1);
            targetMock.Setup(x => x.GetFactValue<int>(FactType.YPosition)).Returns(0);

            Assert.AreEqual("1 Cousin", resolver.Execute(sourceMock.Object, targetMock.Object));
        }

        [TestMethod]
        public void Execute_FirstCousinOnceRemovedAboveSource()
        {
            var resolver = new CousinResolver();

            var sourceMock = GetSourceMock();
            var targetMock = new Mock<IFamilyMember>();
            targetMock.Setup(x => x.GetFactValue<int>(FactType.XPosition)).Returns(1);
            targetMock.Setup(x => x.GetFactValue<int>(FactType.YPosition)).Returns(-1);

            Assert.AreEqual("1 Cousin 1 Removed", resolver.Execute(sourceMock.Object, targetMock.Object));
        }

        [TestMethod]
        public void Execute_FirstCousinOnceRemovedBelowSource()
        {
            var resolver = new CousinResolver();

            var sourceMock = GetSourceMock();
            var targetMock = new Mock<IFamilyMember>();
            targetMock.Setup(x => x.GetFactValue<int>(FactType.XPosition)).Returns(1);
            targetMock.Setup(x => x.GetFactValue<int>(FactType.YPosition)).Returns(1);

            Assert.AreEqual("1 Cousin 1 Removed", resolver.Execute(sourceMock.Object, targetMock.Object));
        }

        [TestMethod]
        public void Execute_FirstCousinTwiceRemovedAboveSource()
        {
            var resolver = new CousinResolver();

            var sourceMock = GetSourceMock();
            var targetMock = new Mock<IFamilyMember>();
            targetMock.Setup(x => x.GetFactValue<int>(FactType.XPosition)).Returns(1);
            targetMock.Setup(x => x.GetFactValue<int>(FactType.YPosition)).Returns(-2);

            Assert.AreEqual("1 Cousin 2 Removed", resolver.Execute(sourceMock.Object, targetMock.Object));
        }

        [TestMethod]
        public void Execute_FirstCousinTwiceRemovedBelowSource()
        {
            var resolver = new CousinResolver();

            var sourceMock = GetSourceMock();
            var targetMock = new Mock<IFamilyMember>();
            targetMock.Setup(x => x.GetFactValue<int>(FactType.XPosition)).Returns(1);
            targetMock.Setup(x => x.GetFactValue<int>(FactType.YPosition)).Returns(2);

            Assert.AreEqual("1 Cousin 2 Removed", resolver.Execute(sourceMock.Object, targetMock.Object));
        }

        [TestMethod]
        public void Execute_NCousinMRemoved()
        {
            int n = 3,
                m = 4;

            var resolver = new CousinResolver();

            var sourceMock = GetSourceMock();
            var targetMock = new Mock<IFamilyMember>();
            targetMock.Setup(x => x.GetFactValue<int>(FactType.XPosition)).Returns(n);
            targetMock.Setup(x => x.GetFactValue<int>(FactType.YPosition)).Returns(m);

            Assert.AreEqual(string.Format("{0} Cousin {1} Removed", n, m), resolver.Execute(sourceMock.Object, targetMock.Object));
        }

        private Mock<IFamilyMember> GetSourceMock()
        {
            var sourceMock = new Mock<IFamilyMember>();
            sourceMock.Setup(x => x.GetFactValue<int>(FactType.XPosition)).Returns(0);
            sourceMock.Setup(x => x.GetFactValue<int>(FactType.YPosition)).Returns(0);
            return sourceMock;
        }
    }
}
