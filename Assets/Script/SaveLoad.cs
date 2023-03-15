using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoad : MonoBehaviour
{
    public string saveFileName = "save.dat";

    // Fonction appelée lors du clic sur le bouton "Sauvegarder"
    public void SaveScene()
    {
        // Crée un objet de sérialisation
        BinaryFormatter bf = new BinaryFormatter();

        // Ouvre le fichier pour écrire
        FileStream file = File.Create(Application.persistentDataPath + "/" + saveFileName);

        // Sérialise les données de la scène actuelle et les enregistre dans le fichier
        bf.Serialize(file, new SceneData());

        // Ferme le fichier
        file.Close();
    }

    // Fonction appelée lors du clic sur le bouton "Charger"
    public void LoadScene()
    {
        // Vérifie si le fichier de sauvegarde existe
        if (File.Exists(Application.persistentDataPath + "/" + saveFileName))
        {
            // Crée un objet de sérialisation
            BinaryFormatter bf = new BinaryFormatter();

            // Ouvre le fichier pour lire
            FileStream file = File.Open(Application.persistentDataPath + "/" + saveFileName, FileMode.Open);

            // Désérialise les données et les restaure dans la scène actuelle
            SceneData sceneData = (SceneData)bf.Deserialize(file);
            sceneData.Restore();

            // Ferme le fichier
            file.Close();
        }
    }
}

// Classe pour stocker les données de la scène actuelle
[System.Serializable]
public class SceneData
{
    // Les données que vous voulez sauvegarder, comme le placement des objets, les paramètres de la caméra, etc.
    // Vous pouvez ajouter ou supprimer des champs selon vos besoins

    // Fonction pour restaurer les données dans la scène actuelle
    public void Restore()
    {
        // Restaure les données sauvegardées dans la scène actuelle
    }
}