using UnityEngine;
using UnityEngine.UI;

public class DayNightController : MonoBehaviour
{
    // Références vers les éléments de la scène
    public Light targetLight;
    public Scrollbar scrollbar;
    public Text timeText;

    // Couleurs pour les différentes heures de la journée
    public Color[] colors;
    // Heures de transition entre les couleurs
    public int[] transitionHours;

    // Angle de départ et angle de fin pour l'orientation de la lumière
    public float startAngle = 0f;
    public float endAngle = 180f;

    // Variables de suivi de l'état actuel
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

        // Initialisation de la position de la scrollbar à 12h
        scrollbar.value = 0.5f;
    }


    private void Update()
    {
        // Vérification que toutes les références sont renseignées
        if (targetLight != null && scrollbar != null && timeText != null)
        {
            // Récupération de la valeur du scrollbar
            float value = scrollbar.value;

            // Interpolation de la couleur de la lumière en fonction du temps écoulé depuis le dernier changement de couleur
            targetLight.color = Color.Lerp(startColor, targetColor, currentColorLerpTime / colorLerpTime);
            // Orientation de la lumière
            targetLight.transform.rotation = Quaternion.Euler(new Vector3(Mathf.Lerp(startAngle, endAngle, value), 0, 0));

            // Calcul de l'heure en fonction de la position du scrollbar
            int newHour = Mathf.RoundToInt(value * 23);
            if (newHour != currentHour)
            {
                // Mise à jour de l'heure affichée
                currentHour = newHour;
                timeText.text = currentHour.ToString("00") + "h";

                // Changement de couleur en fondu
                for (int i = 0; i < transitionHours.Length; i++)
                {
                    if (currentHour < transitionHours[i])
                    {
                        // Définition des couleurs de départ et d'arrivée pour le fondu
                        startColor = targetColor;
                        targetColor = colors[i];
                        // Réinitialisation du temps écoulé pour le fondu
                        currentColorLerpTime = 0.0f;
                        // Calcul du temps total nécessaire pour effectuer le fondu jusqu'à l'heure suivante
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
                // La couleur de départ devient la couleur d'arrivée pour le prochain fondu
                startColor = targetColor;
            }
        }
    }
}
