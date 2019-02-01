using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Settings
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISettingEncryptionService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="settingDefinition"></param>
        /// <param name="plainValue"></param>
        /// <returns></returns>
        [CanBeNull]
        string Encrypt([NotNull]SettingDefinition settingDefinition, [CanBeNull] string plainValue);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settingDefinition"></param>
        /// <param name="encryptedValue"></param>
        /// <returns></returns>
        [CanBeNull]
        string Decrypt([NotNull]SettingDefinition settingDefinition, [CanBeNull] string encryptedValue);
    }
}
