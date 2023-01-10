using System;
using System.Reflection;
using Networking.Loggers;

namespace UndefinedServer.Loggers
{

    public class MainServerLogger : Logger
    {
        private static object _sendLock = new();
        public MainServerLogger()
        {
            AppDomain.CurrentDomain.UnhandledException +=
                (sender, args) =>
                {
                    if(args.ExceptionObject is Exception exception)
                        Error(exception.Message);
                };
        }
        public override void Info(string info)
        {
            lock(_sendLock)
            {
                Console.Write($"{DateTime.Now:G} ");
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine($"[INFO] {info}");
                Console.ResetColor();
            }
            
        }

        public override void Warning(string warning)
        {
            lock (_sendLock)
            {
                Console.Write($"{DateTime.Now:G} ");
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine($"[WARNING] {warning}");
                Console.ResetColor();
            }
        }


        public override void Error(string error)
        {
            lock (_sendLock)
            {
                Console.Write($"{DateTime.Now:G} ");
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine($"[ERROR] {error}");
                Console.ResetColor();
            }
        }

        public override void Error(Exception e)
        {
            switch (e)
            {
                case TargetInvocationException { InnerException: { } } invocationException:
                    Undefined.Logger.Error($"{invocationException.InnerException.Message}\n{invocationException.InnerException.StackTrace}");
                    break;
                default:
                    Undefined.Logger.Error($"{e.Message}\n{e.StackTrace}");
                    break;
            }
        }
    }
}