using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBetweenTwoPoints : MonoBehaviour
{
    public Transform MoveObject;
    public Transform StartPoint;
    public Transform EndPoint;
    private SphereCollider _collider;
    private float _speed = 1f;
    private bool _isGoingForward = true;
    private Rigidbody _rb;
    private bool _isHit = false;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<SphereCollider>();
    }

    private void FixedUpdate()
    {
        if (_isHit) return;

        if (_isGoingForward)
        {
            _speed += Time.deltaTime;

            if (_speed > 1f) _isGoingForward = false;
        }

        if (!_isGoingForward)
        {
            _speed -= Time.deltaTime;

            if (_speed <= 0) _isGoingForward = true;
        }

        Vector3 pos = Vector3.Lerp(StartPoint.position, EndPoint.position, _speed);
        _rb.MovePosition(pos);
        // transform.position = pos;
    }

    private void OnCollisionEnter(Collision other)
    {
        // Debug.Log("__hit by " + other.gameObject.name);

        if (other.gameObject.CompareTag("Player"))
        {
            // Debug.Log("__hit by player__");
            _isHit = true;
            
            Shield shield = other.gameObject.GetComponent<Shield>();

            if (shield)
            {
                // Debug.LogFormat("Shield speed: {0}, direction: {1}", shield.Speed, shield.Direction);

                Vector3 contactPoint = other.GetContact(0).point;
                // Debug.Log("Contact point: "+contactPoint);
                
                // Debug.Log("Contact point: "+other.GetContact(0).thisCollider.ClosestPoint());
                // _rb.isKinematic = false;
                _rb.useGravity = true;
                
                Vector3 direction = shield.Direction;
                direction.Normalize();
                float force = shield.Speed * 150f;

                // MRA: add min and max values to force
                _rb.AddForce(direction * force, ForceMode.Impulse);
                // _rb.AddForceAtPosition(direction * force, contactPoint, ForceMode.Impulse);
                shield.DrawDebug();
            }
        }
    }
    
    private void ResetBall()
    {
        _isHit = false;
        _isGoingForward = true;
        _rb.useGravity = false;
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
        
        transform.position = StartPoint.position;
    }
    
    private void OnEnable()
    {
        HandController.TriggerPressed += OnTriggerPressed;
    }

    private void OnTriggerPressed()
    {
        ResetBall();
    }

    private void OnDisable()
    {
        HandController.TriggerPressed -= OnTriggerPressed;
    }

}