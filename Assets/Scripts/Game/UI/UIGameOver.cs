using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIGameOver : MonoBehaviour
{
    private int totalScore = 0;
    [SerializeField] TextMeshProUGUI scoreText;

    private void Start()
    {
        DisplayScore();
    }

    private void DisplayScore()
    {
        totalScore = FindObjectOfType<GameManager>().totalScore;
        scoreText.text = $"Your Score: {totalScore}";
    }

    private void Update()
    {

    }
}
