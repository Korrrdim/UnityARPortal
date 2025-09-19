using UnityEngine;

[CreateAssetMenu(fileName = "ResourcesConfig", menuName = "SO/Resources Config")]
public class ResourcesConfig : ScriptableObject
{
    [field:SerializeField] public string PortalPrefabPath { get; private set; }
    [field: SerializeField] public string RealWorldCameraMaterialPath { get; private set; }
    [field: SerializeField] public string ARCameraMaterialPath { get; private set; }
}
