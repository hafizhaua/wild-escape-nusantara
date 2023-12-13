using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PuzzleController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] public PuzzleSO existingPuzzle;
    private PuzzleSO collectedPuzzle;

    void Start()
    {
        collectedPuzzle = FindObjectOfType<GameManager>().getPuzzleByIndex(existingPuzzle.puzzleIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Box")
        {
            StartCoroutine(CollectPuzzle());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Box")
        {
            UncollectPuzzle();
        }
    }

    IEnumerator CollectPuzzle()
    {
        yield return new WaitForSecondsRealtime(1f);
        existingPuzzle.GetPuzzleInfo();
        collectedPuzzle.isPuzzleCollected = true;

    }

    private void UncollectPuzzle()
    {
        existingPuzzle.GetPuzzleInfo();
        collectedPuzzle.isPuzzleCollected = false;
    }

}
