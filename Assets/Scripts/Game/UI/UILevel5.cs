using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UILevel5 : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    // Start is called before the first frame update

    private void DisplayScore()
    {
        scoreText.text = $"You've opened new Biome!";
    }
}
