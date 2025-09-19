using UnityEngine;

public interface IResourceService
{
    GameObject GetPortal();
    Material GetARCameraMaterial();
    Material GetRealWorldCameraMaterial();
}

public class ResourceService : IResourceService
{
    private readonly ResourcesConfig _config;

    public ResourceService(ResourcesConfig config)
    {
        _config = config;
    }

    public GameObject GetPortal()
    {
        return Resources.Load<GameObject>(_config.PortalPrefabPath);
    }

    public Material GetARCameraMaterial()
    {
        return Resources.Load<Material>(_config.ARCameraMaterialPath);
    }

    public Material GetRealWorldCameraMaterial()
    {
        return Resources.Load<Material>(_config.RealWorldCameraMaterialPath);
    }
}
