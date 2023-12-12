using System.Collections;
using UnityEngine;

public class ShowHideUI : MonoBehaviour
{
    [SerializeField]
    public GameObject uiElement;

    public void ShowAndHideUI(float duration)
    {
        StartCoroutine(ShowAndHideRoutine(duration));
    }

    IEnumerator ShowAndHideRoutine(float duration)
    {
        if (uiElement != null)
        {
            uiElement.SetActive(true);

            yield return new WaitForSeconds(duration);

            uiElement.SetActive(false);
        }
    }
}
