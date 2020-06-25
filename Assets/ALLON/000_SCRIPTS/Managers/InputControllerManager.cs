using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

/// <summary>
/// This manager is responsible for registering the input controllers
/// </summary>
public class InputControllerManager : MonoBehaviour
{
    [SerializeField] private BaseInputController _leftHandController;
    [SerializeField] private BaseInputController _rightHandController;

    #region EVENTS

    private void OnEnable()
    {
        InputDevices.deviceConnected += OnDeviceConnected;
        InputDevices.deviceDisconnected += OnDeviceDisconnected;
    }

    private void OnDisable()
    {
        InputDevices.deviceConnected -= OnDeviceConnected;
        InputDevices.deviceDisconnected -= OnDeviceDisconnected;
    }

    private void OnDeviceConnected(InputDevice device)
    {
        if (!device.isValid) return;

        // we check for one flag at the time
        if ((device.characteristics & InputDeviceCharacteristics.HeldInHand) == InputDeviceCharacteristics.HeldInHand)
        {
            if ((device.characteristics & InputDeviceCharacteristics.Left) == InputDeviceCharacteristics.Left)
            {
                _leftHandController.InitInputController(device);
            }
            else if ((device.characteristics & InputDeviceCharacteristics.Right) == InputDeviceCharacteristics.Right)
            {
                _rightHandController.InitInputController(device);
            }
        }
    }

    private void OnDeviceDisconnected(InputDevice device)
    {
        if (!device.isValid) return;

        // we check for one flag at the time
        if ((device.characteristics & InputDeviceCharacteristics.HeldInHand) == InputDeviceCharacteristics.HeldInHand)
        {
            if ((device.characteristics & InputDeviceCharacteristics.Left) == InputDeviceCharacteristics.Left)
            {
                _leftHandController.DisconnectInputController(device);
            }
            else if ((device.characteristics & InputDeviceCharacteristics.Right) == InputDeviceCharacteristics.Right)
            {
                _rightHandController.DisconnectInputController(device);
            }
        }
    }
    #endregion
}