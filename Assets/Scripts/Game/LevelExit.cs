using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    private float loadDelay = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(LoadNextScene());
        }
    }

    IEnumerator LoadNextScene()
    {
        // Tunggu selama 1 detik
        yield return new WaitForSecondsRealtime(loadDelay);

        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        string currentName = SceneManager.GetActiveScene().name;

        int nextSceneIndex = currentIndex + 1;

        if (!SceneManager.GetSceneAt(nextSceneIndex).name.Contains("Level"))
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }
}