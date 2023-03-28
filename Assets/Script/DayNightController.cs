using UnityEngine;
using UnityEngine.UI;

public class DayNightController : MonoBehaviour
{
    public Light targetLight;
    public Scrollbar scrollbar;
    public Text timeText;

    public Color[] colors;
    public int[] transitionHours;

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
        startColor = targetColor = colors[0];
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
                for (int i = 0; i < transitionHours.Length; i++)
                {
                    if (currentHour < transitionHours[i])
                    {
                        startColor = targetColor;
                        targetColor = colors[i];
                        currentColorLerpTime = 0.0f;
                        colorLerpTime = (float)(transitionHours[i] - currentHour) / 24f;
                        break;
                    }
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
