using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.LoggerService
{
    public class FileLogerService:ILogerService
    {
        Queue<string> queueMessage = new Queue<string>();
        static Object lockerQueue = new Object();
        static Object lockerEcho = new Object();
        public Task RunTaskEcho() =>
            Task.Run(() =>
            {
                string textLog = String.Empty;
                while (true)
                {
                    lock (lockerQueue)
                    {
                        if (queueMessage.Count > 0)
                        {
                            textLog = queueMessage.Dequeue();
                        }
                    }
                    if (!String.IsNullOrEmpty(textLog)) {
                        WriteTextToLog(queueMessage.Dequeue());
                    }
                    textLog = String.Empty;
                }
            });
        public void WriteTextToLog(string _text) {
            File.AppendAllText("logs.txt", _text);
            Task.Delay(10000);
        }
        public Task SendMessageToLog(string _message) =>
            Task.Run(() =>
            {
                lock (lockerQueue)
                {
                    queueMessage.Enqueue(_message);
                }
            });
    }
}
