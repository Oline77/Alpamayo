using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data.SQLite;
using Mono.Data.Sqlite;

public class DatabaseHelper
{
    // Méthode de connexion à la base de donnée
    public static SQLiteConnection GetConnection()
    {
        string connectionString = "Data Source=StockageInfoFormulaire.sqlite";
        return new SQLiteConnection(connectionString);
    }

    public void InsertProject(Project project)
    {
        using (var connection = GetConnection())
        {
            connection.Open();

            // Cr�ation de la table si elle n'existe pas d�j�
            using (var command = new SQLiteCommand("CREATE TABLE IF NOT EXISTS CreateProject " +
                "(Id INTEGER," +
                "NomProjet TEXT NOT NULL, " +
                "NomClient TEXT NOT NULL, " +
                "NumeroChantier NUMERIC NOT NULL, " +
                "Adresse TEXT NOT NULL, " +
                "Voie NUMERIC NOT NULL, " +
                "CodePostale NUMERIC NOT NULL, " +
                "Ville TEXT NOT NULL, " +
                "Description TEXT NOT NULL" +
                "PRIMARY KEY(Id AUTOINCREMENT))", connection))
            {
                command.ExecuteNonQuery();
            }

            // Insertion des informations du formulaire dans la table
            using (var command = new SQLiteCommand("INSERT INTO CreateProject " +
                "(Id, " +
                "NomProjet," +
                "NomClient," +
                "NomClient, " +
                "NumeroChantier, " +
                "Adresse, " +
                "Voie, " +
                "CodePostale, " +
                "Ville, " +
                "Description) VALUES " +
                "(@Id, " +
                "@NomProjet, " +
                "@NomClient, " +
                "@NumeroChantier, " +
                "@Adresse, " +
                "@Voie, " +
                "@CodePostale, " +
                "@Ville, " +
                "@Description)", connection))
            {
                command.Parameters.AddWithValue("@Id", project.Id);
                command.Parameters.AddWithValue("@NomProjet", project.NomProjet);
                command.Parameters.AddWithValue("@NomClient", project.NomClient);
                command.Parameters.AddWithValue("@NumeroChantier", project.NumeroChantier);
                command.Parameters.AddWithValue("@Adresse", project.Adresse);
                command.Parameters.AddWithValue("@Voie", project.Voie);
                command.Parameters.AddWithValue("@CodePostale", project.CodePostale);
                command.Parameters.AddWithValue("@Ville", project.Ville);
                command.Parameters.AddWithValue("@Description", project.Description);
                command.ExecuteNonQuery();
            }
        }
    }
}
