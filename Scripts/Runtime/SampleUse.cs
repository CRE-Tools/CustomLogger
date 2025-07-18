using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PUCPR.CustomLogger
{

    //this file use 3 classes for example how this SDK (Custom Logger) will work
    public class SampleUse : MonoBehaviour
    {
        //This class uses a centralized logging key that controls all debug logs
        //The logs will be printed according to the settings configured for this key in CustomLoggerSettings
        //If the key is enabled in settings, logs will be printed with the configured color and other settings

        [SerializeField] private CustomLoggerKey keyForAllDebugsInThisClass;

        private void Start ()
        {
            //Logs a standard message using the configured key
            //The message will be printed according to the settings in CustomLoggerSettings
            //If the key is disabled, this log will not appear in the console
            CustomLogger.DebugLog(LogType.Log, this, "this class started", keyForAllDebugsInThisClass);
        }

        public void OnThisSampleMethodIsCalled()
        {

            //Logs a standard message using the configured key
            //The message will be printed according to the settings in CustomLoggerSettings
            //If the key is disabled, this log will not appear in the console
            CustomLogger.DebugLog(LogType.Log, this, "this method has ben called", keyForAllDebugsInThisClass);
        }
    }

    public class OtherSampleUse : MonoBehaviour
    {
        private void Start()
        {
            //in this case, the key is passed directly. the key in case is "NewType"
            //Logs a standard message using the configured key
            //The message will be printed according to the settings in CustomLoggerSettings
            //If the key is disabled, this log will not appear in the console
            CustomLogger.DebugLog(LogType.Log, this, "this class started", CustomLoggerKey.AlwaysLog);
        }
    }

    public class AnotherSampleUse : MonoBehaviour 
    {
        private void Start()
        {
            //Attempts to log a message using the None key
            //None is a special key that represents no logging
            //This log will not appear in the console as it's meant to be a fallback value
            CustomLogger.DebugLog(LogType.Log, this, "this class started", CustomLoggerKey.NeverLog);
        }
    }
    
}
