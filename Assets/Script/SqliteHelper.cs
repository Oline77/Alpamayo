using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mono.Data.Sqlite;
using UnityEngine;
using System.Data;

namespace DataBank
{
    public class SqliteHelper
    {
        private const string Tag = "Riz: SqliteHelper:\t";

        // Nom de la base de donn�es
        private const string database_name = "my_db";

        // Chaine de connection � la base de donn�es
        public string db_connection_string;

        // Connexion � la base de donn�es
        public IDbConnection db_connection;

        // Constructeur de la classe, initialise la connexion � la base de donn�es
        public SqliteHelper()
        {
            db_connection_string = "URI=file:" + Application.persistentDataPath + "/" + database_name;
            Debug.Log("db_connection_string" + db_connection_string);
            db_connection = new SqliteConnection(db_connection_string);
            db_connection.Open();
        }

        // Destructeur de la classe, ferme la connexion � la base de donn�es
        ~SqliteHelper()
        {
            db_connection.Close();
        }

        // Fonctions virtuelles (� impl�menter dans les classes qui h�ritent de cette classe)
        public virtual IDataReader getDataById(int id)
        {
            Debug.Log(Tag + "This function is not implemnted");
            throw null;
        }

        public virtual IDataReader getDataByString(string str)
        {
            Debug.Log(Tag + "This function is not implemnted");
            throw null;
        }

        public virtual void deleteDataById(int id)
        {
            Debug.Log(Tag + "This function is not implemented");
            throw null;
        }

        public virtual void deleteDataByString(string id)
        {
            Debug.Log(Tag + "This function is not implemented");
            throw null;
        }

        public virtual IDataReader getAllData()
        {
            Debug.Log(Tag + "This function is not implemented");
            throw null;
        }

        public virtual void deleteAllData()
        {
            Debug.Log(Tag + "This function is not implemnted");
            throw null;
        }

        public virtual IDataReader getNumOfRows()
        {
            Debug.Log(Tag + "This function is not implemnted");
            throw null;
        }

        // Fonctions d'aide
        // Retourne un objet IDbCommand pour ex�cuter des requ�tes SQL sur la base de donn�es
        public IDbCommand getDbCommand()
        {
            return db_connection.CreateCommand();
        }

        // Renvoie tous les enregistrements de la table sp�cifi�e
        public IDataReader getAllData(string table_name)
        {
            IDbCommand dbcmd = db_connection.CreateCommand();
            dbcmd.CommandText =
                "SELECT * FROM " + table_name;
            IDataReader reader = dbcmd.ExecuteReader();
            return reader;
        }

        // Supprime toutes les donn�es de la table sp�cifi�e
        public void deleteAllData(string table_name)
        {
            IDbCommand dbcmd = db_connection.CreateCommand();
            dbcmd.CommandText = "DROP TABLE IF EXISTS " + table_name;
            dbcmd.ExecuteNonQuery();
        }

        // Renvoie le nombre total de lignes dans la table sp�cifi�e
        public IDataReader getNumOfRows(string table_name)
        {
            IDbCommand dbcmd = db_connection.CreateCommand();
            dbcmd.CommandText =
                "SELECT COALESCE(MAX(id)+1, 0) FROM " + table_name;
            IDataReader reader = dbcmd.ExecuteReader();
            return reader;
        }

        public void close()
        {
            db_connection.Close();
        }
    }
}