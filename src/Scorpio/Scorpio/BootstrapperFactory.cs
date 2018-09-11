using Scorpio.Modularity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio
{
    /// <summary>
    /// 
    /// </summary>
    public static class BootstrapperFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TStartupModule"></typeparam>
        /// <returns></returns>
        public static IBootstrapper Create<TStartupModule>() where TStartupModule : IScorpioModule
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TStartupModule"></typeparam>
        /// <returns></returns>
        public static IBootstrapper Create<TStartupModule>(Action<BootstrapperCreationOptions> action) where TStartupModule : IScorpioModule
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IBootstrapper Create(Type startupType)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IBootstrapper Create(Type startupType, Action<BootstrapperCreationOptions> action)
        {
            throw new NotImplementedException();
        }
    }
}
