using UnityEngine;
using System.Data; // Importation de la bibliothèque System.Data pour interagir avec la base de données
using Mono.Data.Sqlite; // Importation de la bibliothèque Mono.Data.Sqlite pour interagir avec la base de données SQLite
using System.IO; // Importation de la bibliothèque System.IO pour travailler avec les fichiers et les dossiers

/* Cette classe gère la connexion à la base de données et encapsule les requêtes SQL pour créer une table, insérer des données, et récupérer des données */

public class DatabaseManager
{

    private string dbName = "myDatabase.db"; // Nom de la base de données
    private string dbPath; // Chemin d'accès à la base de données
    private IDbConnection dbConnection; // Objet de connexion à la base de données

    public DatabaseManager()
    {
        dbPath = Path.Combine(Application.persistentDataPath, dbName); // Obtient le chemin d'accès à la base de données en fonction du nom de la base de données et du dossier de stockage persistant de l'application
        dbConnection = new SqliteConnection("URI=file:" + dbPath); // Initialise l'objet de connexion avec le chemin d'accès à la base de données
        dbConnection.Open(); // Ouvre la connexion à la base de données
    }

    public void CreateTable(string tableName, string[] columnNames, string[] columnTypes)
    {
        string query = "CREATE TABLE IF NOT EXISTS " + tableName + " (id INTEGER PRIMARY KEY AUTOINCREMENT, ";

        for (int i = 0; i < columnNames.Length; i++)
        {
            query += columnNames[i] + " " + columnTypes[i] + ", "; // Ajoute le nom de la colonne et le type de données à la requête SQL
        }

        query = query.TrimEnd(',', ' ') + ")"; // Supprime la dernière virgule et l'espace inutile et termine la requête SQL
        ExecuteNonQuery(query); // Exécute la requête SQL pour créer une table
    }

    public void InsertData(string tableName, string[] columnNames, object[] columnValues)
    {
        string query = "INSERT INTO " + tableName + " (";

        for (int i = 0; i < columnNames.Length; i++)
        {
            query += columnNames[i] + ", "; // Ajoute le nom de la colonne à la requête SQL
        }

        query = query.TrimEnd(',', ' ') + ") VALUES (";

        for (int i = 0; i < columnValues.Length; i++)
        {
            if (columnValues[i] is string)
            {
                query += "'" + columnValues[i] + "', "; // Ajoute la valeur de la colonne entre guillemets simples s'il s'agit d'une chaîne de caractères
            }
            else
            {
                query += columnValues[i] + ", "; // Ajoute la valeur de la colonne telle quelle si ce n'est pas une chaîne de caractères
            }
        }

        query = query.TrimEnd(',', ' ') + ")"; // Supprime la dernière virgule et l'espace inutile et termine la requête SQL
        ExecuteNonQuery(query); // Exécute la requête SQL pour insérer des données dans une table
    }

    public IDataReader GetData(string tableName)
    {
        string query = "SELECT * FROM " + tableName; // Requête SQL pour récupérer toutes les données
        return ExecuteReader(query);
    }

    public void CloseConnection()
    {
        dbConnection.Close();
        dbConnection = null;
    }

    private void ExecuteNonQuery(string query)
    {
        IDbCommand command = dbConnection.CreateCommand();
        command.CommandText = query;
        command.ExecuteNonQuery();
    }

    private IDataReader ExecuteReader(string query)
    {
        IDbCommand command = dbConnection.CreateCommand();
        command.CommandText = query;
        return command.ExecuteReader();
    }

}
