using UnityEngine;

[CreateAssetMenu(fileName = "RenderLayersConfig", menuName = "SO/Render Layers Config")]
public class RenderLayersConfig : ScriptableObject
{
    [Header("Render Modes")]
    [field:SerializeField] public LayerMask ARRenderMask { get; private set; }
    [field: SerializeField] public LayerMask RealWorldRenderMask { get; private set; }
}
