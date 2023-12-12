using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PuzzleController : MonoBehaviour
{
    // Start is called before the first frame update


    public UnityEvent onPressed;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Box")
        {
            // Invoke UI Display
            onPressed.Invoke();
        }
    }
}
