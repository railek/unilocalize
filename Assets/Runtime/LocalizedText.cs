using UnityEngine;
using UnityEngine.UI;

namespace Railek.Unilocalize
{
    [AddComponentMenu("UI/Localized Text", 11)]
    [RequireComponent(typeof(Text))]
    public class LocalizedText : LocalizedTextComponent<Text>
    {
        protected override void SetText(Text text, string value)
        {
            if (text == null)
            {
                Debug.LogWarning("Missing Text Component on " + gameObject, gameObject);
                return;
            }
            text.text = value;
        }
    }
}
