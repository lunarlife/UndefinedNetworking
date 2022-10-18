using System;
using Networking.Loggers;

namespace UndefinedServer.Loggers
{

    public class MainServerLogger : Logger
    {

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
            Console.Write($"{DateTime.Now:G} ");
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine($"[INFO] {info}");
            Console.ResetColor();
        }

        public override void Warning(string warning)
        {
            Console.Write($"{DateTime.Now:G} ");
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine($"[WARNING] {warning}");
            Console.ResetColor();
        }


        public override void Error(string error)
        {
            Console.Write($"{DateTime.Now:G} ");
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine($"[ERROR] {error}");
            Console.ResetColor();
        }
    }
}