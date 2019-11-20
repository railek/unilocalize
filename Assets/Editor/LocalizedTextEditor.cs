using UnityEditor;

namespace Railek.Unilocalize
{
    [CustomEditor(typeof(LocalizedText), true)]
    [CanEditMultipleObjects]
    public class LocalizedTextEditor : LocalizedEditor<LocalizedText>
    {
    }
}
