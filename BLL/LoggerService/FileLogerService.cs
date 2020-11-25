using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BLL.LoggerService
{
    public class FileLogerService:ILogerService
    {
        public Task RunTaskEcho() =>
            Task.Run(() =>
            {
                while (true)
                {
                    Debug.WriteLine("Echo");
                    Task.Delay(1000).Wait();
                }
            });
        Queue<string> queue = new Queue<string>();
        static Object locker = new Object();
        public Task WriteTextToLog(string _text) {
            lock (locker) {
                File.AppendAllText("logs.txt", _text);
            }
            return Task.Delay(10000);
        }
    }
}
