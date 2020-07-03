using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class BallCollision : MonoBehaviour
{
    public static event Action<int> MultiplierHit;
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
            // Multiplier multiplier = other.gameObject.GetComponent<Multiplier>();

            // Debug.Log("[Ball] hit multiplier " + other.gameObject.name + ": " + multiplier.GetMultiplier());

            Vector3 distanceToCenter = other.transform.parent.position - other.GetContact(0).point;
            // Debug.Log("[Ball] Hit position "+other.GetContact(0).point);
            // Debug.Log("[Ball] Hit position DISTANCE "+distanceToCenter.magnitude);

            float distance = distanceToCenter.magnitude;
            if (distance <= 0.5f) // size is 1m, so radius is 0.5
            {
                MultiplierHit?.Invoke(5);
            } else if (distance <= 1.5f)
            {
                MultiplierHit?.Invoke(3);
            }
            else
            {
                MultiplierHit?.Invoke(2);
            }
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