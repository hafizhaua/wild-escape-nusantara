using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] int playerLives = 3;

    
    void Awake()
    {
        // Find game manager in each scenes
        int numGameManager = FindObjectsOfType<GameManager>().Length;
        if (numGameManager > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void ProcessPlayerDeath()
    {
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

        // Reset current scene
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
