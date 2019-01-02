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
    [TestCategory("GrandNieceNephewSubResolver_Unit")]
    public class GrandNieceNephewSubResolver_Unit
    {
        private GrandNieceNephewSubResolver resolver;
        private Mock<ITerminalSubResolver> successorMock;
        private Mock<IFamilyMember> sourceMock;
        private IFamilyMember source;

        public GrandNieceNephewSubResolver_Unit()
        {
            successorMock = new Mock<ITerminalSubResolver>();
            resolver = new GrandNieceNephewSubResolver(successorMock.Object);

            sourceMock = new Mock<IFamilyMember>();
            sourceMock.Setup(x => x.GetFactValue<int>(FactType.XPosition)).Returns(0);
            sourceMock.Setup(x => x.GetFactValue<int>(FactType.YPosition)).Returns(0);
            source = sourceMock.Object;
        }

        [TestMethod]
        public void Handle_IsGrandniece()
        {
            var targetMock = new Mock<IFamilyMember>();
            targetMock.Setup(x => x.GetFactValue<int>(FactType.XPosition)).Returns(0);
            targetMock.Setup(x => x.GetFactValue<int>(FactType.YPosition)).Returns(2);
            targetMock.Setup(x => x.Gender).Returns(Gender.Female);

            Assert.AreEqual("Grandniece", resolver.Handle(source, targetMock.Object));
        }

        [TestMethod]
        public void Handle_IsGrandnephew()
        {
            var targetMock = new Mock<IFamilyMember>();
            targetMock.Setup(x => x.GetFactValue<int>(FactType.XPosition)).Returns(0);
            targetMock.Setup(x => x.GetFactValue<int>(FactType.YPosition)).Returns(2);
            targetMock.Setup(x => x.Gender).Returns(Gender.Male);

            Assert.AreEqual("Grandnephew", resolver.Handle(source, targetMock.Object));
        }

        [TestMethod]
        public void Handle_IsNGreatGrandniece()
        {
            var numberOfGreats = 2;

            var targetMock = new Mock<IFamilyMember>();
            targetMock.Setup(x => x.GetFactValue<int>(FactType.XPosition)).Returns(0);
            targetMock.Setup(x => x.GetFactValue<int>(FactType.YPosition)).Returns(2 + numberOfGreats);
            targetMock.Setup(x => x.Gender).Returns(Gender.Female);

            var expectedName = string.Concat(Enumerable.Repeat("Great ", numberOfGreats)) + "Grandniece";

            Assert.AreEqual(expectedName, resolver.Handle(source, targetMock.Object));
        }

        [TestMethod]
        public void Handle_IsNotGrandnieceOrGrandnephew()
        {
            var targetMock = new Mock<IFamilyMember>();
            targetMock.Setup(x => x.GetFactValue<int>(FactType.XPosition)).Returns(0);
            targetMock.Setup(x => x.GetFactValue<int>(FactType.YPosition)).Returns(1);

            resolver.Handle(source, targetMock.Object);

            successorMock.Verify(x => x.Handle(It.IsAny<IFamilyMember>(), It.IsAny<IFamilyMember>()), Times.Once);
        }
    }
}
