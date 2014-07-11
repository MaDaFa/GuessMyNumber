using Autofac;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GuessMyNumber.WebServer
{
    public class GameDependencyResolver : DefaultDependencyResolver
    {
        private readonly IContainer dependencyContainer;

        public GameDependencyResolver(IContainer dependencyContainer)
        {
            this.dependencyContainer = dependencyContainer;
        }

        public override object GetService(Type serviceType)
        {
            var service = default(object);

            if (!this.dependencyContainer.TryResolve(serviceType, out service))
            {
                service = base.GetService(serviceType);
            }

            return service;
        }

        public override IEnumerable<object> GetServices(Type serviceType)
        {
            var services = new List<object>();
            var service = default(object);

            if (this.dependencyContainer.TryResolve(serviceType, out service))
            {
                services.Add(service);
            }

            return services.Concat(base.GetServices(serviceType));
        }
    }
}