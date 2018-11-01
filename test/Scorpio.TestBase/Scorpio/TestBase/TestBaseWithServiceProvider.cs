﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Scorpio.TestBase
{
   public abstract class TestBaseWithServiceProvider
    {
        protected abstract IServiceProvider ServiceProvider { get; }

        protected virtual T GetService<T>()
        {
            return ServiceProvider.GetService<T>();
        }

        protected virtual T GetRequiredService<T>()
        {
            return ServiceProvider.GetRequiredService<T>();
        }
    }
}
