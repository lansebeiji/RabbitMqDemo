using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace GD.CoursePreparationSystem.Common
{
    public class IocLifetimeScope : ILifetimeScope
    {
        private Autofac.ILifetimeScope scope;
        private IocLifetimeScope(Autofac.ILifetimeScope scope)
        {
            this.scope = scope;
        }

        public static ILifetimeScope CreateLifetimeScope(Autofac.ILifetimeScope scope)
        {
            var iocScope = new IocLifetimeScope(scope);
            CallContextUtility.SetData<ILifetimeScope>(iocScope);
            return iocScope;
        }

        public static ILifetimeScope CurrentLifetimeScope
        {
            get { return CallContextUtility.GetData<ILifetimeScope>(); }
        }

        public TService Resolve<TService>()
        {
            return this.scope.Resolve<TService>();
        }

        public TService Resolve<TService>(string serviceName)
        {
            return this.scope.ResolveNamed<TService>(serviceName);
        }

        public TService Resolve<TService>(IEnumerable<Autofac.Core.Parameter> parameters)
        {
            return this.scope.Resolve<TService>(parameters);
        }

        public TService Resolve<TService>(params Autofac.Core.Parameter[] parameters)
        {
            return this.scope.Resolve<TService>(parameters);
        }

        public TService ResolveP<TService>(string connection)
        {
            return this.scope.Resolve<TService>(new NamedParameter("connStr", connection));
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    CallContextUtility.ClearData<ILifetimeScope>();
                    scope.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
