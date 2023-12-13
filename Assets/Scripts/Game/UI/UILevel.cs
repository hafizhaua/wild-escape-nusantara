using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UILevel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI levelName;
    [SerializeField] GameObject popup;

    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        livesText.text = gameManager.playerLives.ToString();
        scoreText.text = gameManager.totalScore.ToString();
        levelName.text = SceneManager.GetActiveScene().name;
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

    public void DisplayCollectedPuzzle()
    {
        if (FindObjectOfType<PuzzleController>().collectedPuzzle.isPuzzleCollected)
        {
            StartCoroutine(DisplayCollectPuzzle());
        }
    }

    private IEnumerator DisplayCollectPuzzle()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        popup.SetActive(true);

        yield return new WaitForSecondsRealtime(2f);
        popup.SetActive(false);
    }
}
