using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Load_Level : MonoBehaviour
{
        public string[] sceneNames; // Noms des scènes à charger
    public int sceneIndex = 0; // Index de la scène à charger (utilisé si sceneNames est vide)
    
    public void LoadScene()
    {
        // Vérifier s'il y a des noms de scène spécifiés
        if (sceneNames.Length > 0)
        {
            // Charger la scène suivante dans la liste
            SceneManager.LoadScene(sceneNames[sceneIndex]);
            sceneIndex = (sceneIndex + 1) % sceneNames.Length; // Passer à la scène suivante
        }
        else
        {
            // Charger la scène spécifiée par l'index
            SceneManager.LoadScene(sceneIndex);
        }
    }
}