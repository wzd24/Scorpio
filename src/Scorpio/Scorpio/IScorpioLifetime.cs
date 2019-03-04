using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio
{
    /// <summary>
    /// 
    /// </summary>
    public interface IScorpioLifetime
    {
        /// <summary>
        /// 
        /// </summary>
        void OnStopping();

        /// <summary>
        /// 
        /// </summary>
        void OnStopped();

        /// <summary>
        /// 
        /// </summary>
        void OnStarted();
    }
}
