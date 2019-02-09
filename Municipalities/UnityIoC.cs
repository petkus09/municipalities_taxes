using Municipalities.Cmd.Service;
using Municipalities.Cmd.Service.Contracts;
using Municipalities.Input;
using Municipalities.Input.Contracts;
using Municipalities.Logging.Contracts;
using Unity;
using Unity.Lifetime;

namespace Municipalities
{
    public static class UnityIoC
    {
        public static IUnityContainer Configure(IProgramLogger logger)
        {
            var container = new UnityContainer();

            container.RegisterInstance<IProgramLogger>(logger);
            container.AddExtension(new Diagnostic());
            container.RegisterType<IUserInputBehaviour, UserInputBehaviour>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICommandMapper, CommandMapper>(new ContainerControlledLifetimeManager());

            container.RegisterType<IAddCommand, AddCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IGetCommand, GetCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IImportCommand, ImportCommand>(new ContainerControlledLifetimeManager());

            return container;
        }
    }
}
