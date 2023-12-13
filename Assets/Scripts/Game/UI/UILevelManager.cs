using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class UILevelManager : MonoBehaviour
{

    [SerializeField] public List<Sprite> puzzleSprites;
    [SerializeField] public List<GameObject> puzzlePlaceholder;

    private List<string> levels = new List<string>()
    {
        "Level1","Level2","Level3","Level4",
    };


    void Start()
    {
        int i = 0;
        foreach (var level in levels)
        {
            if (!FindObjectOfType<GameManager>().getPuzzleByIndex(level).isPuzzleCollected)
            {
                puzzlePlaceholder[i].SetActive(false);
            }

            i++;
        }

    }

    void Update()
    {

    }
}
