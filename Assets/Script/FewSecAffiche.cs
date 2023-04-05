using System.Collections;
using UnityEngine;

public class FewSecAffiche : MonoBehaviour
{
    public Canvas canvas;
    public float timeInSeconds;

    public void Show()
    {
        StartCoroutine(ShowAndHide(canvas, timeInSeconds));
    }

    IEnumerator ShowAndHide(Canvas canvas, float delay)
    {
        canvas.gameObject.SetActive(true);
        yield return new WaitForSeconds(delay);
        canvas.gameObject.SetActive(false);
    }
}