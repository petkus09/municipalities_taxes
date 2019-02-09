using System;

namespace Municipalities.Logging.Contracts
{
    public interface IProgramLogger
    {
        void Debug(string message);
        void Info(string message);
        void Warn(string message);
        void Error(string message, Exception ex);
        void Error(string message, Exception ex, string userMessage);
    }
}
