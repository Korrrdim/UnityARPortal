using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class AppBootstrap : MonoBehaviour
{
    [SerializeField] private ARRaycastManager _raycastManager;
    [SerializeField] private ARPlaneManager _planeManager;
    [Space]
    [SerializeField] private RenderLayersConfig _renderLayersConfig;
    [SerializeField] private ResourcesConfig _resourcesConfig;
    [SerializeField] private User _user;

    private void Awake()
    {
        ServiceLocator.Initialize(_raycastManager, _planeManager, _resourcesConfig, _user, _renderLayersConfig);
    }
}
