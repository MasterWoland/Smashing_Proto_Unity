using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LaunchManager : MonoBehaviour
{
    [SerializeField] private GameObject _ballPrefab;
    [SerializeField] private int _ballPoolSize = 3; //MRA: from config
    public BallController[] _balls;
    private int _curBallIndex = 0;
    private BallController _currentBall;
    // private Transform _curBallTransform;
    // private Rigidbody _curBallRigidbody;
    // private bool _isBallLaunched = false;
    private bool _isGameRunning = false;
    private const float TIME_BETWEEN_LAUNCHES = 3f;
    private float _curLaunchTimer = 0;
    // private bool _isBallHit = false;
    
    // temp
    public Transform SpawnTF;
    public Transform TargetTF;
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

    // private void FixedUpdate()
    // {
    //     // if (!_isBallLaunched) return;
    //     // if (_isBallHit) return;
    //     
    //     _parabolaTime += Time.fixedDeltaTime;
    //     
    //     Vector3 pos = MathParabola.Parabola(SpawnTF.position, _targetPosition, _ballMaxHeight, _parabolaTime / _ballSpeed);
    //     _curBallRigidbody.MovePosition(pos);
    //
    //     if (_parabolaTime >= _ballSpeed)
    //     {
    //         Debug.Log("[LaunchManager] DONE");
    //         _isBallLaunched = false;
    //         _curBallRigidbody.useGravity = true;
    //         // dispatch event
    //
    //         // // MRA: this may be placed elsewhere
    //         _curBallIndex++;
    //         if (_curBallIndex >= _ballPoolSize) _curBallIndex = 0;
    //         SetCurrentBall(_curBallIndex);
    //         // // -------------------------------
    //     }
    // }

    #region HELPERS
    private void InitBallPool()
    {
        _balls = new BallController[_ballPoolSize];
        BallController ball;

        for (int i = 0; i < _ballPoolSize; i++)
        {
            ball = Instantiate(_ballPrefab, this.transform).GetComponent<BallController>();
            ball.gameObject.SetActive(false);
            _balls[i] = ball;
        }
    }

    private void LaunchBall()
    {
        SetCurrentBall(_curBallIndex);
        _curLaunchTimer = 0;

        // // min value 2.0 is good
        // // max value 6.0 is good, may even be higher
        // _ballMaxHeight = Random.Range(2.0f, 5.0f); // MRA: we may want to weigh this (curve?)
        //
        // var position = TargetTF.position;
        // float posX = position.x;
        // float posZ = position.z;
        // posX += Random.Range(-0.75f, 0.75f);
        // posZ += Random.Range(-0.75f, 1.0f);
        // _targetPosition = new Vector3(posX, position.y, posZ);
        
        // _parabolaTime = 0;
    }

    private void SetCurrentBall(int index)
    {
        _curBallIndex = index;
        _currentBall = _balls[_curBallIndex];
        _currentBall.PrepareBall(SpawnTF.position, TargetTF.position);
        _currentBall.Launch();
        // _curBallTransform = _balls[_curBallIndex].transform;
        // _curBallRigidbody = _balls[_curBallIndex].GetComponent<Rigidbody>();
        // _curBallRigidbody.velocity = Vector3.zero;
        // _curBallRigidbody.angularVelocity = Vector3.zero;
        // _curBallRigidbody.useGravity = false;
        // _curBallTransform.gameObject.SetActive(true);
        
        // MRA: this may be placed elsewhere
        // _curBallIndex++;
        // if (_curBallIndex >= _ballPoolSize) _curBallIndex = 0;
        // -------------------------------
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
        // ShieldCollision.Hit += OnHit;
    }

    private void OnDisable()
    {
        HandController.TriggerPressed -= OnTriggerPressed;
        // ShieldCollision.Hit -= OnHit;
    }

    private void OnTriggerPressed()
    {
        Debug.Log("[SimpleParabola] Trigger Pressed");

        // Launch ball if possible 
        if (!_isGameRunning)
        {
            StartGame();
        }
    }

    // private void OnHit()
    // {
    //     // _isBallHit = true; //MRA: this means we no longer update the parabola
    //     
    //     SetCurrentBall(_curBallIndex);
    // }
    #endregion
}