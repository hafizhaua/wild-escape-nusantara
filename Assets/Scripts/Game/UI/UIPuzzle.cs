using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIPuzzle : MonoBehaviour
{
    // Start is called before the first frame update

    private string levelName;
    private PuzzleController puzzle;

    void Start()
    {
        puzzle = FindObjectOfType<PuzzleController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PopUpPuzzle()
    {
        levelName = SceneManager.GetActiveScene().name;
        Debug.Log($"You got {levelName} puzzle!");
    }
}
