using log4net;
using Municipalities.Input.Contracts;
using Municipalities.Logging;
using Municipalities.Logging.Contracts;
using System;
using System.Collections.Concurrent;
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
        private readonly BlockingCollection<Action> _requests;
        private readonly CancellationTokenSource _cancelTokenSource;

        public App()
        {
            _requests = new BlockingCollection<Action>();
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
            inputBehaviour.RequestSoftwareExit += ExitApplication;

            var inputBehaviourThread = new Thread(inputBehaviour.StartListening)
            {
                IsBackground = true
            };
            inputBehaviourThread.Start();

            ProcessRequests();
            _requests.Dispose();
            inputBehaviourThread.Abort();

            _logger.Info("Shutting down...");
        }

        private void ProcessRequests()
        {
            var token = _cancelTokenSource.Token;
            while (!_requests.IsCompleted)
            {
                _requests.Take(token).Invoke();
            }
        }

        public void ExitApplication()
        {
            _cancelTokenSource.Cancel();
        }
    }
}
