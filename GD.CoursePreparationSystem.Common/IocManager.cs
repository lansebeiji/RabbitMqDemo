using Autofac;
using System.Linq;
using System.Reflection;
using System;
using System.Collections.Generic;
using Autofac.Core;

namespace GD.CoursePreparationSystem.Common
{
    public class IocManager
    {
        private IocManager()
        {
        }

        private static IocManager _iocManager;

        public IContainer Container { get; private set; }

        static IocManager()
        {
            _iocManager = new IocManager()
            {
                ContainerBuilder = new ContainerBuilder(),
            };
        }

        public static IocManager Instance
        {
            get
            {
                return _iocManager;
            }
        }

        public ContainerBuilder ContainerBuilder { get; private set; }

       
        public IocManager RegisterAutofacModules(params Assembly[] assemblies)
        {
            ContainerBuilder.RegisterAssemblyModules(assemblies);
            

            return this;
        }

        public IContainer Build()
        {
            if (Container != null) return Container;
            Container = ContainerBuilder.Build();
            return Container;
        }

        public static TService Resolve<TService>()
        {
            var scope = IocLifetimeScope.CurrentLifetimeScope;
            if (scope != null)
              return  scope.Resolve<TService>();
            return IocManager.Instance.Container.Resolve<TService>();
        }

        public static TService Resolve<TService>(string serviceName)
        {
            var scope = IocLifetimeScope.CurrentLifetimeScope;
            if (scope != null)
                return scope.Resolve<TService>(serviceName);
            return IocManager.Instance.Container.ResolveNamed<TService>(serviceName);
        }

        public static TService Resolve<TService>(IEnumerable<Parameter> parameters)
        {
            var scope = IocLifetimeScope.CurrentLifetimeScope;
            if (scope != null)
                return scope.Resolve<TService>(parameters);
            return IocManager.Instance.Container.Resolve<TService>(parameters);
        }

        public static TService Resolve<TService>(params Parameter[] parameters)
        {
            var scope = IocLifetimeScope.CurrentLifetimeScope;
            if (scope != null)
                return scope.Resolve<TService>(parameters);
            return IocManager.Instance.Container.Resolve<TService>(parameters);
        }
        public static ILifetimeScope BeginLifetimeScope()
        {
            var scope = IocManager.Instance.Container.BeginLifetimeScope();
            return IocLifetimeScope.CreateLifetimeScope(scope);
        }
    }
}
