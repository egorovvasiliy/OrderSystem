using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.LoggerService
{
    public interface ILogerService
    {
        public Task WriteTextToLog(string textLog);
    }
}
