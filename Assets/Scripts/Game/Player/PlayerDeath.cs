using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public void ProcessPlayerDeath()
    {
        FindObjectOfType<GameManager>().ProcessPlayerDeath();
    }
}
