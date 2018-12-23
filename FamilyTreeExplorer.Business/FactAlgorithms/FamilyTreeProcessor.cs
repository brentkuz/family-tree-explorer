using FamilyTreeExplorer.Business.FactAlgorithms.Interfaces;
using FamilyTreeExplorer.Business.FamilyTree.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTreeExplorer.Business.FactAlgorithms
{
    public class FamilyTreeProcessor : IFamilyTreeProcessor
    {
        private List<IExecutableAlgorithm> algorithms = new List<IExecutableAlgorithm>();

        public FamilyTreeProcessor(params IExecutableAlgorithm[] algorithmsToExecute)
        {
            foreach (var alg in algorithmsToExecute)
            {
                algorithms.Add(alg);
            }
        }

        public IExecutableAlgorithm[] Algorithms { get { return algorithms.ToArray(); } }

        public void Process(IFamilyTree tree, IFamilyMember source)
        {
            try
            {
                foreach (var alg in algorithms)
                {
                    alg.Execute(tree, source);
                }
            }
            catch (Exception)
            {
                tree.ClearMemberFacts();
                throw;
            }
        }
        
    }
}
