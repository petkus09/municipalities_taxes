using log4net;
using log4net.Config;
using Municipalities.Logging.Contracts;
using System;
using System.IO;
using System.Reflection;

namespace Municipalities.Logging
{
    public class ProgramLogger : IProgramLogger
    {
        private readonly ILog _logger;

        public ProgramLogger(ILog logger)
        {
            _logger = logger;
            var executingAssembly = Assembly.GetEntryAssembly();
            var repo = LogManager.GetRepository(executingAssembly);
            var configurationFile = new FileInfo("log4net.config");
            XmlConfigurator.Configure(repo, configurationFile);
        }

        public void Info(string message) => _logger.Info(message);

        public void Warn(string message) => _logger.Warn(message);

        public void Error(string message, Exception ex) => _logger.Error(message, ex);

        public void Error(string message, Exception ex, string userMessage)
        {
            _logger.Warn(userMessage);
            _logger.Error(message, ex);
        }
    }
}
