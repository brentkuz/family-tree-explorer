using FamilyTreeExplorer.Business.FactAlgorithms.Interfaces;
using FamilyTreeExplorer.Business.FactAlgorithms.RelationshipNameResolvers.DirectLineageSubResolvers;
using FamilyTreeExplorer.Business.FamilyTree.Interfaces;
using FamilyTreeExplorer.Crosscutting.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTreeExplorer.Test.Business.FactAlorithms.RelationshipNameResolvers.DirectLineageSubResolvers
{
    [TestClass]
    [TestCategory("NieceNephewResolver_Unit")]
    public class NieceNephewResolver_Unit
    {
        private NieceNephewSubResolver resolver;
        private Mock<IGrandchildSubResolver> successorMock;
        private Mock<IFamilyMember> sourceMock;
        private IFamilyMember source;

        public NieceNephewResolver_Unit()
        {
            successorMock = new Mock<IGrandchildSubResolver>();
            resolver = new NieceNephewSubResolver(successorMock.Object);

            sourceMock = new Mock<IFamilyMember>();
            sourceMock.Setup(x => x.GetFactValue<int>(FactType.XPosition)).Returns(0);
            sourceMock.Setup(x => x.GetFactValue<int>(FactType.YPosition)).Returns(0);
            source = sourceMock.Object;
        }

        [TestMethod]
        public void Handle_IsNiece()
        {
            var targetMock = new Mock<IFamilyMember>();
            targetMock.Setup(x => x.GetFactValue<int>(FactType.XPosition)).Returns(0);
            targetMock.Setup(x => x.GetFactValue<int>(FactType.YPosition)).Returns(1);
            targetMock.Setup(x => x.Gender).Returns(Gender.Female);

            Assert.AreEqual("Niece", resolver.Handle(source, targetMock.Object));
        }

        [TestMethod]
        public void Handle_IsNephew()
        {
            var targetMock = new Mock<IFamilyMember>();
            targetMock.Setup(x => x.GetFactValue<int>(FactType.XPosition)).Returns(0);
            targetMock.Setup(x => x.GetFactValue<int>(FactType.YPosition)).Returns(1);
            targetMock.Setup(x => x.Gender).Returns(Gender.Male);

            Assert.AreEqual("Nephew", resolver.Handle(source, targetMock.Object));
        }

        [TestMethod]
        public void Handle_IsNotNieceOrNephew()
        {
            var targetMock = new Mock<IFamilyMember>();
            targetMock.Setup(x => x.GetFactValue<int>(FactType.XPosition)).Returns(0);
            targetMock.Setup(x => x.GetFactValue<int>(FactType.YPosition)).Returns(2);

            resolver.Handle(source, targetMock.Object);

            successorMock.Verify(x => x.Handle(It.IsAny<IFamilyMember>(), It.IsAny<IFamilyMember>()), Times.Once);
        }
    }
}
