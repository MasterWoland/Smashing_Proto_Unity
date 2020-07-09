﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _multiplierText;
    [SerializeField] private TextMeshProUGUI _ballsMissedText;

    private int _score = 0;
    private int _currentMultiplier = 1;
    
    private void OnEnable()
    {
        Multiplier.MultiplierHit += OnMultiplierHit;
        TargetCollision.TargetHit += OnTargetHit;

        _scoreText.text = "Score: "+_score.ToString("000000");
    }

    private void OnTargetHit(int value, Vector3 position)
    {
        _score += value * _currentMultiplier;
        _scoreText.text = "Score: "+_score.ToString("000000");
    }

    private void OnDisable()
    {
        Multiplier.MultiplierHit -= OnMultiplierHit;
    }

    private void OnMultiplierHit(int multiplier)
    {
        // Debug.Log("[UI] multiplier: "+multiplier);
        _currentMultiplier = multiplier;
        _multiplierText.text = "Multiplier: " + _currentMultiplier.ToString()+"x";
    }
}
