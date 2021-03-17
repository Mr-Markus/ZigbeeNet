using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace ZigBeeNet.Util
{
    public class LogManager
    {
        private static readonly LogManager _nullLogManger = new LogManager(new NullLoggerFactory());
        private static LogManager _mgr = _nullLogManger;
        private readonly ILoggerFactory _loggerfactory;

	    private LogManager(ILoggerFactory loggerFactory)
        {
            _loggerfactory = loggerFactory;
        }

        public static ILogger GetLog(string name) => _mgr._loggerfactory.CreateLogger(name);

        public static ILogger GetLog(Type type) =>_mgr._loggerfactory.CreateLogger(type);

        public static ILogger GetLog<T>() => _mgr._loggerfactory.CreateLogger<T>();

        static public void SetFactory(ILoggerFactory factory)
        {
            if (_mgr != _nullLogManger)
                throw new InvalidOperationException("ILoggerFactory can only be set one");
            _mgr=new LogManager(factory);
        }
    }
}
