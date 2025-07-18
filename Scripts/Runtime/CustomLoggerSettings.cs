using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        [SerializeField] private CustomLoggerType[] _loggers;
        private List<string> _keyLogs = new List<string>();

        [HideInInspector] public bool isValidKeys = true;
        [HideInInspector] public string helpMsg;
        private static string[] _reservedKeys = { "as", "int", "float", "bool", "string", "object", "unityObject" };
        private static string[] _defaultKeys = { "NeverLog", "AlwaysLog" };


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
                int index = _keyLogs.IndexOf(key) - _defaultKeys.Length;

                if (string.IsNullOrEmpty(key))
                    return (false, $"{index}:\n String Empty");

                if (key.Contains(' '))
                    return (false, $"{index}:\n\"{key}\" Has spacing");

                if (!key.All(char.IsLetter))
                    return (false, $"{index}:\n\"{key}\" Has invalid characters.\n Only letters are allowed.");

                if (_reservedKeys.Contains(key))
                    return (false, $"{index}:\n\"{key}\" Is reserved");

                int count = _keyLogs.Count(x => x.ToLower() == key.ToLower());
                if (count > 1)
                {
                    if (_defaultKeys.Contains(key))
                        return (false, $"{index}:\n\"{key}\" Is reserved");
                    return (false, $"{index}:\n\"{key}\" Is already used");
                }
            }

            return (true, "");
        }

        private static List<string> ConstructKeyNames(CustomLoggerType[] newLogger)
        {
            List<string> keysNames = new List<string>();
            keysNames = _defaultKeys.ToList();

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
                settings._loggers = new CustomLoggerType[] { };

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
            if (logKey < 0)
                return (false, "");

            var settings = GetOrCreateSettings();

            if ((int)logKey > settings._loggers.Length)
                return (false, "");

            CustomLoggerType logger = settings._loggers[(int)logKey - _defaultKeys.Length];
            string color = ColorUtility.ToHtmlStringRGB(logger.color);

            return (logger.showLog, color);
        }
        #endregion

        public bool NeedToApplyChanges()
        {
            var ek = Enum.GetNames(typeof(CustomLoggerKey)).ToList<string>();

            if (_keyLogs.Count == 0)
                _keyLogs = ConstructKeyNames(_loggers);

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
                label = "Custom Logger",
                guiHandler = (searchContext) =>
                {
                    var settings = CustomLoggerSettings.GetSerializedSettings();
                    EditorGUILayout.PropertyField(settings.FindProperty("_loggers"), new GUIContent("Loggers"));

                    ConstructGUI(settings.targetObject as CustomLoggerSettings);

                    settings.ApplyModifiedPropertiesWithoutUndo();
                },

                keywords = new HashSet<string>(new[] { "Logger", "DebugLog", "Log", "Console" })
            };
            return provider;
        }

        private static void ConstructGUI(CustomLoggerSettings settings)
        {
            if (settings == null) return;

            if (settings.isValidKeys)
            {
                if (settings.NeedToApplyChanges())
                    if (GUILayout.Button("Apply Settings"))
                        settings.ApplyNewInspectorValues();
            }
            else
                EditorGUILayout.HelpBox("Invalid KeyName at element " + settings.helpMsg, MessageType.Warning);
        }
    }
}
