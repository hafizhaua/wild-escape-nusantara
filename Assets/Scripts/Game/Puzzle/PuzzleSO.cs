using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Puzzle", fileName = "New Puzzle")]
public class PuzzleSO : ScriptableObject
{
    [SerializeField] public string puzzleIndex;
    [SerializeField] public Sprite puzzleImage;
    [SerializeField] public bool isPuzzleCollected;

    public void GetPuzzleInfo()
    {
        Debug.Log($"{puzzleIndex} collected = {isPuzzleCollected}");
    }

}
