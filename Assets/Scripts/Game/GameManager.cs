using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;

    private int totalScore = 0;

    private static GameManager _instance;

    public static GameManager Instance
    {
        get { return _instance; }
    }

    void Awake()
    {
        // Find game manager in each scenes
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // UI Update
        livesText.text = $"Lives: {playerLives}";
        scoreText.text = totalScore.ToString();
    }

    public void ProcessPlayerDeath()
    {
        StartCoroutine(DelayDeath());
    }

    IEnumerator DelayDeath()
    {
        yield return new WaitForSecondsRealtime(1f);

        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetToFirstScene();
        }
    }

    private void ResetToFirstScene()
    {
        // Back to first scene
        SceneManager.LoadScene(0);
        Destroy(this.gameObject);
    }

    private void TakeLife()
    {
        playerLives--;

        // UI Update
        livesText.text = $"Lives: {playerLives}";

        // Reset current scene
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void AddScore(int score)
    {
        totalScore += score;

        // Update UI
        scoreText.text = totalScore.ToString();
    }
}
