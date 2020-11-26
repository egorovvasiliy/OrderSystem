using BLL.Constants;
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
        public Task SendMessageToLog(string _message) =>   //ILogerService
            Task.Run(() =>
            {
                lock (lockerQueue)
                {
                    queueMessage.Enqueue($"{DateTime.Now.ToString()}: {_message}\n");
                }
            });
        public Task RunTaskEcho() =>                      //ILogerService
            Task.Run(() =>
            {
                string textLog = String.Empty;
                while (true)
                {
                    //В этом месте месте может понадобиться установить задержку в милисекундах,чтобы дать возможность отработать очереди из "тасков SendMessageToLog"
                    //т.к. при
                    lock (lockerQueue)
                    {
                        if (queueMessage.Count > 0)
                        {
                            textLog = queueMessage.Dequeue();
                        }
                    }
                    if (!String.IsNullOrEmpty(textLog)) {
                        WriteTextToLog(textLog);
                        textLog = String.Empty;
                    }
                }
            });
        public void WriteTextToLog(string _text) {
            Task.Delay(IntervalParams.delayWriteLog).Wait();
            File.AppendAllText("logs.txt", _text);
        }
    }
}
