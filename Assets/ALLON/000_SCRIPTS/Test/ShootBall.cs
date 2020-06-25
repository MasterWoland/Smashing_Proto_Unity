using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ShootBall : MonoBehaviour
{
    [SerializeField] private GameObject _debugObject;
    // private Transform _targetTransform;
    private Transform _transform;
    private Rigidbody _rigidbody;
    private bool _doLaunch = false;
    private Vector3 _targetPos = default;

    private void Awake()
    {
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void InitBall(Vector3 spawnPos)
    {
       Reset(spawnPos);
    }

    public void Launch(Vector3 targetPos)
    {
        _targetPos = targetPos;
        _doLaunch = true;
    }

    void FixedUpdate()
    {
        if (!_doLaunch) return;

        Vector3 direction = _targetPos - _transform.position;
        direction.Normalize();

        
        Debug.Log("Launch "+direction);
        
        _rigidbody.AddForce(direction * 1200f);

        _doLaunch = false;
    }

    public void Reset(Vector3 spawnPos)
    {
        _doLaunch = false;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        
        Vector3 pos = spawnPos;
        pos.y += _transform.localScale.y * 0.5f;

        _transform.position = pos;
    }
}