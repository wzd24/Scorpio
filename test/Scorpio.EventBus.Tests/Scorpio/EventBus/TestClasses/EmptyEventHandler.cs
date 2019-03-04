﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scorpio.EventBus.TestClasses
{
    internal class EmptyEventHandler : IEventHandler<string>
    {
        public Task HandleEventAsync(string eventData)
        {
            return Task.CompletedTask;
        }
    }
}
