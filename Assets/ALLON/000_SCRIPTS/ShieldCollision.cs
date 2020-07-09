using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Checks if the GameObject collides with a Shield
/// </summary>
public class ShieldCollision : MonoBehaviour
{
    public event Action HitShield;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other)
    {
        // Debug.Log("__hit by " + other.gameObject.name);

        if (other.gameObject.CompareTag("Player"))
        {
            // Debug.Log("__hit by player__");
            // _isHit = true;
            //
            Shield shield = other.gameObject.GetComponent<Shield>();
            
            if (shield)
            {
                // Debug.LogFormat("Shield speed: {0}, direction: {1}", shield.Speed, shield.Direction);
            
                Vector3 contactPoint = other.GetContact(0).point;
                // Debug.Log("Contact point: "+contactPoint);
                
                // Debug.Log("Contact point: "+other.GetContact(0).thisCollider.ClosestPoint());
                // _rb.isKinematic = false;
                _rigidbody.useGravity = true;
                
                Vector3 direction = shield.Direction;
                direction.Normalize();
                float force = shield.Speed * 175f;
            
                // MRA: add min and max values to force
                _rigidbody.AddForce(direction * force, ForceMode.Impulse);
                // _rb.AddForceAtPosition(direction * force, contactPoint, ForceMode.Impulse);
                shield.DrawDebug();

                HitShield?.Invoke();
                // Hit?.Invoke();
            }
        }
    }
}
