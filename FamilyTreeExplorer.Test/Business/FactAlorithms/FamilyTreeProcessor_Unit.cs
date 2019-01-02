using FamilyTreeExplorer.Business.FactAlgorithms;
using FamilyTreeExplorer.Business.FactAlgorithms.Interfaces;
using FamilyTreeExplorer.Business.FamilyTree.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTreeExplorer.Test.FactAlorithms
{
    [TestClass]
    [TestCategory("FamilyTreeProcessor_Unit")]
    public class FamilyTreeProcessor_Unit
    {
        [TestMethod]
        public void Ctor_InjectedAlgoritmsAddedToPrivateList()
        {
            var algs = new IExecutableAlgorithm[] {
                new FindBasicRelationships(),
                new ResolveRelationshipNames()
            };
            var proc = new FamilyTreeProcessor(algs);

            Assert.AreEqual(algs.Length, proc.Algorithms.Length);
        }

        [TestMethod]
        public void Execute_IfAnAlgorithmFailsFactsAreCleared()
        {
            var findBasicMock = new Mock<IFindBasicRelationships>();
            var resolveMock = new Mock<IResolveRelationshipNames>();
            resolveMock.Setup(x => x.Execute(It.IsAny<IFamilyTree>(), It.IsAny<IFamilyMember>())).Throws(new Exception());

            var treeMock = new Mock<IFamilyTree>();
            treeMock.Setup(x => x.ClearMemberFacts());
            var membMock = new Mock<IFamilyMember>();            

            var proc = new FamilyTreeProcessor(new IExecutableAlgorithm[] { findBasicMock.Object, resolveMock.Object });

            Assert.ThrowsException<Exception>(new Action(() => { 
                proc.Process(treeMock.Object, membMock.Object);
            }));

            treeMock.Verify(x => x.ClearMemberFacts(), Times.Once);            
        }
    }
}
