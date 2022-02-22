using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    //Singleton script for SoundManager Instance
    private static ScoreController instance;
    public static ScoreController Instance { get { return instance; } }

    private TMPro.TextMeshProUGUI scoreText;

    private int score = 0;

    private void Awake()
    {
        scoreText = GetComponent<TMPro.TextMeshProUGUI>();

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
    }

    private void Start()
    {
        RefreshUI();
    }

    public void IncreaseScore(int increment)
    {
        score += increment;
        RefreshUI();
    }

    private void RefreshUI()
    {
        scoreText.text = "P1 Score: " + score;
    }
}
