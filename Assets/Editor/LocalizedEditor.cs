using UnityEditor;
using UnityEngine;
using Railek.Unibase.Editor;

namespace Railek.Unilocalize
{
    public abstract class LocalizedEditor<T> : EditorBase where T : class, ILocalize
    {
        private SerializedProperty keyProperty;

        protected override void LoadSerializedProperty()
        {
            keyProperty = GetProperty("key");
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();

            EditorGUILayout.PropertyField(keyProperty);

            var key = keyProperty.stringValue;

            if (!string.IsNullOrEmpty(key))
            {
                DrawAutoComplete(keyProperty);
            }

            serializedObject.ApplyModifiedProperties();

            if (!EditorGUI.EndChangeCheck()) return;
            if (target is T text)
            {
                text.OnLocalize();
            }
        }

        private static void DrawAutoComplete(SerializedProperty property)
        {
            var localizedStrings = LocalizationImporter.GetLanguagesStartsWith(property.stringValue);

            if (localizedStrings.Count == 0)
            {
                localizedStrings = LocalizationImporter.GetLanguagesContains(property.stringValue);
            }

            foreach (var local in localizedStrings)
            {
                if (!GUILayout.Button(local.Value[0]))
                {
                    continue;
                }

                property.stringValue = local.Key;
                GUIUtility.hotControl = 0;
                GUIUtility.keyboardControl = 0;
            }
        }
    }
}
