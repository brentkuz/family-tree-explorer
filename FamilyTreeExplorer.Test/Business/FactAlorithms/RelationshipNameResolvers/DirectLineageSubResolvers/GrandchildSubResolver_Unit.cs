using FamilyTreeExplorer.Business.FactAlgorithms.Interfaces;
using FamilyTreeExplorer.Business.FactAlgorithms.RelationshipNameResolvers.DirectLineageSubResolvers;
using FamilyTreeExplorer.Business.FamilyTree.Interfaces;
using FamilyTreeExplorer.Crosscutting.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FamilyTreeExplorer.Test.Business.FactAlorithms.RelationshipNameResolvers.DirectLineageSubResolvers
{
    [TestClass]
    [TestCategory("GrandchildSubResolver_Unit")]
    public class GrandchildSubResolver_Unit
    {
        private GrandchildSubResolver resolver;
        private Mock<IGrandNieceNephewSubResolver> successorMock;
        private Mock<IFamilyMember> sourceMock;
        private IFamilyMember source;

        public GrandchildSubResolver_Unit()
        {
            successorMock = new Mock<IGrandNieceNephewSubResolver>();
            resolver = new GrandchildSubResolver(successorMock.Object);

            sourceMock = new Mock<IFamilyMember>();
            sourceMock.Setup(x => x.GetFactValue<int>(FactType.XPosition)).Returns(0);
            sourceMock.Setup(x => x.GetFactValue<int>(FactType.YPosition)).Returns(0);
            source = sourceMock.Object;
        }

        [TestMethod]
        public void Handle_IsGrandchild()
        {
            var targetMock = new Mock<IFamilyMember>();
            targetMock.Setup(x => x.GetFactValue<int>(FactType.XPosition)).Returns(0);
            targetMock.Setup(x => x.GetFactValue<int>(FactType.YPosition)).Returns(2);
            targetMock.Setup(x => x.HasFact(FactType.Ancestor)).Returns(true);

            Assert.AreEqual("Grandchild", resolver.Handle(source, targetMock.Object));
        }

        [TestMethod]
        public void Handle_IsNGreatGrandchild()
        {
            var numberOfGreats = 2;

            var targetMock = new Mock<IFamilyMember>();
            targetMock.Setup(x => x.GetFactValue<int>(FactType.XPosition)).Returns(0);
            targetMock.Setup(x => x.GetFactValue<int>(FactType.YPosition)).Returns(2 + numberOfGreats);
            targetMock.Setup(x => x.HasFact(FactType.Ancestor)).Returns(true);

            var expectedName = string.Concat(Enumerable.Repeat("Great ", numberOfGreats)) + "Grandchild";

            Assert.AreEqual(expectedName, resolver.Handle(source, targetMock.Object));
        }

        [TestMethod]
        public void Handle_IsNotGrandchild()
        {
            var targetMock = new Mock<IFamilyMember>();
            targetMock.Setup(x => x.GetFactValue<int>(FactType.XPosition)).Returns(0);
            targetMock.Setup(x => x.GetFactValue<int>(FactType.YPosition)).Returns(1);

            resolver.Handle(source, targetMock.Object);

            successorMock.Verify(x => x.Handle(It.IsAny<IFamilyMember>(), It.IsAny<IFamilyMember>()), Times.Once);
        }
    }
}
