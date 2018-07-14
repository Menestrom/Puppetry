﻿using System.Threading;

namespace Puppetry.Puppeteer.PuppetDriver
{
    internal static class Driver
    {
        private static ThreadLocal<DriverHandler> _handler = new ThreadLocal<DriverHandler>();

        internal static DriverHandler Instance
        {
            get
            {
                if (_handler.Value == null)
                {
                    _handler.Value = new DriverHandler();
                }

                return _handler.Value;
            }
        }

        internal static void Clear()
        {
            _handler.Value = null;
        }
    }
}
