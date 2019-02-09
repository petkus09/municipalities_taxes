using Municipalities.Logging.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using Unity;

namespace Municipalities
{
    public static class UnityIoC
    {
        public static IUnityContainer Configure(IProgramLogger logger)
        {
            var container = new UnityContainer();

            container.RegisterInstance(logger);

            return container;
        }
    }
}
