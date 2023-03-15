using UnityEngine;
using UnityEngine.UI;

public class DayNightController : MonoBehaviour
{
    public Light targetLight;
    public Scrollbar scrollbar;
    public Color startColor = Color.green;
    public Color middleColor = Color.blue;
    public Color endColor = Color.red;

    private void Awake()
    {
        if (targetLight == null)
        {
            targetLight = GetComponent<Light>();
        }
    }

    private void Update()
    {
        if (targetLight != null && scrollbar != null)
        {
            float value = scrollbar.value;
            targetLight.color = Color.Lerp(startColor, middleColor, value);
            targetLight.intensity = Mathf.Lerp(0.1f, 1.0f, value);
        }
    }
}
