using UnityEditor;

namespace Railek.Unilocalize
{
    [CustomEditor(typeof(LocalizedTextMeshPro), true)]
    [CanEditMultipleObjects]
    public class LocalizedTextMeshProEditor : LocalizedEditor<LocalizedTextMeshPro>
    {
    }
}
