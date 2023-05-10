/// @file: DatabaseManager.cs
/// @brief: Le fichier contient une classe pour créer et gérer une base de données SQLite dans l'application Unity
/// @author: Barbaud M.
/// @date: 10/05/2023
/// @update: ~

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Data;
using Mono.Data.Sqlite;
using TMPro;
using System.Data.SQLite;

public class DatabaseManager : MonoBehaviour
{
    // Declaration des champs de saisie dans l'interface graphique de l'utilisateur
    public TextMeshProUGUI nomProjetInput;
    public TextMeshProUGUI nomClientInput;
    public TextMeshProUGUI numeroChantierInput;
    public TextMeshProUGUI adresseInput;
    public TextMeshProUGUI voieInput;
    public TextMeshProUGUI codePostaleInput;
    public TextMeshProUGUI villeInput;
    public TextMeshProUGUI descriptionInput;

    private IDbConnection _connection;

    private void Start()
    {
        // Chemin vers la base de données SQLite
        string connectionString = "URI=file:" + Application.persistentDataPath + "StockageInfoFormulaire.db";

        // Creation de la connexion à la base de données
        _connection = new SqliteConnection(connectionString);

        // Ouverture de la connexion à la base de données
        _connection.Open();

        // Creation de la table pour stocker les informations du formulaire
        //IDbCommand dbCommand = _connection.CreateCommand(); // Creation d'un objet de commande de base de donnée qui est associé à un connexion de base de donnée
        SqliteCommand dbCommand = (SqliteCommand)_connection.CreateCommand();

        dbCommand.CommandText = "CREATE TABLE IF NOT EXISTS Projects " +
                "(Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "NomProjet TEXT NOT NULL, " +
                "NomClient TEXT NOT NULL, " +
                "NumeroChantier TEXT NOT NULL, " +
                "Adresse TEXT NOT NULL, " +
                "Voie TEXT NOT NULL, " +
                "CodePostale TEXT NOT NULL, " +
                "Ville TEXT NOT NULL, " +
                "Description TEXT NOT NULL)";
        dbCommand.ExecuteNonQuery();
    }
    
    // Méthode qui sera appelé quand l'utilisateur click sur le bouton valider
    public void OnSendButtonClicked()
    {
        // Récupération des données du formulaire
        string nomProjet = nomProjetInput.text; // nomProjetInput correspond au champ d'entrée depuis le formulaire
        string nomClient = nomClientInput.text;
        string numeroChantier = numeroChantierInput.text; 
        string adresse = adresseInput.text;
        string voie = voieInput.text;
        string codePostale = codePostaleInput.text;
        string ville = villeInput.text;
        string description = descriptionInput.text;

        // Debug.Log Récupération des données depuis le formulaire
        Debug.Log("données depuis formulaire : " + nomProjet);
        Debug.Log("données depuis formulaire : " + nomClient.GetType() + " -> " + nomClient);
        Debug.Log("données depuis formulaire : " + numeroChantier);
        Debug.Log("données depuis formulaire : " + adresse);
        Debug.Log("données depuis formulaire : " + voie);
        Debug.Log("données depuis formulaire : " + codePostale);
        Debug.Log("données depuis formulaire : " + ville);
        Debug.Log("données depuis formulaire : " + description);

        // Insertion des données dans la base de données
        //IDbCommand dbCommand = _connection.CreateCommand();
        SqliteCommand dbCommand = (SqliteCommand)_connection.CreateCommand();

        // Ajout des données dans les variables de la base de donnée
        dbCommand.Parameters.Add(new SqliteParameter("@nomProjet", nomProjet));
        dbCommand.Parameters.Add(new SqliteParameter("@nomClient", nomClient));
        dbCommand.Parameters.Add(new SqliteParameter("@numeroChantier", numeroChantier));
        dbCommand.Parameters.Add(new SqliteParameter("@adresse", adresse));
        dbCommand.Parameters.Add(new SqliteParameter("@voie", voie));
        dbCommand.Parameters.Add(new SqliteParameter("@codePostale", codePostale));
        dbCommand.Parameters.Add(new SqliteParameter("@ville", ville));
        dbCommand.Parameters.Add(new SqliteParameter("@description", description));

        // Debug.Log Récupération des données depuis les variables
        Debug.Log("données depuis variables : " + dbCommand.Parameters["@nomProjet"].Value);
        Debug.Log("données depuis variables : " + dbCommand.Parameters["@nomClient"].Value);
        Debug.Log("données depuis variables : " + dbCommand.Parameters["@numeroChantier"].Value);
        Debug.Log("données depuis variables : " + dbCommand.Parameters["@adresse"].Value);
        Debug.Log("données depuis variables : " + dbCommand.Parameters["@voie"].Value);
        Debug.Log("données depuis variables : " + dbCommand.Parameters["@codePostale"].Value);
        Debug.Log("données depuis variables : " + dbCommand.Parameters["@ville"].Value);
        Debug.Log("données depuis variables : " + dbCommand.Parameters["@description"].Value);
  
        dbCommand.CommandText = "INSERT INTO Projects " +
            "(NomProjet, " +
            "NomClient, " +
            "NumeroChantier, " +
            "Adresse, " +
            "Voie, " +
            "CodePostale, " +
            "Ville, " +
            "Description) " +
            "VALUES " +
            "(@nomProjet, " +
            "@nomClient, " +
            "@numeroChantier, " +
            "@adresse, " +
            "@voie, " +
            "@codePostale, " +
            "@ville, " +
            "@description)";
        dbCommand.ExecuteNonQuery();

        _connection.Close();
    }

    private void OnDestroy()
    {
        // Fermeture de la connexion à la base de données

    }

    // Méthode pour lire la base de données
    /*public void LoadProjects()
    {
        IDbCommand dbCommand = _connection.CreateCommand();

        dbCommand.CommandText = "SELECT * FROM Projects";

        IDataReader reader = dbCommand.ExecuteReader();

        while(reader.Read())
        {
            int id = reader.GetInt32(0);
            string nomProjet = reader.GetString(1);
            string nomClient = reader.GetString(2);
            int numeroChantier = reader.GetInt32(3);
            string adresse = reader.GetString(4);
            int voie = reader.GetInt32(5);
            int codePostale = reader.GetInt32(6);
            string ville = reader.GetString(7);
            string description = reader.GetString(8);

            Debug.LogFormat("Projet {0} - " +
                "Nom du Projet: {1}, " +
                "Nom du Client: {2}, " +
                "Numero de Chantier {3}, " +
                "Adresse {4}, " +
                "Voie {5}, " +
                "Code Postale {6}, " +
                "Ville {7}, " +
                "Description {8}", 

                id, 
                nomProjet, 
                nomClient, 
                numeroChantier, 
                adresse, 
                voie, 
                codePostale, 
                ville, 
                description);
        }
        reader.Close();
    }*/
}
