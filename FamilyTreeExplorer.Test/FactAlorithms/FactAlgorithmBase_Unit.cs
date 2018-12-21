using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using FamilyTreeExplorer.Business.Objects.Interfaces;
using FamilyTreeExplorer.Business.FactAlgorithms;
using FamilyTreeExplorer.Business.Objects;
using FamilyTreeExplorer.Business.Objects.Relationships;
using FamilyTreeExplorer.Crosscutting.Enums;

namespace FamilyTreeExplorer.Test.FactAlorithms
{
    [TestClass]
    [TestCategory("FactAlgorithmBase_Unit")]
    public class FactAlgorithmBase_Unit
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Ctor_NullTree()
        {
            var membMock = new Mock<IFamilyMember>();
            new FindBasicRelationships(null, membMock.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Ctor_NullTreeRoot()
        {
            var treeMock = new Mock<IFamilyTree>();
            var membMock = new Mock<IFamilyMember>();
            new FindBasicRelationships(treeMock.Object, membMock.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(NotInFamilyTreeException))]
        public void Ctor_SourceNotInTree()
        {
            var treeMock = new Mock<IFamilyTree>();
            treeMock.Setup(x => x.Root).Returns(new Partnership(new FamilyMember(), new FamilyMember()));
            treeMock.Setup(x => x.MemberExists(It.IsAny<IFamilyMember>())).Returns(false);
            var membMock = new Mock<IFamilyMember>();
            new FindBasicRelationships(treeMock.Object, membMock.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidSourceException))]
        public void Ctor_SourceIsInlaw()
        {
            var treeMock = new Mock<IFamilyTree>();
            treeMock.Setup(x => x.Root).Returns(new Partnership(new FamilyMember(), new FamilyMember()));
            treeMock.Setup(x => x.MemberExists(It.IsAny<IFamilyMember>())).Returns(true);
            var membMock = new Mock<IFamilyMember>();
            membMock.Setup(x => x.HasFact(It.Is<FactType>(t => t == FactType.InLaw))).Returns(true);
            new FindBasicRelationships(treeMock.Object, membMock.Object);
        }
    }
}
