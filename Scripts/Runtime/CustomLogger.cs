using UnityEngine;
using UnityEngine.UIElements;

namespace PUCPR.CustomLogger
{

    public class CustomLogger : MonoBehaviour
    {
        public static void DebugLog(LogType logType, Object objContext, string message, CustomLoggerKey key)
        {
            if (key == CustomLoggerKey.NeverLog)
                return;

            string color = "FFFFFF";

            if (key != CustomLoggerKey.AlwaysLog)
            {
                var LogSettings = CustomLoggerSettings.GetLoggerTypeSettings(key);

                if (!LogSettings.showLog)
                    return;

                color = LogSettings.color;
            }

            string msg = $"<color=#{color}>[{objContext.GetType()}]: {message}</color>";
            Debug.LogFormat(logType, LogOption.None, objContext, msg);
        }
    }
}
