using UnityEngine;
using UnityEngine.UI;

public class DayNightController : MonoBehaviour
{
    public Light targetLight;
    public Scrollbar scrollbar;
    public Text timeText;

    public Color color1 = new Color(0.2f, 0.2f, 0.4f);
    public Color color2 = new Color(0.4f, 0.4f, 0.6f);
    public Color color3 = new Color(1f, 0.8f, 0.6f);
    public Color color4 = new Color(0.8f, 0.2f, 0.2f);

    public float startAngle = 0f;
    public float endAngle = 180f;

    private int currentHour = 0;
    private Color targetColor;
    private Color startColor;
    private float colorLerpTime = 1.0f;
    private float currentColorLerpTime = 0.0f;

    private void Awake()
    {
        if (targetLight == null)
        {
            targetLight = GetComponent<Light>();
        }
        startColor = targetColor = color1;
    }

    private void Update()
    {
        if (targetLight != null && scrollbar != null && timeText != null)
        {
            float value = scrollbar.value;
            targetLight.color = Color.Lerp(startColor, targetColor, currentColorLerpTime / colorLerpTime);
            targetLight.intensity = Mathf.Lerp(1.0f, 0.5f, value);
            targetLight.transform.rotation = Quaternion.Euler(new Vector3(Mathf.Lerp(startAngle, endAngle, value), 0, 0));

            // Calcul de l'heure en fonction de la position du scrollbar
            int newHour = Mathf.RoundToInt(value * 23);
            if (newHour != currentHour)
            {
                currentHour = newHour;
                timeText.text = currentHour.ToString("00") + "h";

                // Changement de couleur en fondu
                if (currentHour >= 0 && currentHour < 8)
                {
                    targetColor = color1;
                    colorLerpTime = 1.0f;
                }
                else if (currentHour >= 8 && currentHour < 12)
                {
                    startColor = targetColor;
                    targetColor = color2;
                    currentColorLerpTime = 0.0f;
                    colorLerpTime = 1.0f;
                }
                else if (currentHour >= 12 && currentHour < 17)
                {
                    startColor = targetColor;
                    targetColor = color3;
                    currentColorLerpTime = 0.0f;
                    colorLerpTime = 1.0f;
                }
                else if (currentHour >= 17 && currentHour < 23)
                {
                    startColor = targetColor;
                    targetColor = color4;
                    currentColorLerpTime = 0.0f;
                    colorLerpTime = 1.0f;
                }
            }

            // Fondu de couleur
            if (currentColorLerpTime < colorLerpTime)
            {
                currentColorLerpTime += Time.deltaTime;
            }
            else
            {
                startColor = targetColor;
            }
        }
    }
}
