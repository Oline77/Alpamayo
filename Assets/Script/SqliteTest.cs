using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;

public class SqliteTest : MonoBehaviour
{
    // A utiliser pour l'initialisation
    void Start()
    {
        // Création de la base de donnée
        string connection = "URI=file:" + Application.persistentDataPath + "/My_Database"; 

        // Ouverture de la connection avec la base de donnée
        IDbConnection dbcon = new SqliteConnection(connection);
        dbcon.Open();

        // Création d'une table
        IDbCommand dbcmd;
        IDataReader reader;

        dbcmd = dbcon.CreateCommand();
        string q_createTable =
          "CREATE TABLE IF NOT EXISTS my_table (id INTEGER PRIMARY KEY, val INTEGER )";

        dbcmd.CommandText = q_createTable;
        reader = dbcmd.ExecuteReader();

        // Insertion de valeur dans la table
        IDbCommand cmnd = dbcon.CreateCommand();
        cmnd.CommandText = "INSERT INTO my_table (id, val) VALUES (0, 5)";
        cmnd.ExecuteNonQuery();

        // Lire et imprimer toutes les valeurs du tableau
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader lecteur;
        string query = "SELECT * FROM my_table";
        cmnd_read.CommandText = query;
        lecteur = cmnd_read.ExecuteReader();

        while (lecteur.Read())
        {
            Debug.Log("id: " + lecteur[0].ToString());
            Debug.Log("val: " + lecteur[1].ToString());
        }

        dbcon.Close();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
