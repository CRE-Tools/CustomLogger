using UnityEngine;
using UnityEditor;

namespace PUCPR.CustomLogger.Editor
{
    [CustomEditor(typeof(CustomLoggerSettings))]
    public class EDITOR_Logger : UnityEditor.Editor
    {
        CustomLoggerSettings script;

        public override void OnInspectorGUI()
        {
            script = (CustomLoggerSettings)target;

            base.OnInspectorGUI();

            if (script.isValidKeys)
            {
                if (script.NeedToApplyChanges())
                    if (GUILayout.Button("Apply enum logger"))
                        script.ApplyNewInspectorValues();
            }
            else
                EditorGUILayout.HelpBox("Invalid Key Name: " + script.helpMsg, MessageType.Warning);
        }

        void OnDisable()
        {
            if (script.isValidKeys)
                if (script.NeedToApplyChanges())
                    script.ApplyNewInspectorValues();
        }
    }
}
