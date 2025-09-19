using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Transform _portal;
    [SerializeField] private Camera _cameraRealWorld;
    [SerializeField] private Camera _cameraAR;

    private void Start()
    {
        PortalData portalData = new PortalData(_portal, _cameraRealWorld, _cameraAR);
        ServiceLocator.PortalController.GetPortalData(portalData);
    }

    private void LateUpdate()
    {
        ServiceLocator.PortalController.UpdateMirrorCameras();
    }
}
