using System;

namespace Municipalities.Logging.Contracts
{
    public interface IProgramLogger
    {
        void Info(string message);
        void Warn(string message);
        void Error(string message, Exception ex);
        void Error(string message, Exception ex, string userMessage);
    }
}
