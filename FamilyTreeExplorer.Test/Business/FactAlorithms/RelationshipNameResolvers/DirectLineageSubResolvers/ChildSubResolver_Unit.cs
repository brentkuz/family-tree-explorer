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
    [TestCategory("ChildSubResolver_Unit")]
    public class ChildSubResolver_Unit
    {
        private ChildSubResolver resolver;
        private Mock<INieceNephewSubResolver> successorMock;
        private Mock<IFamilyMember> sourceMock;
        private IFamilyMember source;

        public ChildSubResolver_Unit()
        {
            successorMock = new Mock<INieceNephewSubResolver>();
            resolver = new ChildSubResolver(successorMock.Object);

            sourceMock = new Mock<IFamilyMember>();
            sourceMock.Setup(x => x.GetFactValue<int>(FactType.XPosition)).Returns(0);
            sourceMock.Setup(x => x.GetFactValue<int>(FactType.YPosition)).Returns(0);
            source = sourceMock.Object;
        }

        [TestMethod]
        public void Handle_IsChild()
        {
            var targetMock = new Mock<IFamilyMember>();
            targetMock.Setup(x => x.GetFactValue<int>(FactType.XPosition)).Returns(0);
            targetMock.Setup(x => x.GetFactValue<int>(FactType.YPosition)).Returns(1);
            targetMock.Setup(x => x.HasFact(FactType.Ancestor)).Returns(true);

            Assert.AreEqual("Child", resolver.Handle(source, targetMock.Object));
        }

        [TestMethod]
        public void Handle_IsNotSibling()
        {
            var targetMock = new Mock<IFamilyMember>();
            targetMock.Setup(x => x.GetFactValue<int>(FactType.XPosition)).Returns(0);
            targetMock.Setup(x => x.GetFactValue<int>(FactType.YPosition)).Returns(2);

            resolver.Handle(source, targetMock.Object);

            successorMock.Verify(x => x.Handle(It.IsAny<IFamilyMember>(), It.IsAny<IFamilyMember>()), Times.Once);
        }
    }
}
