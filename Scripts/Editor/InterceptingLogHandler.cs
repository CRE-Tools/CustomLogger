using System.Linq;
using UnityEngine;

namespace PUCPR.CustomLogger.Editor
{
    public class InterceptingLogHandler : ILogHandler
    {
        private readonly ILogHandler _inner;

        public InterceptingLogHandler(ILogHandler inner) => _inner = inner;

        #region Interface Implementation
        public void LogFormat(LogType logType, Object context, string format, params object[] args)
        {
            CustomLoggerKey key = ArgsAsKey(args);

            if (key == CustomLoggerKey.NeverLog)
                return;

            string msg = MessageFormatter(key, context, format);

            if (string.IsNullOrEmpty(msg))
                return;

            LogType logArg = ArgsAsLogType(args);

            if (logArg != LogType.Assert)
                logType = logArg;

            _inner.LogFormat(logType, context, msg, args);
        }

        public void LogException(System.Exception e, Object context) => _inner.LogException(e, context);
        #endregion

        private LogType ArgsAsLogType(object[] args)
        {
            if (args != null && args.Length > 0)
            {
                foreach (var arg in args)
                    if (arg is LogType type)
                        return type;
            }
            return LogType.Assert;
        }

        private CustomLoggerKey ArgsAsKey(object[] args)
        {
            if (args != null && args.Length > 0)
            {
                foreach (var arg in args)
                    if (arg is CustomLoggerKey key)
                        return key;
            }
            return CustomLoggerKey.AlwaysLog;
        }

        private string MessageFormatter(CustomLoggerKey key, Object context, string format)
        {
            string color = "FFFFFF";
            string s_context = "";

            //caso de problema em build, colocar um simbolo para executar esse "IF" apenas em editor -- INIT
            if (key != CustomLoggerKey.AlwaysLog)
            {
                var LogSettings = CustomLoggerSettings.GetLoggerTypeSettings(key);

                if (!LogSettings.showLog)
                    return string.Empty;

                color = LogSettings.color;
                s_context = $"[{key}]";
            }



            /*string */s_context += context == null ? $"" : $" [{context.GetType()}]";

            return $"{s_context}:<color=#{color}> {format}</color>";
        }
    }
}
