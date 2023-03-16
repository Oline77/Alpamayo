using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveFile : MonoBehaviour
{
    public void SaveScene()
    {
        string path = UnityEditor.EditorUtility.SaveFilePanel("Sauvegarder votre projet", "", "projetVert", "unity");
        if (path.Length != 0)
        {
            UnityEditor.SceneManagement.EditorSceneManager.SaveScene(UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene(), path);
        }
    }
}
