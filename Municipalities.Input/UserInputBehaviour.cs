using Municipalities.Cmd.Models;
using Municipalities.Input.Contracts;
using Municipalities.Logging.Contracts;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace Municipalities.Input
{
    public class UserInputBehaviour : IUserInputBehaviour
    {
        private readonly IProgramLogger _log;
        private readonly ICommandMapper _mapper;

        public UserInputBehaviour(IProgramLogger log, ICommandMapper mapper)
        {
            _log = log;
            _mapper = mapper;
        }

        public void StartListening(CancellationToken cancelToken)
        {
            PrintHelp();

            var commandReadThread = new Thread(() => ReadLine(cancelToken))
            {
                IsBackground = true
            };
            try
            {
                commandReadThread.Start();
            }
            catch (Exception ex)
            {
                _log.Error(
                    "Unexpected error occured while starting listening threads",
                    ex,
                    "Whoops. Something went wrong!"
                );
            }
            finally
            {
                WaitHandle.WaitAny(new[] { cancelToken.WaitHandle });
                commandReadThread.Abort();
            }
        }

        private void PrintHelp()
        {
            _mapper.ExecuteCommand(new string[] { "help" });
        }

        private void ReadLine(CancellationToken cancelToken)
        {
            while (!cancelToken.IsCancellationRequested)
            {
                try
                {
                    _log.Info("Type your command...");
                    var line = Console.ReadLine();
                    _log.Debug(line);
                    var args = StrToStringArgsParser.GetArgumentsFromString(line);
                    _mapper.ExecuteCommand(args);
                }
                catch(System.ComponentModel.Win32Exception ex)
                {
                    _log.Error("Unexpected error occured while trying to parse arguments", ex, "Error occured when trying to parse arguments. Please try again");
                }
                catch(CmdCommandExecuteArgumentException ex)
                {
                    _log.Error("Validation error occured", ex, ex.ValidationMessage);
                }
                catch (Exception ex)
                {
                    _log.Error("Unexpected error occured", ex);
                }
            }
        }
    }
    
    //Ugly implementation of parsing string to command-line arguments
    //Since default command-line provides easy way of retrieving args[] in Main
    //We need a raw method which would parse argument-like array from the string
    public static class StrToStringArgsParser
    {
        [DllImport("shell32.dll", SetLastError = true)]
        static extern IntPtr CommandLineToArgvW(
        [MarshalAs(UnmanagedType.LPWStr)] string lpCmdLine, out int pNumArgs);

        public static string[] GetArgumentsFromString(string commandLine)
        {
            int numberOfArguments;
            var pointer = CommandLineToArgvW(commandLine, out numberOfArguments);
            if (pointer == IntPtr.Zero)
                throw new System.ComponentModel.Win32Exception();
            try
            {
                var resultArguments = new string[numberOfArguments];
                for (var i = 0; i < resultArguments.Length; i++)
                {
                    var argument = Marshal.ReadIntPtr(pointer, i * IntPtr.Size);
                    resultArguments[i] = Marshal.PtrToStringUni(argument);
                }

                return resultArguments;
            }
            finally
            {
                Marshal.FreeHGlobal(pointer);
            }
        }
    }
}
