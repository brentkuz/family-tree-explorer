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
    [TestCategory("AuntUncleSubResolver_Unit")]
    public class AuntUncleSubResolver_Unit
    {
        private AuntUncleSubResolver resolver;
        private Mock<ISpouseSubResolver> successorMock;
        private IFamilyMember source;

        public AuntUncleSubResolver_Unit()
        {
            successorMock = new Mock<ISpouseSubResolver>();
            resolver = new AuntUncleSubResolver(successorMock.Object);

            var sourceMock = new Mock<IFamilyMember>();
            sourceMock.Setup(x => x.GetFactValue<int>(FactType.XPosition)).Returns(0);
            sourceMock.Setup(x => x.GetFactValue<int>(FactType.YPosition)).Returns(0);
            source = sourceMock.Object;
        }

        [TestMethod]
        public void Handle_IsAunt()
        {
            var targetMock = new Mock<IFamilyMember>();
            targetMock.Setup(x => x.GetFactValue<int>(FactType.XPosition)).Returns(0);
            targetMock.Setup(x => x.GetFactValue<int>(FactType.YPosition)).Returns(-1);
            targetMock.Setup(x => x.Gender).Returns(Gender.Female);

            Assert.AreEqual("Aunt", resolver.Handle(source, targetMock.Object));
        }

        [TestMethod]
        public void Handle_IsUncle()
        {
            var targetMock = new Mock<IFamilyMember>();
            targetMock.Setup(x => x.GetFactValue<int>(FactType.XPosition)).Returns(0);
            targetMock.Setup(x => x.GetFactValue<int>(FactType.YPosition)).Returns(-1);
            targetMock.Setup(x => x.Gender).Returns(Gender.Male);

            Assert.AreEqual("Uncle", resolver.Handle(source, targetMock.Object));
        }

        [TestMethod]
        public void Handle_IsNotAuntOrUncle()
        {
            var targetMock = new Mock<IFamilyMember>();
            targetMock.Setup(x => x.GetFactValue<int>(FactType.XPosition)).Returns(0);
            targetMock.Setup(x => x.GetFactValue<int>(FactType.YPosition)).Returns(-2);

            resolver.Handle(source, targetMock.Object);

            successorMock.Verify(x => x.Handle(It.IsAny<IFamilyMember>(), It.IsAny<IFamilyMember>()), Times.Once);
        }
    }
}
