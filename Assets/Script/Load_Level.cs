using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Load_Level : MonoBehaviour
{
    Button ConceptionButton;
    Button VisualisationButton;
    Button MenuButton;
    //utton ExportationButton;
    private void Awake()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        ConceptionButton = root.Q<Button>("Conception");
        VisualisationButton = root.Q<Button>("Visualisation");
        MenuButton = root.Q<Button>("Exit");
        //ExportationButton = root.Q<Button>("Exportation");

        ConceptionButton.clicked += Conception;
        VisualisationButton.clicked += Visualisation;
        MenuButton.clicked += Exit;
        //ExportationButton.clicked += StartGame;
    }

    private void Conception()
    {
        Debug.Log("TEST");
        SceneManager.LoadScene("Conception");
    }

    private void Visualisation()
    {
        Debug.Log("Visu");
        SceneManager.LoadScene("Visualisation");
    }
    private void Exit()
    {
        Debug.Log("Menu");
        SceneManager.LoadScene("Menu");
    }

}