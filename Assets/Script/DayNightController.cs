using UnityEngine;
using UnityEngine.UI;

public class DayNightController : MonoBehaviour
{
    // R�f�rences vers les �l�ments de la sc�ne
    public Light targetLight;
    public Scrollbar scrollbar;
    public Text timeText;

    // Couleurs pour les diff�rentes heures de la journ�e
    public Color[] colors;
    // Heures de transition entre les couleurs
    public int[] transitionHours;

    // Angle de d�part et angle de fin pour l'orientation de la lumi�re
    public float startAngle = 0f;
    public float endAngle = 180f;

    // Variables de suivi de l'�tat actuel
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

        // Initialisation de la position de la scrollbar � 12h
        scrollbar.value = 0.5f;
    }


    private void Update()
    {
        // V�rification que toutes les r�f�rences sont renseign�es
        if (targetLight != null && scrollbar != null && timeText != null)
        {
            // R�cup�ration de la valeur du scrollbar
            float value = scrollbar.value;

            // Interpolation de la couleur de la lumi�re en fonction du temps �coul� depuis le dernier changement de couleur
            targetLight.color = Color.Lerp(startColor, targetColor, currentColorLerpTime / colorLerpTime);
            // Orientation de la lumi�re
            targetLight.transform.rotation = Quaternion.Euler(new Vector3(Mathf.Lerp(startAngle, endAngle, value), 0, 0));

            // Calcul de l'heure en fonction de la position du scrollbar
            int newHour = Mathf.RoundToInt(value * 23);
            if (newHour != currentHour)
            {
                // Mise � jour de l'heure affich�e
                currentHour = newHour;
                timeText.text = currentHour.ToString("00") + "h";

                // Changement de couleur en fondu
                for (int i = 0; i < transitionHours.Length; i++)
                {
                    if (currentHour < transitionHours[i])
                    {
                        // D�finition des couleurs de d�part et d'arriv�e pour le fondu
                        startColor = targetColor;
                        targetColor = colors[i];
                        // R�initialisation du temps �coul� pour le fondu
                        currentColorLerpTime = 0.0f;
                        // Calcul du temps total n�cessaire pour effectuer le fondu jusqu'� l'heure suivante
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
                // La couleur de d�part devient la couleur d'arriv�e pour le prochain fondu
                startColor = targetColor;
            }
        }
    }
}
