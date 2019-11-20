using UnityEngine;
using UnityEngine.UI;

namespace Railek.Unilocalize
{
    [RequireComponent(typeof(Dropdown))]
    [AddComponentMenu("UI/Language Dropdown", 36)]
    public class LanguageDropdown : MonoBehaviour, ILocalize
    {
        [SerializeField] private Dropdown dropdown;

        public void Reset()
        {
            dropdown = GetComponent<Dropdown>();
        }

        public void Start()
        {
            CreateDropdown();

            Localization.Instance.AddOnLocalizeEvent(this);
        }

        private void CreateDropdown()
        {
            dropdown.options.Clear();

            var languageNames = LocalizationImporter.Languages;

            foreach (var languageName in languageNames)
            {
                dropdown.options.Add(new Dropdown.OptionData(languageName));
            }

            dropdown.value = Localization.Instance.SelectedLanguageIndex;
        }

        public void OnLocalize()
        {
            dropdown.onValueChanged.RemoveListener(Localization.Instance.SelectLanguage);
            dropdown.value = Localization.Instance.SelectedLanguageIndex;
            dropdown.onValueChanged.AddListener(Localization.Instance.SelectLanguage);
        }
    }
}
