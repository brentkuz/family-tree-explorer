using FamilyTreeExplorer.Business.FactAlgorithms;
using FamilyTreeExplorer.Business.FactAlgorithms.Interfaces;
using FamilyTreeExplorer.Business.FactAlgorithms.RelationshipNameResolvers;
using FamilyTreeExplorer.Business.FamilyTree;
using FamilyTreeExplorer.Business.FamilyTree.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace FamilyTreeExplorer.Crosscutting.ConsoleApp.Utility
{
    public class UnityContainerFactory : UnityContainerFactoryBase
    {
        protected override void Register(IUnityContainer container)
        {

            container.RegisterType<IFindBasicRelationships, FindBasicRelationships>();

            container.RegisterType<ICousinResolver, CousinResolver>();
            container.RegisterType<IDirectLineageResolver, DirectLineageResolver>();
            container.RegisterType<IResolveRelationshipNames, ResolveRelationshipNames>(new ContainerControlledLifetimeManager(),
                new InjectionConstructor(new ResolvedArrayParameter<IRelationshipResolver>(
                    new ResolvedParameter<ICousinResolver>(),
                    new ResolvedParameter<IDirectLineageResolver>()
                    )));
            container.RegisterType<IFamilyTreeProcessor, FamilyTreeProcessor>(new ContainerControlledLifetimeManager(),
                new InjectionConstructor(new ResolvedArrayParameter<IExecutableAlgorithm>(
                    new ResolvedParameter<IFindBasicRelationships>(),
                    new ResolvedParameter<IResolveRelationshipNames>()
                    )));
            //container.RegisterType<>();

            //container.RegisterType<IRelationshipResolver, ResolveRelationshipNames>();
        }
    }
}
