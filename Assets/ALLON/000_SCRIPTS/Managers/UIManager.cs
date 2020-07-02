using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _multiplierText;
    [SerializeField] private TextMeshProUGUI _ballsMissedText;
    
    private void OnEnable()
    {
        BallCollision.MultiplierHit += OnMultiplierHit;
    }
    
    private void OnDisable()
    {
        BallCollision.MultiplierHit -= OnMultiplierHit;
    }

    private void OnMultiplierHit(float multiplier)
    {
        _multiplierText.text = "Multiplier: " + multiplier.ToString()+"x";
    }
}
