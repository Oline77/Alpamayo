using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UiInputManager : MonoBehaviour
{
    Button StartButton;
    Button HighscoreButton;
    Button LeaveButton;

    private void Awake()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        StartButton = root.Q<Button>("Conception");
        HighscoreButton = root.Q<Button>("Visualisation");
        LeaveButton = root.Q<Button>("Exportation");

        StartButton.clicked += StartGame;
        HighscoreButton.clicked += ShowHighscore;
        LeaveButton.clicked += LeaveGame;

    }

    private void StartGame()
    {
        Debug.Log("Starting Game");
        SceneManager.LoadScene("terrainDeplacement");
    }

    private void ShowHighscore()
    {
        Debug.Log("Showing Highscore");
    }

    private void LeaveGame()
    {
        Debug.Log("Leaving Game");
        Application.Quit();
    }

    private void OnDisable()
    {

        StartButton.clicked -= StartGame;
        HighscoreButton.clicked -= ShowHighscore;
        LeaveButton.clicked -= LeaveGame;
    }
}