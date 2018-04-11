using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinNavigation.Utils
{
    public class AsyncCommand : Command
    {
        public AsyncCommand(Func<Task> execute) : base(() => execute())
        {
        }
        public AsyncCommand(Func<object, Task> execute) : base(() => execute(null))
        {
        }
    }
}
