using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsParabolaManager : MonoBehaviour
{
    [SerializeField] private ShootBall _ball;
    [SerializeField] private GameObject _debugPrefab;
    [SerializeField] private Transform _spawnTransform;
    [SerializeField] private Transform _targetTransform;
    // [SerializeField] private float _ballSpeed = 3f;
    private Transform _ballTransform = null;
    private Rigidbody _ballRigidbody = null;
    private bool _isBallAvailable = true;
    private Transform _camTransform;
    private float _timer = 0;
    private List<GameObject> _debugBalls = new List<GameObject>();
    
    private void Start()
    {
       _ball.InitBall(_spawnTransform.position);
    }

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
        if (_isBallAvailable)
        {
            _isBallAvailable = false;
            _ball.gameObject.SetActive(true);
            _ball.Launch(_targetTransform.position);
        }
    }

    private void Update()
    {
        // editor testing
        if (Input.GetMouseButtonDown(0))
        {
            if (_isBallAvailable)
            {
                _isBallAvailable = false;
                _ball.gameObject.SetActive(true);
                _ball.Launch(_targetTransform.position);
            }
        }
        // -----------------

        if (_isBallAvailable) return;

        // GameObject go = Instantiate(_debugPrefab, _ball.transform.position, Quaternion.identity, _spawnTransform);
        // _debugBalls.Add(go);

        _timer += Time.deltaTime;

        if (_timer >= 3f)
        {
            Reset();
        }
    }

    private void Reset()
    {
        Debug.Log("[Manager] Ball Reset");

        _timer = 0;
        _isBallAvailable = true;
        DestroyDebugBalls();
        _debugBalls.Clear();
        
        // _ball.InitBall(_spawnTransform.position);
        _ball.Reset(_spawnTransform.position);
        _ball.gameObject.SetActive(false);
    }

    private void DestroyDebugBalls()
    {
        foreach (GameObject debugBall in _debugBalls)
        {
            Destroy(debugBall);
        }
    }
}
