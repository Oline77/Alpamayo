using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class scriptOnClick : MonoBehaviour
{
    Button Sapin;
    Button Arbre;
    GameObject obj;
    //utton ExportationButton;
    private void Awake()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        obj = GameObject.Find("PoserObjet");

        Sapin = root.Q<Button>("ButtonSapin");
        Arbre = root.Q<Button>("ButtonBouleau");

        Sapin.clicked += SapinClick;
        Arbre.clicked += ArbreClick;
    }

    private void SapinClick()
    {
        Debug.Log("SapinClick");
        obj.GetComponent<ActionButton>().SelectObject(1);


    }

    private void ArbreClick()
    {
        Debug.Log("ArbreClick");
        obj.GetComponent<ActionButton>().SelectObject(0);
    }

    
}
