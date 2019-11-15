using System;
using UnityEngine;
using System.Collections.Generic;
using Railek.Unibase.Utilities;
using Railek.Unibase.Helpers;
using Railek.Unibase;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Railek.Unilocalize
{
    [Serializable]
    public class Localization : SingletonScriptableObject<Localization>
    {

        #if UNITY_EDITOR
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
        #endif

        [SerializeField] private TextAsset inputFiles;

        public TextAsset InputFiles
        {
            get => inputFiles;
            set => inputFiles = value;
        }

        private List<string> _languages;

        public int SelectedLanguageIndex
        {
            get
            {
                _languages = LocalizationImporter.Languages;
                return _languages.IndexOf(SelectedLanguage);
            }
        }

        [SerializeField] private string selectedLanguage;

        public string SelectedLanguage
        {
            get => selectedLanguage;
            set
            {
                selectedLanguage = value;
                InvokeOnLocalize();
            }
        }

        [SerializeField] private VoidEvent voidEvent;

        public void InvokeOnLocalize()
        {
            voidEvent?.Raise();
        }

        public void SelectLanguage(int selected)
        {
            SelectedLanguage = LocalizationImporter.Languages[selected];
        }

        public void AddOnLocalizeEvent(ILocalize localize)
        {
            voidEvent.RemoveListener(localize.OnLocalize);
            voidEvent.AddListener(localize.OnLocalize);
            localize.OnLocalize();
        }

        public void RemoveOnLocalizeEvent(ILocalize localize)
        {
            voidEvent.RemoveListener(localize.OnLocalize);
        }
    }
}
