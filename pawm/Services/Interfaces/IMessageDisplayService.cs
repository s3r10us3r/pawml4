using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pawm.Services.Interfaces
{
    public interface IMessageDisplayService
    {
        public Task DisplayMessage(string message);
        public Task<string> AskForText(string title, string message, int length = 100, string defaultText = "");
    }
}
