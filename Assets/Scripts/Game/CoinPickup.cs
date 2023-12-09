using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{

    [SerializeField] private int scorePickup = 100;
    private bool wasCollected = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !wasCollected)
        {
            wasCollected = true;
            // Nambah ke main score menggunakan scorePickup

            this.gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
    }
}
