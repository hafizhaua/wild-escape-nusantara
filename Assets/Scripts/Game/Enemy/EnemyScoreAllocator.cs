using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScoreAllocator : MonoBehaviour
{
    [SerializeField] public int killScore = 100;

    public void AddKillScore()
    {
        FindObjectOfType<GameManager>().AddScore(killScore);
    }

}
