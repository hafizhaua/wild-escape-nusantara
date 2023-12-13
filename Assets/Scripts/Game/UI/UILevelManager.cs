using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UILevelManager : MonoBehaviour
{

    [SerializeField] public List<TextMeshProUGUI> puzzleTexts;


    // Start is called before the first frame update
    void Start()
    {
        puzzleTexts[0].text = FindObjectOfType<GameManager>().getPuzzleByIndex("Level1").isPuzzleCollected.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
