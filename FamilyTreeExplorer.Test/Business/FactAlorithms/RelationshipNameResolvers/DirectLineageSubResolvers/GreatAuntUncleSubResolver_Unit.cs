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
    [TestCategory("GreatAuntUncleSubResolver_Unit")]
    public class GreatAuntUncleSubResolver_Unit
    {
        private GreatAuntUncleSubResolver resolver;
        private Mock<IParentSubResolver> successorMock;
        private IFamilyMember source;

        public GreatAuntUncleSubResolver_Unit()
        {
            successorMock = new Mock<IParentSubResolver>();
            resolver = new GreatAuntUncleSubResolver(successorMock.Object);

            var sourceMock = new Mock<IFamilyMember>();
            sourceMock.Setup(x => x.GetFactValue<int>(FactType.XPosition)).Returns(0);
            sourceMock.Setup(x => x.GetFactValue<int>(FactType.YPosition)).Returns(0);
            source = sourceMock.Object;
        }

        [TestMethod]
        public void Handle_IsGreatAunt()
        {
            var targetMock = new Mock<IFamilyMember>();
            targetMock.Setup(x => x.GetFactValue<int>(FactType.XPosition)).Returns(0);
            targetMock.Setup(x => x.GetFactValue<int>(FactType.YPosition)).Returns(-2);
            targetMock.Setup(x => x.Gender).Returns(Gender.Female);

            Assert.AreEqual("Great Aunt", resolver.Handle(source, targetMock.Object));
        }

        [TestMethod]
        public void Handle_IsGreatUncle()
        {
            var targetMock = new Mock<IFamilyMember>();
            targetMock.Setup(x => x.GetFactValue<int>(FactType.XPosition)).Returns(0);
            targetMock.Setup(x => x.GetFactValue<int>(FactType.YPosition)).Returns(-2);
            targetMock.Setup(x => x.Gender).Returns(Gender.Male);

            Assert.AreEqual("Great Uncle", resolver.Handle(source, targetMock.Object));
        }

        [TestMethod]
        public void Handle_IsNGreatUncle()
        {
            var numberOfGreats = 2;

            var targetMock = new Mock<IFamilyMember>();
            targetMock.Setup(x => x.GetFactValue<int>(FactType.XPosition)).Returns(0);
            targetMock.Setup(x => x.GetFactValue<int>(FactType.YPosition)).Returns(-1 - numberOfGreats);
            targetMock.Setup(x => x.Gender).Returns(Gender.Male);

            var expectedName = string.Concat(Enumerable.Repeat("Great ", numberOfGreats)) + "Uncle";

            Assert.AreEqual(expectedName, resolver.Handle(source, targetMock.Object));
        }

        [TestMethod]
        public void Handle_IsNotGreatAuntOrUncle()
        {
            var targetMock = new Mock<IFamilyMember>();
            targetMock.Setup(x => x.GetFactValue<int>(FactType.XPosition)).Returns(0);
            targetMock.Setup(x => x.GetFactValue<int>(FactType.YPosition)).Returns(-1);

            resolver.Handle(source, targetMock.Object);

            successorMock.Verify(x => x.Handle(It.IsAny<IFamilyMember>(), It.IsAny<IFamilyMember>()), Times.Once);
        }
    }
}
