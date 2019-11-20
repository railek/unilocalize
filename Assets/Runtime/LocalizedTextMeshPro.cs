using UnityEngine;
using TMPro;

namespace Railek.Unilocalize
{
    [AddComponentMenu("UI/Localized TextMesh Pro", 11)]
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LocalizedTextMeshPro : LocalizedTextComponent<TextMeshProUGUI>
    {
        protected override void SetText(TextMeshProUGUI text, string value)
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
