using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallController : MonoBehaviour
{
    public static event Action<BallController> BallNotHit;

    [HideInInspector] public bool IsHit = false;
    [HideInInspector] public bool ShouldFollowParabola = false;
    private ShieldCollision _shieldCollision;
    private BallCollisionChecker _collisionChecker;
    private Vector3 _spawnPosition;
    private Vector3 _targetPosition;
    private Rigidbody _rigidbody;
    private float _timer = 0f;
    private float _speed = 2f; // MRA time to reach target position 
    private float _maxHeight = 3f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _shieldCollision = GetComponent<ShieldCollision>();
        _collisionChecker = GetComponent<BallCollisionChecker>();
        
        if(_shieldCollision == null) Debug.LogError("[BallController] No ShieldCollision component found.");
        if(_collisionChecker == null) Debug.LogError("[BallController] No CollisionChecker component found.");
    }

    private void FixedUpdate()
    {
        if (!ShouldFollowParabola) return;
        
        _timer += Time.fixedDeltaTime;

        Vector3 pos = MathParabola.Parabola(_spawnPosition, _targetPosition, _maxHeight, _timer / _speed);
        _rigidbody.MovePosition(pos);

        if (_timer >= _speed)
        {
            // our ball has not been hit and has finished its trajectory
            BallNotHit?.Invoke(this);

            // Debug.Log("[BallController] X __ Ball not hit __ X");
            Reset();
        }
    }

    public void PrepareBall(Vector3 spawnPos, Vector3 targetPos)
    {
        // Debug.Log("[BallController] Prepare()");

        _maxHeight = Random.Range(1.5f, 4.0f); // MRA: we may want to weigh this (curve?)

        _spawnPosition = spawnPos;
        var position = GetTargetPosition(targetPos, out float posX, out float posZ);
        _targetPosition = new Vector3(posX, position.y, posZ);

        ResetRigidbody();

        gameObject.SetActive(true);

        _timer = 0;
    }

    public void Launch()
    {
        // Debug.Log("[BallController] Launch");
        _collisionChecker.Reset();
        ShouldFollowParabola = true;
    }
    
    #region EVENTS
    private void OnEnable()
    {
        _shieldCollision.HitShield += OnHitShield;
    }

    private void OnDisable()
    {
        _shieldCollision.HitShield -= OnHitShield;
    }
    
    private void OnHitShield()
    {
        ShouldFollowParabola = false;
    }
    #endregion

    #region HELPER METHODS
    private Vector3 GetTargetPosition(Vector3 targetPos, out float posX, out float posZ)
    {
        var position = targetPos;
        posX = position.x;
        posZ = position.z;
        posX += Random.Range(-0.75f, 0.75f);
        posZ += Random.Range(-0.5f, 0.5f);
        return position;
    }

    private void ResetRigidbody()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.useGravity = false;
    }

    private void Reset()
    {
        // Debug.Log("RESET");
        ShouldFollowParabola = false;
        gameObject.SetActive(false);
        _collisionChecker.Reset();
    }
    #endregion
}