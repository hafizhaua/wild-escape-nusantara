using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleDrop : MonoBehaviour
{

    private Image image;

    

    private void Start()
    {
        image = this.gameObject.GetComponent<Image>();
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PuzzlePiece")
        {
            if (gameObject.name == other.gameObject.name)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
                other.gameObject.GetComponent<Image>().color = new Color(image.color.r, image.color.g, image.color.b, 0f);
            }
        }
    }

}
