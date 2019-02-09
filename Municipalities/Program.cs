using log4net;
using Municipalities.Logging;
using Municipalities.Logging.Contracts;
using System;
using Unity;

namespace Municipalities
{
    class Program
    {
        private static IUnityContainer _container;
        private static IProgramLogger _logger;

        static void Main(string[] args)
        {
            var log4NetLogger = LogManager.GetLogger(typeof(Program));
            var logger = new ProgramLogger(log4NetLogger);

            _container = UnityIoC.Configure(logger);
            _logger = _container.Resolve<IProgramLogger>();

            _logger.Info("Starting Municipalities service...");
            _logger.Warn("Testing warn...");
            _logger.Error("Testing error...", new Exception("some error"));
            _logger.Error("Testing error with print to console...", new Exception("some error"), "whoops!");
            _logger.Info("Shutting down...");
        }
    }
}
