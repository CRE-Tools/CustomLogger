using UnityEngine;

namespace PUCPR.CustomLogger
{

    public class CustomLogger : MonoBehaviour
    {
        public static void DebugLog(LogType logType, UnityEngine.Object objContext, string message, CustomLoggerKey key)
        {
            var LogSettings = CustomLoggerSettings.GetLoggerTypeSettings(key);

            if (!LogSettings.showLog) return;


            string msg = $"<color=#{LogSettings.color}>[{objContext.GetType()}]: {message}</color>";
            Debug.LogFormat(logType, LogOption.None, objContext, msg);
        }
    }
}
