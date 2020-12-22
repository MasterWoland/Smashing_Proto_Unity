using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandController : BaseInputController
{
    private InputDevice _device = default;
    private bool _doCheckForInput = false;
    private Animator _animator = null;
    private static readonly int Trigger = Animator.StringToHash("Trigger");
    private static readonly int Grip = Animator.StringToHash("Grip");

    [Header("Locomotion")] [SerializeField]
    private Transform _rigTransform = null;

    [SerializeField] private Transform _camTransform = null;
    [SerializeField] private float _locomotionSpeed = 1f;

    // Parabola Testing
    public static event Action TriggerPressed;
    private bool _isTriggerPressed = false;


    public override void InitInputController(InputDevice device)
    {
        _device = device;
        _doCheckForInput = true;

        if (_animator == null) _animator = GetAnimator();

        if (_animator == null) Debug.LogError("[HandController] " + this.name + " no Animator found");
    }

    public override void DisconnectInputController(InputDevice device)
    {
        _doCheckForInput = false;
    }

    private void Update()
    {
        if (!_doCheckForInput) return;

        CheckTriggerButton(); // for parabola testing

        CheckTriggerValue();
        CheckGrip();
        CheckTouchpad();
    }

    #region HELPER METHODS
    private Animator GetAnimator()
    {
        return gameObject.GetComponentInChildren<Animator>();
    }

    private void CheckTriggerButton()
    {
        _device.TryGetFeatureValue(CommonUsages.triggerButton, out bool IsPressed);

        // make sure we only dispatch one event per trigger press 
        if (!_isTriggerPressed && IsPressed)
            TriggerPressed?.Invoke();

        _isTriggerPressed = IsPressed;
    }

    private void CheckTriggerValue()
    {
        _device.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);
        if (triggerValue > 0.1f)
        {
            _animator.SetFloat(Trigger, triggerValue);
        }
        else
        {
            _animator.SetFloat(Trigger, 0); //MRA: optimize this
        }
    }

    private void CheckGrip()
    {
        if (_device.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            _animator.SetFloat(Grip, gripValue);
        }
        else
        {
            _animator.SetFloat(Grip, 0);
        }
    }

    private void CheckTouchpad()
    {
        _device.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 primary2DAxisValue);
        if (primary2DAxisValue != Vector2.zero)
        {
            // Debug.Log("[HandController] touch pad " + primary2DAxisValue);

            Move(primary2DAxisValue);
        }
    }

    private void Move(Vector2 inputAxis)
    {
        // find rotation of head
        float yaw = _camTransform.transform.eulerAngles.y;
        Quaternion headYaw = Quaternion.Euler(0, yaw, 0);

        // input direction
        Vector3 direction = new Vector3(inputAxis.x, 0, inputAxis.y);

        // correct for head rotation
        direction = headYaw * direction;

        _rigTransform.Translate(direction * (Time.deltaTime * _locomotionSpeed));
    }
    #endregion
}