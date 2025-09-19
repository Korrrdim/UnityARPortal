using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public interface IPortalPlacementController
{
    void Initialize();
    void Dispose();
}

public class PortalPlacementController : IPortalPlacementController
{
    private Camera _userCamera;
    private GameObject _portalPrefab;
    private ARRaycastManager _raycastManager;
    private ARPlaneManager _planeManager;
    private readonly List<ARRaycastHit> _hits = new();

    private GameObject _spawnedPortal;

    public PortalPlacementController()
    {
        _userCamera = ServiceLocator.User.Camera;
        _raycastManager = ServiceLocator.RaycastManager;
        _planeManager = ServiceLocator.PlaneManager;
        _portalPrefab = ServiceLocator.ResourceService.GetPortal();
    }

    public void Initialize()
    {
#if UNITY_EDITOR
        var pose = new Pose(Vector3.zero, Quaternion.identity);
        SpawnPortal(pose);
        FacePortalToCamera();
#endif
        TouchSimulation.Enable();
        EnhancedTouchSupport.Enable();
        Touch.onFingerDown += HandleTouch;
    }

    public void Dispose()
    {
        Touch.onFingerDown -= HandleTouch;
        EnhancedTouchSupport.Disable();
        TouchSimulation.Disable();
    }

    private void HandleTouch(Finger finger)
    {
        if (finger.index != 0)
            return;

        if (!_raycastManager.Raycast(finger.screenPosition, _hits, TrackableType.PlaneWithinPolygon))
            return;

        var hitPose = _hits[0].pose;
        var plane = _planeManager.GetPlane(_hits[0].trackableId);

        if (_spawnedPortal == null)
            SpawnPortal(hitPose);
        else
            MovePortal(hitPose);

        if (plane.alignment == PlaneAlignment.HorizontalUp)
            FacePortalToCamera();
    }

    private void SpawnPortal(Pose pose)
    {
        _spawnedPortal = GameObject.Instantiate(_portalPrefab, pose.position, pose.rotation);
    }

    private void MovePortal(Pose pose)
    {
        _spawnedPortal.transform.SetPositionAndRotation(pose.position, pose.rotation);
    }

    private void FacePortalToCamera()
    {
        Vector3 toCamera = _userCamera.transform.position - _spawnedPortal.transform.position;
        toCamera.y = _userCamera.transform.position.y;

        if (toCamera != Vector3.zero)
            _spawnedPortal.transform.rotation = Quaternion.LookRotation(toCamera);
    }
}
