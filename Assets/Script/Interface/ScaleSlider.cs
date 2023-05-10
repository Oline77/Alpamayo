using UnityEngine;
using UnityEngine.UI;

public class ScaleSlider : MonoBehaviour
{
    public Slider scaleSlider;
    public float minScale = 0.5f;
    public float maxScale = 3f;

    private Vector3 initialScale;
    private Vector3 centerPosition;

    private void Start()
    {
        GameObject[] selectableObjects = GameObject.FindGameObjectsWithTag("Selectable");

        foreach (GameObject selectableObject in selectableObjects)
        {
            initialScale = selectableObject.transform.localScale;
            centerPosition = selectableObject.transform.position;
        }
    }

    public void OnSliderChanged()
    {
        GameObject[] selectableObjects = GameObject.FindGameObjectsWithTag("Selectable");

        foreach (GameObject selectableObject in selectableObjects)
        {
            float newScale = Mathf.Lerp(minScale, maxScale, scaleSlider.value);
            selectableObject.transform.localScale = initialScale * newScale;
            selectableObject.transform.position = centerPosition - (selectableObject.transform.localScale - initialScale) / 2f;
        }
    }
}
