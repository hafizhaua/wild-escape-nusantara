using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] public int playerLives { get; private set; } = 1;
    public int totalScore { get; private set; } = 0;

    public static GameManager _instance { get; private set; }

    private UnityEvent<int> OnScoreChanged;
    private UnityEvent<int> OnLivesChanged;


    void Awake()
    {
        // Singleton pattern
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    private void Start()
    {

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
            ResetToGameOver();
        }
    }

    private void ResetToGameOver()
    {
        // Go to Game Over
        SceneManager.LoadScene("GameOver");
    }

    private void TakeLife()
    {
        playerLives--;

        // Reset current scene
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void AddScore(int score)
    {
        totalScore += score;
    }

    public void ResetScore()
    {
        totalScore = 0;
    }
}
