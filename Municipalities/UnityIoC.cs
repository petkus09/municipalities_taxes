using Municipalities.Cmd.Service;
using Municipalities.Cmd.Service.Contracts;
using Municipalities.Data;
using Municipalities.Data.Contracts;
using Municipalities.Input;
using Municipalities.Input.Contracts;
using Municipalities.Logging.Contracts;
using Municipalities.Requests;
using Municipalities.Requests.Contracts;
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

            container.RegisterType<IRequestsFacade, RequestsFacade>(new ContainerControlledLifetimeManager());
            container.RegisterType<INewRecordRequests, NewRecordRequests>(new ContainerControlledLifetimeManager());

            container.RegisterType<IAppData, AppData>(new ContainerControlledLifetimeManager());

            return container;
        }
    }
}
