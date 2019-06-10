﻿using Scorpio.Domain.Repositories;
using Scorpio.Domain.Services;
using Scorpio.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Domain
{
    class ConventionaInterceptorRegistrar : IConventionaInterceptorRegistrar
    {
        public void Register(IConventionaInterceptorContext context)
        {
            context.Add<IRepository,Uow.UnitOfWorkInterceptor>();
            context.Add<IDomainService, Uow.UnitOfWorkInterceptor>();
        }
    }
}
