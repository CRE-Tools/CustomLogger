using UnityEditor;
using UnityEngine;

namespace PUCPR.CustomLogger.Editor
{
    [InitializeOnLoad]
    public static class InterceptorInitializer
    {
        [InitializeOnLoadMethod]
        private static void Execute()
        {
            var defaultHandler = Debug.unityLogger.logHandler;
            Debug.unityLogger.logHandler = new InterceptingLogHandler(defaultHandler);
        }
    }
}
