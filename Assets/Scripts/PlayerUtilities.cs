using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using UnityEngine.Events;

public class PlayerUtilities: MonoBehaviour
{
    public XRDirectInteractor leftDirectInteractor;
    public XRRayInteractor leftRayInteractor;
    public XRDirectInteractor rightDirectInteractor;
    public XRRayInteractor rightRayInteractor;
    public LineRenderer leftLineRenderer;
    public LineRenderer rightLineRenderer;

    public Transform playerCamera;
    public XRRig rig;

    static InputDevice _device;

    bool rotSet = false;

    private void Awake()
    {
        SetDevice(InputDevices.GetDeviceAtXRNode(XRNode.Head));
        InputDevices.deviceConnected += SetDevice;
    }

    private void LateUpdate()
    {
        if (!rotSet)
        {
            rig.MatchRigUpCameraForward(Vector3.up, Vector3.forward);
            rotSet = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (leftDirectInteractor.selectTarget != null) leftRayInteractor.enabled = false; else leftRayInteractor.enabled = true;
        if (leftRayInteractor.selectTarget != null) leftDirectInteractor.enabled = false; else leftDirectInteractor.enabled = true;
        if (rightDirectInteractor.selectTarget != null) rightRayInteractor.enabled = false; else rightRayInteractor.enabled = true;
        if (rightRayInteractor.selectTarget != null) rightDirectInteractor.enabled = false; else rightDirectInteractor.enabled = true;

        if (leftRayInteractor.isSelectActive) leftLineRenderer.enabled = true; else leftLineRenderer.enabled = false;
        if (rightRayInteractor.isSelectActive) rightLineRenderer.enabled = true; else rightLineRenderer.enabled = false;
    }

    void SetDevice(InputDevice device)
    {
        if (device.characteristics == (device.characteristics | InputDeviceCharacteristics.HeadMounted))
        {
            _device = device;
        }
        if (_device == null)
        {
            _device = InputDevices.GetDeviceAtXRNode(XRNode.Head);

        }
    }

    public static float? GetRelativeHeight(Vector3 posIn, float yOffset)
    {
        if (_device != null && _device.isValid)
        {
            //this "initialization" check is very scuffed, but i think it works. literally just checks if hmd is reporting anything thats not 0,0,0
            _device.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 pos);
            if (!pos.Equals(Vector3.zero))
            {
                return posIn.y + yOffset;
            }
        }
        else if (_device == null)
        {
            _device = InputDevices.GetDeviceAtXRNode(XRNode.Head);
            
        }
        return null;
    }
}
