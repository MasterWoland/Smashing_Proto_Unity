using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class BallCollisionChecker : MonoBehaviour
{
    public static event Action<int> MultiplierHit;
    public bool _hasHitMultiplier = false;

    private void Awake()
    {
        Reset();
    }

    public void Reset()
    {
        _hasHitMultiplier = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Debug.Log("[Ball] hit player");
        }
        else if (other.gameObject.CompareTag("Multiplier"))// && !_hasHitMultiplier)
        {
            // Debug.Log("[Ball] hit multiplier "+other.gameObject.name);
            
            _hasHitMultiplier = true; // triggers only the first multiplier that it hits

            Vector3 distanceToCenter = other.transform.parent.position - other.GetContact(0).point;

            float distance = distanceToCenter.magnitude;
            if (distance <= 0.5f) // size is 1m, so radius is 0.5
            {
                // Debug.Log("[Ball] hit multiplier: 5 ");
                MultiplierHit?.Invoke(5);
            }
            else if (distance <= 1.5f)
            {
                // Debug.Log("[Ball] hit multiplier: 3 ");
                MultiplierHit?.Invoke(3);
            }
            else
            {
                // Debug.Log("[Ball] hit multiplier: 2 ");
                MultiplierHit?.Invoke(2);
            }
        }
        else if (other.gameObject.CompareTag("Ground"))
        {
            // Debug.Log("[Ball] hit ground");
        }
        else
        {
            // Debug.Log("[Ball] hit: " + other.gameObject.tag);
        }
    }
}