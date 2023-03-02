using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Interface_Action : MonoBehaviour
{
    public GameObject Menu;
    public GameObject Quitter;
    public GameObject Open;
    public TMP_InputField[] inputFields; // tableau d'InputFields Text Mesh Pro

    private TouchScreenKeyboard keyboard; // variable privée pour stocker le clavier virtuel ouvert sur le téléphone

        // méthode appelée lorsque l'utilisateur clique sur l'InputField
    public void OnInputFieldClick()
    {
        // ouvre le clavier virtuel sur le téléphone
        keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
    }

    //Méthode permettant d'ouvrir d'afficher ou de fermer avec meme bouton 
    public void ButtonClicked()
    {
        if (Menu.activeInHierarchy == true)
        {
            Menu.SetActive(false);
        }
        else
        {
            Menu.SetActive(true);
        }
    }

    //Méthode permettant d'ouvrir de faure disparaître un objet
    public void ButtonLeave()
    {
        Quitter.SetActive(false);
    }

    //Méthode permettant d'ouvrir d'afficher un objet
    public void OpenMenu()
    {
      Open.SetActive(true);  
    }


    // méthode appelée lorsque l'utilisateur clique sur un InputField
    public void OnInputFieldClick(TMP_InputField clickedInputField)
    {
        // ouvre le clavier virtuel sur le téléphone
        keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);

        // stocke l'InputField cliqué dans une variable temporaire pour pouvoir y attribuer du texte plus tard
        clickedInputField = inputFields[GetClickedInputFieldIndex(clickedInputField)];
    }

    // méthode appelée à chaque image de l'application
    private void Update()
    {
        // vérifie si le clavier virtuel est ouvert et que l'utilisateur a fini de taper du texte
        if (keyboard != null && keyboard.status == TouchScreenKeyboard.Status.Done)
        {
            // attribue le texte tapé dans l'InputField correspondant
            if (inputFields != null && inputFields.Length > 0)
            {
                // récupère l'input field actif
                TMP_InputField activeInputField = null;
                foreach (TMP_InputField inputField in inputFields)
                {
                    if (inputField.isFocused)
                    {
                        activeInputField = inputField;
                        break;
                    }
                }

                // attribue le texte tapé dans l'input field actif
                if (activeInputField != null)
                {
                    activeInputField.text = keyboard.text;
                }
            }

            // réinitialise la variable keyboard
            keyboard = null;
        }
    }

    // méthode qui renvoie l'index de l'InputField cliqué dans le tableau d'InputFields
    private int GetClickedInputFieldIndex(TMP_InputField clickedInputField)
    {
        for (int i = 0; i < inputFields.Length; i++)
        {
            if (inputFields[i] == clickedInputField)
            {
                return i;
            }
        }

        return -1;
    }
}
