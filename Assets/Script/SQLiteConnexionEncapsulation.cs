using UnityEngine;
using System.Data; // Importation de la biblioth�que System.Data pour interagir avec la base de donn�es
using Mono.Data.Sqlite; // Importation de la biblioth�que Mono.Data.Sqlite pour interagir avec la base de donn�es SQLite
using System.IO; // Importation de la biblioth�que System.IO pour travailler avec les fichiers et les dossiers

/* Cette classe g�re la connexion � la base de donn�es et encapsule les requ�tes SQL pour cr�er une table, ins�rer des donn�es, et r�cup�rer des donn�es */

public class DatabaseManager
{

    private string dbName = "myDatabase.db"; // Nom de la base de donn�es
    private string dbPath; // Chemin d'acc�s � la base de donn�es
    private IDbConnection dbConnection; // Objet de connexion � la base de donn�es

    public DatabaseManager()
    {
        dbPath = Path.Combine(Application.persistentDataPath, dbName); // Obtient le chemin d'acc�s � la base de donn�es en fonction du nom de la base de donn�es et du dossier de stockage persistant de l'application
        dbConnection = new SqliteConnection("URI=file:" + dbPath); // Initialise l'objet de connexion avec le chemin d'acc�s � la base de donn�es
        dbConnection.Open(); // Ouvre la connexion � la base de donn�es
    }

    public void CreateTable(string tableName, string[] columnNames, string[] columnTypes)
    {
        string query = "CREATE TABLE IF NOT EXISTS " + tableName + " (id INTEGER PRIMARY KEY AUTOINCREMENT, ";

        for (int i = 0; i < columnNames.Length; i++)
        {
            query += columnNames[i] + " " + columnTypes[i] + ", "; // Ajoute le nom de la colonne et le type de donn�es � la requ�te SQL
        }

        query = query.TrimEnd(',', ' ') + ")"; // Supprime la derni�re virgule et l'espace inutile et termine la requ�te SQL
        ExecuteNonQuery(query); // Ex�cute la requ�te SQL pour cr�er une table
    }

    public void InsertData(string tableName, string[] columnNames, object[] columnValues)
    {
        string query = "INSERT INTO " + tableName + " (";

        for (int i = 0; i < columnNames.Length; i++)
        {
            query += columnNames[i] + ", "; // Ajoute le nom de la colonne � la requ�te SQL
        }

        query = query.TrimEnd(',', ' ') + ") VALUES (";

        for (int i = 0; i < columnValues.Length; i++)
        {
            if (columnValues[i] is string)
            {
                query += "'" + columnValues[i] + "', "; // Ajoute la valeur de la colonne entre guillemets simples s'il s'agit d'une cha�ne de caract�res
            }
            else
            {
                query += columnValues[i] + ", "; // Ajoute la valeur de la colonne telle quelle si ce n'est pas une cha�ne de caract�res
            }
        }

        query = query.TrimEnd(',', ' ') + ")"; // Supprime la derni�re virgule et l'espace inutile et termine la requ�te SQL
        ExecuteNonQuery(query); // Ex�cute la requ�te SQL pour ins�rer des donn�es dans une table
    }

    public IDataReader GetData(string tableName)
    {
        string query = "SELECT * FROM " + tableName; // Requ�te SQL pour r�cup�rer toutes les donn�es
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
