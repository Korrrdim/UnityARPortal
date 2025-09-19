using UnityEngine;

public interface IUserController
{
    void Initialize();
    void Dispose();
}

public class UserController : IUserController
{
    private User _user;
    private RenderLayersConfig _renderLayersConfig;

    private bool _isInRealWorldRender = true;

    public UserController()
    {
        _user = ServiceLocator.User;
        _renderLayersConfig = ServiceLocator.RenderLayersConfig;
    }

    public void Initialize()
    {
        _user.OnEnterPortal += ToggleCameraMode;
        InitRealWorldRenderCameraMode();
    }

    public void Dispose()
    {

        _user.OnEnterPortal -= ToggleCameraMode;
    }

    private void ToggleCameraMode()
    {
        if (_isInRealWorldRender)
        {
            _user.Camera.cullingMask = _renderLayersConfig.ARRenderMask;
        }
        else
        {
            _user.Camera.cullingMask = _renderLayersConfig.RealWorldRenderMask;
        }

        Debug.Log("Camera render mode toggled.");
        _isInRealWorldRender = !_isInRealWorldRender;
    }

    private void InitRealWorldRenderCameraMode()
    {
        _isInRealWorldRender = true;
        _user.Camera.cullingMask = _renderLayersConfig.RealWorldRenderMask;
    }
}
