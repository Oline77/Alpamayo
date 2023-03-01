using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoutonMenuConception : MonoBehaviour
{
    public GameObject Menu;

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
}
