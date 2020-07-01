using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class Shield : MonoBehaviour
{
   public Vector3 _prevPosition;
   public Vector3 _curPosition;
   public Vector3 Direction = Vector3.zero;
   public float Speed;
   public float Magnitude;
   private Transform _transform;
   private Rigidbody _rigidbody;
   
   // public Vector3 Direction
   // {
   //    get
   //    {
   //       return _curPosition - _prevPosition;
   //    }
   // }

   private void Awake()
   {
      _transform = transform;

      _prevPosition = _curPosition = _transform.position;
   }

   private void Start()
   {
      _rigidbody = GetComponent<Rigidbody>();
   }

   private void Update()
   {
      _prevPosition = _curPosition;
      _curPosition = _transform.position;

      Direction = _curPosition - _prevPosition;
      Speed = Direction.magnitude; // MRA: work with threshold
      // Debug.Log("__magnitude: "+Direction.magnitude);
      // Direction.Normalize(); // do this in receiver
   }

   // private void FixedUpdate()
   // {
   //    Magnitude = _rigidbody.velocity.magnitude;
   //    
   //    Debug.Log("__vel: "+_rigidbody.velocity.sqrMagnitude);
   // }

   public float GetVelocityMagnitude()
   {
      return _rigidbody.velocity.magnitude;
   }

   public void DrawDebug()
   {
      var position = _transform.position;
      Debug.DrawLine(position, end: position + (Direction * 100f), Color.green, 200f);
   }
}
