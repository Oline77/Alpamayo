using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchCam : MonoBehaviour
{

    public GameObject[] Cameras;
    public Text buttonText;
    int currentCam;

    // Start is called before the first frame update
    void Start()
    {
        currentCam = 0;
        setCam(currentCam);
    }

    //Active ou désactive les caméras
    public void setCam(int idx)
    {
        for (int i = 0; i < Cameras.Length; i++)
        {
            if (i == idx)
            {
                Cameras[i].SetActive(true);
            }
            else
            {
                Cameras[i].SetActive(false);
            }
        }
    }

    //Rajoute le nombre de caméra
    public void toggleCam()
    {
        currentCam++;
        if (currentCam > Cameras.Length - 1)
            currentCam = 0;
        setCam(currentCam);
    }
}