using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    private string saveFilePath;

    private void Start()
    {
        saveFilePath = Application.persistentDataPath + "/saveData.dat";
    }

    public void SaveGame()
    {
        // Crée une instance de la classe de données que vous voulez sauvegarder
        // Par exemple, si vous voulez sauvegarder le score du joueur, vous pouvez créer une classe PlayerData qui contient un int score
        PlayerData playerData = new PlayerData();
        playerData.score = 10;

        // Ouvre un fichier en écriture
        FileStream file = File.Create(saveFilePath);

        // Sérialise les données de joueur et les écrit dans le fichier
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, playerData);

        // Ferme le fichier
        file.Close();
    }

    public void LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            // Ouvre le fichier en lecture
            FileStream file = File.Open(saveFilePath, FileMode.Open);

            // Désérialise les données de joueur et les stocke dans une instance de PlayerData
            BinaryFormatter bf = new BinaryFormatter();
            PlayerData playerData = (PlayerData)bf.Deserialize(file);

            // Ferme le fichier
            file.Close();

            // Utilisez les données de joueur pour charger la partie
            // Par exemple, vous pouvez définir le score du joueur à la valeur stockée dans playerData
            Debug.Log("Score loaded: " + playerData.score);
        }
        else
        {
            Debug.Log("Save file not found.");
        }
    }
}

// Classe de données de joueur, vous pouvez ajouter d'autres variables que vous voulez sauvegarder ici
[System.Serializable]
public class PlayerData
{
    public int score;
}
