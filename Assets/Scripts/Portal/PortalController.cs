using System;
using UnityEngine;

public interface IPortalController
{
	void GetPortalData(PortalData portalData);
	void UpdateMirrorCameras();
}

public class PortalController : IPortalController
{
	private const int RENDER_TEXTURE_DEPTH = 24;

	private Camera _playerCamera;

	private Transform _portal;
	private Camera _cameraRealWorld;
	private Camera _cameraAR;

	private Material _cameraMatRealWorld;
	private Material _cameraMatAR;

	public PortalController()
    {
		_playerCamera = ServiceLocator.User.Camera;
		_cameraMatRealWorld = ServiceLocator.ResourceService.GetRealWorldCameraMaterial();
		_cameraMatAR = ServiceLocator.ResourceService.GetARCameraMaterial();
	}

	public void GetPortalData(PortalData portalData)
    {
		SetCameras(portalData.CameraRealWorld, portalData.CameraAR);
		SetPortal(portalData.Portal);
		SetUpTextures();
	}

	public void UpdateMirrorCameras()
	{
		if (_cameraRealWorld == null || _cameraAR == null || _playerCamera == null)
		{
			Debug.LogWarning($"[PortalController] Camera(s) missing: " +
				 $"_playerCamera={_playerCamera != null}, " +
				 $"_cameraRealWorld={_cameraRealWorld != null}, " +
				 $"_cameraAR={_cameraAR != null}");
			return;
		}

		Vector3 relativePos = _playerCamera.transform.position - _portal.position;
		Quaternion relativeRot = Quaternion.Inverse(_portal.rotation) * _playerCamera.transform.rotation;

		_cameraAR.transform.position = _portal.position + _portal.rotation * relativePos;
		_cameraAR.transform.rotation = _portal.rotation * relativeRot;

		_cameraRealWorld.transform.position = _portal.position + _portal.rotation * relativePos;
		_cameraRealWorld.transform.rotation = _portal.rotation * relativeRot;
	}

	private void SetCameras(Camera cameraRealWorld, Camera cameraAR)
    {
		_cameraRealWorld = cameraRealWorld;
		_cameraAR = cameraAR;
	}

	private void SetPortal(Transform portal)
    {
		_portal = portal;
	}

	private void SetUpTextures()
	{
		_cameraRealWorld.targetTexture.Release();
		_cameraRealWorld.targetTexture = new RenderTexture(Screen.width, Screen.height, RENDER_TEXTURE_DEPTH);
		_cameraMatRealWorld.mainTexture = _cameraRealWorld.targetTexture;

		_cameraAR.targetTexture.Release();
		_cameraAR.targetTexture = new RenderTexture(Screen.width, Screen.height, RENDER_TEXTURE_DEPTH);
		_cameraMatAR.mainTexture = _cameraAR.targetTexture;
	}
}

public struct PortalData
{
	public Transform Portal { get; private set; }
	public Camera CameraRealWorld { get; private set; }
	public Camera CameraAR { get; private set; }

	public PortalData(Transform portal, Camera cameraRealWorld, Camera cameraAR)
	{
		Portal = portal;
		CameraRealWorld = cameraRealWorld;
		CameraAR = cameraAR;
	}
}