using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class managerJoystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    // Les variables privées de la classe
    private Image imgJoystickBg;   // Le cercle du fond du joystick
    private Image imgJoystick;     // Le cercle que l'on peut déplacer
    private Vector2 posInput;      // La position du joystick

    void Start()
    {
        // On récupère les images dans les enfants de l'objet de ce script
        imgJoystickBg = GetComponent<Image>();
        imgJoystick = transform.GetChild(0).GetComponent<Image>();
    }

    // Lorsque l'on bouge le joystick
    public void OnDrag(PointerEventData eventData)
    {
        // On calcule la position du joystick par rapport au cercle du fond
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(imgJoystick.rectTransform, eventData.position, eventData.pressEventCamera, out posInput))
        {
            posInput.x = posInput.x / (imgJoystickBg.rectTransform.sizeDelta.x);
            posInput.y = posInput.y / (imgJoystickBg.rectTransform.sizeDelta.y);

            // Si la distance du joystick par rapport au centre est supérieure à 1
            if (posInput.magnitude > 1.0f)
            {
                posInput = posInput.normalized;
            }

            // On déplace le cercle du joystick par rapport à sa position d'origine
            imgJoystick.rectTransform.anchoredPosition = new Vector2(posInput.x * (imgJoystickBg.rectTransform.sizeDelta.x / 2), posInput.y * (imgJoystickBg.rectTransform.sizeDelta.y / 2));
        }
    }

    // Lorsque l'on appuie sur le joystick
    public void OnPointerDown(PointerEventData eventData)
    {
        // On appelle la méthode OnDrag pour mettre le joystick à la position du clic
        OnDrag(eventData);
    }

    // Lorsque l'on relâche le joystick
    public void OnPointerUp(PointerEventData eventData)
    {
        // On remet la position du joystick à zéro
        posInput = Vector2.zero;
        imgJoystick.rectTransform.anchoredPosition = Vector2.zero;
    }

    // La méthode pour récupérer l'input horizontal
    public float inputHorizontal()
    {
        if (Mathf.Abs(posInput.x) > 0.1f)
            return posInput.x;
        else
            return Input.GetAxis("Horizontal");
    }

    // La méthode pour récupérer l'input vertical
    public float inputVertical()
    {
        if (Mathf.Abs(posInput.y) > 0.1f)
            return posInput.y;
        else
            return Input.GetAxis("Vertical");
    }
}
