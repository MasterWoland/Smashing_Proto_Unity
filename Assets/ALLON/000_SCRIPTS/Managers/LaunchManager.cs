using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LaunchManager : MonoBehaviour
{
    [SerializeField] private GameObject _ballPrefab;
    [SerializeField] private int _ballPoolSize = 3; //MRA: from config
    [SerializeField] private Trajectory[] _trajectories;
    [SerializeField] private Transform _targetTransform;
    private Trajectory _curTrajectory;
    private BallController_AddForce[] _balls;
    // private BallController[] _balls;
    private int _curBallIndex = 0;
    private BallController_AddForce _currentBall;
    // private BallController _currentBall;
    private bool _isGameRunning = false;
    private const float TIME_BETWEEN_LAUNCHES = 3f;
    private float _curLaunchTimer = 0;
    
    // temp
    // public Transform SpawnTF;
    // public Transform TargetTF;
    private Vector3 _targetPosition;
    private float _parabolaTime = 0f;
    private float _ballSpeed = 2f; // MRA time to reach target position 
    private float _ballMaxHeight = 3f;
    // -----


    private void Awake()
    {
        InitBallPool();
        
        // MRA: for now we cannot have multiple balls being controlled by the Parabola at the same time
        if(_ballSpeed > TIME_BETWEEN_LAUNCHES) Debug.LogError("[LaunchManager] Ball speed should be less than "+TIME_BETWEEN_LAUNCHES);
    }

    /// <summary>
    /// Update checks if it is time to launch a ball.
    /// We may prefer to do this in a Coroutine instead.
    /// </summary>
    private void Update()
    {
        if (!_isGameRunning) return;
        // if (_isBallHit) return;
        
        _curLaunchTimer += Time.deltaTime;
        if (_curLaunchTimer >= TIME_BETWEEN_LAUNCHES)
        {
            LaunchBall();
        }
    }

    #region HELPERS
    private void InitBallPool()
    {
        _balls = new BallController_AddForce[_ballPoolSize];
        BallController_AddForce ball;
        // BallController ball;

        for (int i = 0; i < _ballPoolSize; i++)
        {
            ball = Instantiate(_ballPrefab, this.transform).GetComponent<BallController_AddForce>();
            // ball = Instantiate(_ballPrefab, this.transform).GetComponent<BallController>();
            ball.gameObject.SetActive(false);
            _balls[i] = ball;
        }
    }

    private void LaunchBall()
    {
        // // MRA: this may be placed elsewhere
        _curBallIndex++;
        if (_curBallIndex >= _ballPoolSize) _curBallIndex = 0;
        // SetCurrentBall(_curBallIndex);
        // // -------------------------------
        
        SetCurrentBall(_curBallIndex);
        _curLaunchTimer = 0;
        
        // _currentBall.PrepareBall(_curTrajectory.SpawnPosition, _curTrajectory.TargetPosition);
        _currentBall.PrepareBall(_curTrajectory.SpawnPosition, _curTrajectory.TargetPosition);
        _currentBall.Launch();
    }

    private void SetCurrentBall(int index)
    {
        int amount = _trajectories.Length;
        _curTrajectory = _trajectories[Random.Range(0, amount)];
        
        _curBallIndex = index;
        _currentBall = _balls[_curBallIndex];
    }
    
    private void StartGame()
    {
        _isGameRunning = true;
        LaunchBall();
    }
    #endregion

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
        // Launch ball if possible 
        if (!_isGameRunning)
        {
            StartGame();
        }
    }
    #endregion
}