using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] int score = 100;
    private bool wasCollected = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !wasCollected)
        {
            wasCollected = true;

            FindObjectOfType<GameManager>().AddScore(score);

            this.gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
    }
}
