using log4net;
using Municipalities.Input.Contracts;
using Municipalities.Logging;
using Municipalities.Logging.Contracts;
using Municipalities.Requests.Contracts;
using System;
using System.Threading;
using Unity;

namespace Municipalities
{
    class Program
    {
        private static App _app;

        static void Main(string[] args)
        {
            _app = new App();
            Console.CancelKeyPress += OnCancelPress;
            _app.Run();
        }

        private static void OnCancelPress(object sender, ConsoleCancelEventArgs e)
        {
            _app.ExitApplication();
        }
    }

    class App
    {
        private IUnityContainer _container;
        private IProgramLogger _logger;
        private readonly CancellationTokenSource _cancelTokenSource;

        public App()
        {
            _cancelTokenSource = new CancellationTokenSource();
        }

        public void Run()
        {
            var log4NetLogger = LogManager.GetLogger(typeof(Program));
            var logger = new ProgramLogger(log4NetLogger);

            _container = UnityIoC.Configure(logger);
            _logger = _container.Resolve<IProgramLogger>();

            _logger.Info("Starting Municipalities service. press Ctrl+C to exit...");
            var inputBehaviour = _container.Resolve<IUserInputBehaviour>();
            var requestsHandling = _container.Resolve<IRequestsFacade>();

            var inputBehaviourThread = new Thread(() => inputBehaviour.StartListening(_cancelTokenSource.Token))
            {
                IsBackground = true
            };
            inputBehaviourThread.Start();
            var requestsHandlingThread = new Thread(() => requestsHandling.ListenForRequests(_cancelTokenSource.Token))
            {
                IsBackground = true
            };
            requestsHandlingThread.Start();


            WaitHandle.WaitAny(new[] { _cancelTokenSource.Token.WaitHandle });

            _logger.Info("Shutting down...");
        }

        public void ExitApplication()
        {
            _cancelTokenSource.Cancel();
        }
    }
}
