using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    private float loadDelay = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && FindObjectOfType<PuzzleController>().collectedPuzzle.isPuzzleCollected)
        {
            StartCoroutine(LoadNextScene());
        }
    }

    IEnumerator LoadNextScene()
    {
        // Tunggu selama 1 detik
        yield return new WaitForSecondsRealtime(loadDelay);

        int currentIndex = SceneManager.GetActiveScene().buildIndex;

        int nextSceneIndex = currentIndex + 1;


        // 2 for not-level scenes
        if (nextSceneIndex < 4)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }

    // UtilityClasses
}