// Copyright (c) 2013 Krkadoni.com - Released under The MIT License.
// Full license text can be found at http://opensource.org/licenses/MIT

using System;
using Krkadoni.EnigmaSettings.Interfaces;

namespace Krkadoni.SESE
{
    public class Log4NetLogger : ILog
    {
        private log4net.ILog _log;

        public Log4NetLogger(log4net.ILog log)
        {
            _log = log ?? throw new ArgumentNullException(nameof(log));
        }

        public void Debug(string message)
        {
            _log.Debug(message);
        }

        public void Warn(string message)
        {
            _log.Warn(message);
        }

        public void Warn(string message, Exception ex)
        {
            _log.Warn(message, ex);
        }

        public void Info(string message)
        {
            _log.Info(message);
        }

        public void Error(string message)
        {
            _log.Error(message);
        }

        public void Error(string message, Exception ex)
        {
            _log.Error(message, ex);
        }
    }
}
