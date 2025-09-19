using System;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class User : MonoBehaviour
{
    public Camera Camera;

    public event Action OnEnterPortal;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<PortalTrigger>(out PortalTrigger portal))
        {
            OnEnterPortal?.Invoke();
        }
    }
}
