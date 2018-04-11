﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

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
