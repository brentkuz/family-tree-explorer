using System;
using System.Collections.Generic;
using System.Text;
using Unity;

namespace FamilyTreeExplorer.Crosscutting.ConsoleApp.Utility
{
    public abstract class UnityContainerFactoryBase
    {
        public IUnityContainer GetContainer()
        {
            var container = new UnityContainer();
            Register(container);
            return container;
        }

        protected abstract void Register(IUnityContainer container);
    }
}
