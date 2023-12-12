using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Puzzle", fileName = "New Puzzle")]
public class PuzzleSO : ScriptableObject
{
    [SerializeField] public int puzzleIndex { get; private set; }
    [SerializeField] public SpriteRenderer puzzleImage;

}
