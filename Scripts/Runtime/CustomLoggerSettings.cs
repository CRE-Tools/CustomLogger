using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace PUCPR.CustomLogger
{
    public class CustomLoggerSettings : ScriptableObject
    {
        private const string k_LoggerSettingsPath = "Assets/LoggerSettings/";
        private const string k_LoggerSettingsAsset = "Loggers.asset";
        private const string k_enumPath = "Packages/com.pucpr.customlogger/Scripts/Runtime/CustomLoggerKey.cs";
        private const string k_enumDeclaration = "CustomLoggerKey";

        [SerializeField] private CustomLoggerTypes[] _loggers;
        private List<string> _keyLogs = new List<string>();

        [HideInInspector] public bool isValidKeys = false;
        [HideInInspector] public string helpMsg;


        private void OnValidate()
        {
            _keyLogs = ConstructKeyNames(_loggers);
            var vK = ValidateKeys();
            isValidKeys = vK.isValid;
            helpMsg = vK.msg;
        }

        private (bool isValid, string msg) ValidateKeys()
        {
            foreach (var key in _keyLogs)
            {
                if (string.IsNullOrEmpty(key))
                    return (false, "String Empty");
            }

            return (true, "");
        }

        private static List<string> ConstructKeyNames(CustomLoggerTypes[] newLogger)
        {
            List<string> keysNames = new List<string>() { "None" };

            foreach (var logger in newLogger)
                keysNames.Add(logger.keyName);

            return keysNames;
        }

        #region Settings
        internal static CustomLoggerSettings GetOrCreateSettings()
        {
            string finalPath = k_LoggerSettingsPath + k_LoggerSettingsAsset;
            var settings = AssetDatabase.LoadAssetAtPath<CustomLoggerSettings>(finalPath);
            if (settings == null)
            {
                settings = ScriptableObject.CreateInstance<CustomLoggerSettings>();
                settings._loggers = new CustomLoggerTypes[] { new CustomLoggerTypes() };

                if (!Directory.Exists(k_LoggerSettingsPath))
                    Directory.CreateDirectory(k_LoggerSettingsPath);

                AssetDatabase.CreateAsset(settings, finalPath);
                AssetDatabase.SaveAssets();

                var keyNames = ConstructKeyNames(settings._loggers);

                CodeGenerator.CreateEnumFromList(k_enumDeclaration, k_enumPath, keyNames, "PUCPR.CustomLogger");
            }
            return settings;
        }

        internal static SerializedObject GetSerializedSettings() => new SerializedObject(GetOrCreateSettings());

        internal static (bool showLog, string color) GetLoggerTypeSettings(CustomLoggerKey logKey)
        {
            if (logKey == CustomLoggerKey.None || logKey < 0)
                return (false, "");

            var settings = GetOrCreateSettings();

            if ((int)logKey > settings._loggers.Length)
                return (false, "");

            CustomLoggerTypes logger = settings._loggers[(int)logKey - 1];
            string color = ColorUtility.ToHtmlStringRGB(logger.color);

            return (logger.showLog, color);
        }
        #endregion

        public bool NeedToApplyChanges()
        {
            var ek = Enum.GetNames(typeof(CustomLoggerKey)).ToList<string>();

            if (ek.Count != _keyLogs.Count)
                return true;

            if (!ek.SequenceEqual(_keyLogs))
                return true;

            return false;
        }

        public void ApplyNewInspectorValues()
        {
            var keyLogs = ConstructKeyNames(_loggers);

            CodeGenerator.CreateEnumFromList(k_enumDeclaration, k_enumPath, keyLogs, "PUCPR.CustomLogger");
        }
    }


    static class CustomLoggerSettingsIMGUIRegister
    {
        [SettingsProvider]
        public static SettingsProvider CreateLoggerCustomSettingsProvider()
        {
            var provider = new SettingsProvider("Project/LoggerCustomIMGUISettings", SettingsScope.Project)
            {
                label = "Logger",
                guiHandler = (searchContext) =>
                {
                    var settings = CustomLoggerSettings.GetSerializedSettings();
                    EditorGUILayout.PropertyField(settings.FindProperty("_loggers"), new GUIContent("Loggers"));
                },

                keywords = new HashSet<string>(new[] { "Logger", "DebugLog", "Log", "Console" })
            };
            return provider;
        }
    }
}
