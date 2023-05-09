using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SwitchCam : MonoBehaviour
{
    public GameObject[] Cameras;
    private int currentCam;
    private Vector3[] originalPositions; // pour stocker les positions d'origine des cam�ras

    public TMP_Text Switch;
    private int counter = 0;
    private string[] newTexts;

    // Start is called before the first frame update
    void Start()
    {
        Switch.text += "2D";
        newTexts = new string[3];
        newTexts[0] = Switch.text;
        newTexts[1] = "3D";
        //newTexts[3] = "2D";
        originalPositions = new Vector3[Cameras.Length]; // initialiser le tableau des positions d'origine
        for (int i = 0; i < Cameras.Length; i++)
        {
            originalPositions[i] = Cameras[i].transform.position; // stocker la position d'origine de chaque cam�ra
        }
        setCam(currentCam);
    }

    //Active ou d�sactive les cam�ras
    public void setCam(int idx)
    {
        for (int i = 0; i < Cameras.Length; i++)
        {
            if (i == idx)
            {
                Cameras[i].SetActive(true);
                Cameras[i].transform.position = originalPositions[i]; // r�initialiser la position de la cam�ra activ�e
            }
            else
            {
                Cameras[i].SetActive(false);
            }
        }
    }

    //Change le text � chaque clique s
    public void newText()
    {
        counter++;
        Switch.text = newTexts[counter];
        if (counter == 1)
        {
            counter = -1;
        }
    }

    //Rajoute le nombre de cam�ra
    public void toggleCam()
    {
        currentCam++;
        if (currentCam > Cameras.Length - 1)
            currentCam = 0;
        setCam(currentCam);
    }
}