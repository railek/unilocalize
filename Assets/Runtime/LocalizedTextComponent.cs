using UnityEngine;

namespace Railek.Unilocalize
{
    public abstract class LocalizedTextComponent<T> : MonoBehaviour, ILocalize where T : Component
    {
        [SerializeField] private T text;

        [SerializeField]
        private string key;
        public string Key
        {
            get => key;
            set
            {
                key = value;
                OnLocalize();
            }
        }

        public void Reset()
        {
            text = GetComponent<T>();
        }

        public void OnEnable()
        {
            Localization.Instance.AddOnLocalizeEvent(this);
        }

        protected abstract void SetText(T component, string value);

        public void OnLocalize()
        {
            SetText(text, LocalizationImporter.Get(key));
        }
    }
}
