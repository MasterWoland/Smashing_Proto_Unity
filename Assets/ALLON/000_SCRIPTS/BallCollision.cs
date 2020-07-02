using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class BallCollision : MonoBehaviour
{
    public static event Action<float> MultiplierHit;
    private bool _hasHitMultiplier = false;

    private void Awake()
    {
        Reset();
    }

    private void Reset()
    {
        _hasHitMultiplier = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("[Ball] hit player");
        }
        else if (other.gameObject.CompareTag("Ground"))
        {
            Debug.Log("[Ball] hit ground");
        }
        else if (other.gameObject.CompareTag("Multiplier") && !_hasHitMultiplier)
        {
            _hasHitMultiplier = true;
            Multiplier multiplier = other.gameObject.GetComponent<Multiplier>();
            MultiplierHit?.Invoke(multiplier.GetMultiplier());

            Debug.Log("[Ball] hit multiplier " + other.gameObject.name + ": " + multiplier.GetMultiplier());
        }
        else
        {
            Debug.Log("[Ball] hit: " + other.gameObject.tag);
        }
    }

    #region EVENTS

    private void OnEnable()
    {
        HandController.TriggerPressed += OnTriggerPressed;
    }

    private void OnDisable()
    {
        HandController.TriggerPressed -= OnTriggerPressed;
    }

    private void OnTriggerPressed()
    {
        Reset();
    }

    #endregion
}