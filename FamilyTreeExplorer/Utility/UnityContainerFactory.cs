using FamilyTreeExplorer.Business.FactAlgorithms;
using FamilyTreeExplorer.Business.FactAlgorithms.Interfaces;
using FamilyTreeExplorer.Business.FactAlgorithms.RelationshipNameResolvers;
using FamilyTreeExplorer.Business.FactAlgorithms.RelationshipNameResolvers.DirectLineageSubResolvers;
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

            //relationship sub-resolvers
            container.RegisterType<IChainedSubResolver, ChainedSubResolver>();
            container.RegisterType<IGrandparentSubResolver, GrandparentSubResolver>();
            container.RegisterType<IGreatAuntUncleSubResolver, GreatAuntUncleSubResolver>();
            container.RegisterType<IParentSubResolver, ParentSubResolver>();
            container.RegisterType<IAuntUncleSubResolver, AuntUncleSubResolver>();
            container.RegisterType<ISpouseSubResolver, SpouseSubResolver>();
            container.RegisterType<ISiblingSubResolver, SiblingSubResolver>();
            container.RegisterType<IChildSubResolver, ChildSubResolver>();
            container.RegisterType<INieceNephewSubResolver, NieceNephewSubResolver>();
            container.RegisterType<IGrandchildSubResolver, GrandchildSubResolver>();
            container.RegisterType<IGrandNieceNephewSubResolver, GrandNieceNephewSubResolver>();

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
        }
    }
}
