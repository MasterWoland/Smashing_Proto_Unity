using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using Pixelplacement;
using TMPro;
using UnityEngine;

public class ScoreAnimation : MonoBehaviour
{
   public TextMeshPro ScoreText;
   private Transform _camTransform;
   private Transform _transform;
   
   private void Awake()
   {
      _transform = transform;
      Reset();
   }

   private void Start()
   {
      _camTransform = Camera.main.transform;
   }

   #region EVENTS
   private void OnEnable()
   {
      TargetCollision.TargetHit += OnTargetHit;
   }
   
   private void OnDisable()
   {
      TargetCollision.TargetHit -= OnTargetHit;
   }

   private void OnTargetHit(int points, Vector3 startPosition)
   {
      ScoreText.text = points.ToString();
      ScoreText.enabled = true;

      Vector3 targetPosition = startPosition;
      targetPosition.y += 3f;

      // MRA: translate positions towards camera, calculations should perhaps be made in Target
      
      
      Color targetColor = ScoreText.color;
      targetColor.a = 0;
      
      _transform.LookAt(_camTransform);
      
      Tween.Position(_transform, startPosition, targetPosition, 5f, 0f, null, Tween.LoopType.None, null, OnComplete);
      Tween.Value(ScoreText.color, targetColor, OnUpdateColor, 5f, 0f, null, Tween.LoopType.None);

   }

   private void OnUpdateColor(Color color)
   {
      ScoreText.color = color;
   }
   #endregion
   
   #region HELPER METHODS
   private void OnComplete()
   {
      Reset();
   }

   private void Reset()
   {
      ScoreText.text = string.Empty;
      ScoreText.enabled = false;
      ScoreText.color = Color.cyan;
   }
   #endregion
}
