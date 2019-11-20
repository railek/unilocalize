using Railek.Unibase.Utilities;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using Railek.Unibase.Editor;

namespace Railek.Unilocalize
{
    [CustomEditor(typeof(Localization))]
    public class LocalizationInspector : EditorBase
    {
        [MenuItem("Railek/Localization Settings")]
        public static void MenuItem()
        {
            var asset = Resources.Load<Localization>("LocalizationSettings");
            if (asset == null)
            {
                const string resourcesPath = "Assets/Resources/";
                Asset.Create<Localization>(resourcesPath);
            }

            Selection.activeObject = asset;
        }

        private SerializedProperty _inputProperty;
        private SerializedProperty _languageProperty;
        private SerializedProperty _eventProperty;

        protected override void LoadSerializedProperty()
        {
            _inputProperty = GetProperty("inputFiles");
            _languageProperty = GetProperty("selectedLanguage");
            _eventProperty = GetProperty("voidEvent");
        }

        private List<string> _languages;

        public override void OnInspectorGUI()
        {
            if (refresh)
            {
                LocalizationImporter.Refresh();
                refresh = false;
            }

            EditorGUI.BeginChangeCheck();
            serializedObject.Update();

            EditorGUILayout.PropertyField(_inputProperty);
            EditorGUILayout.PropertyField(_eventProperty);

            _languages = LocalizationImporter.Languages;

            var changed = false;

            if (_languages != null && _languages.Count != 0)
            {
                var languageIndex = Mathf.Max(_languages.IndexOf(_languageProperty.stringValue), 0);

                EditorGUI.BeginChangeCheck();
                {
                    languageIndex = EditorGUILayout.Popup("Language", languageIndex, _languages.ToArray());
                }
                if (EditorGUI.EndChangeCheck())
                {
                    _languageProperty.stringValue = _languages[languageIndex];
                }
            }
            else
            {
                EditorGUILayout.HelpBox("CSV file Missing", MessageType.Error);
            }

            if (changed || EditorGUI.EndChangeCheck())
            {
                refresh = true;
            }

            serializedObject.ApplyModifiedProperties();

            var localizedText = FindObjectsOfType<LocalizedText>();
            var localizedTextMeshPro = FindObjectsOfType<LocalizedTextMeshPro>();

            foreach (var local in localizedText)
            {
                local.OnLocalize();
            }

            foreach (var local in localizedTextMeshPro)
            {
                local.OnLocalize();
            }
        }

        private static bool refresh = false;
    }
}
