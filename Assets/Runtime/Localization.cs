using System;
using UnityEngine;
using System.Collections.Generic;
using Railek.Unibase.Helpers;
using Railek.Unibase;

namespace Railek.Unilocalize
{
    [Serializable]
    public class Localization : SingletonScriptableObject<Localization>
    {
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

        [SerializeField] private VoidEvent voidEvent = default(VoidEvent);

        public void InvokeOnLocalize()
        {
            if (voidEvent != null)
            {
                voidEvent.Raise();
            }
        }

        public void SelectLanguage(int selected)
        {
            SelectedLanguage = LocalizationImporter.Languages[selected];
        }

        public void AddOnLocalizeEvent(ILocalize localize)
        {
            localize.OnLocalize();
        }
    }
}
