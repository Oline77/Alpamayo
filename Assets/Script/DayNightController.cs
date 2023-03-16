using UnityEngine;
using UnityEngine.UI;

public class DayNightController : MonoBehaviour
{
    public Light targetLight;

    public Scrollbar scrollbar;

    public Color startColor = Color.green;
    public Color middleColor = Color.blue;
    public Color endColor = Color.red;

    public float startAngle = 0f;
    public float endAngle = 180f;

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
            targetLight.color = Color.Lerp(targetLight.color, endColor, value);
            targetLight.intensity = Mathf.Lerp(1.0f, 0.5f, value);
            targetLight.transform.rotation = Quaternion.Euler(new Vector3(Mathf.Lerp(startAngle, endAngle, value), 0, 0));
        }
    }
}
