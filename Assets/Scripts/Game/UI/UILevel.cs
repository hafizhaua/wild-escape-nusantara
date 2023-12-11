using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UILevel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        livesText.text = gameManager.playerLives.ToString();
        scoreText.text = gameManager.totalScore.ToString();
    }

    public void SetLives(int playerLives)
    {
        livesText.text = $"Lives: {playerLives}";
    }

    public void SetScore(int totalScore)
    {
        scoreText.text = totalScore.ToString();
    }

    private void Update()
    {
        SetLives(gameManager.playerLives);
        SetScore(gameManager.totalScore);
    }
}
