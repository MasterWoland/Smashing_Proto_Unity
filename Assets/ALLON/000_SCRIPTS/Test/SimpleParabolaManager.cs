using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class SimpleParabolaManager : MonoBehaviour
{
    [SerializeField] private GameObject _ballPrefab;
    [SerializeField] private GameObject _debugPrefab;
    [SerializeField] private Transform _spawnPos;
    [SerializeField] private Transform _endPos;
    [SerializeField] private float _ballSpeed = 3f;

    // [SerializeField] private Transform _spawnPos;
    private float _animTime;
    private Transform _currentBallTransform;
    private bool _isBall = false;

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
        Debug.Log("[SimpleParabola] Trigger Pressed");

        GameObject go = Instantiate(_ballPrefab, _spawnPos.position, Quaternion.identity, _spawnPos);
        _currentBallTransform = go.transform;

        _isBall = true;
    }

    private void Update()
    {
        if (_isBall)
        {
            _animTime += Time.deltaTime;
            
            _currentBallTransform.position = MathParabola.Parabola(_spawnPos.position, _endPos.position, 3f, _animTime/_ballSpeed);

            if (_animTime >= 5f)
            {
                _isBall = false;
                _animTime = 0;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            GameObject go = Instantiate(_ballPrefab, _spawnPos.position, Quaternion.identity, _spawnPos);
            _currentBallTransform = go.transform;

            _isBall = true;
        }
    }
}