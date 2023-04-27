/// @file: DatabaseHelper.cs
/// @brief: Permet de se connecter a la base de donnee, de creer une table dans la base de donnee, d'inserer les informations et de recuperer les donnees pour les afficher.
/// @author: Barbaud M.
/// @date: 30/03/2023
/// @update: ~

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data.SQLite;
using System;

public class DatabaseHelper : MonoBehaviour
{
    // Methode de connexion a la base de donnee
    public static SQLiteConnection GetConnection()
    {
        string connectionString = "Data Source=StockageInfoFormulaire.db";
        return new SQLiteConnection(connectionString);
    }

    public void InsertProject(Project project)
    {
        // Verification des donnees
        /*if (!project.IsValid())
        {
            throw new ArgumentException("les entrees ne sont pas valides");
        }
        else
        {
            Debug.Log("Les entrees sont valides");
        }*/

        using (var connection = GetConnection())
        {
            connection.Open();

            // Creation de la table si elle n'existe pas deja
            using (var command = new SQLiteCommand("CREATE TABLE IF NOT EXISTS CreateProject " +
                "(Id INTEGER," +
                "NomProjet TEXT NOT NULL, " +
                "NomClient TEXT NOT NULL, " +
                "NumeroChantier INTEGER NOT NULL, " +
                "Adresse TEXT NOT NULL, " +
                "Voie INTEGER NOT NULL, " +
                "CodePostale INTEGER NOT NULL, " +
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

    // Permet de recuperer les donnees
    public List<Project> GetAllProjects()
    {
        List<Project> projects = new List<Project>();

        using (var connection = new SQLiteConnection("Data Source=StockageInfoFormulaire.db"))
        {
            connection.Open();

            using (var command = new SQLiteCommand("SELECT * FROM CreateProject", connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Project project = new Project();

                        project.Id = Convert.ToInt32(reader["Id"]);
                        project.NomProjet = Convert.ToString(reader["NomProjet"]);
                        project.NomClient = Convert.ToString(reader["NomClient"]);
                        project.NumeroChantier = Convert.ToInt32(reader["NumeroChantier"]);
                        project.Adresse = Convert.ToString(reader["Adresse"]);
                        project.Voie = Convert.ToInt32(reader["Voie"]);
                        project.CodePostale = Convert.ToInt32(reader["CodePostale"]);
                        project.Ville = Convert.ToString(reader["Ville"]);
                        project.Description = Convert.ToString(reader["Description"]);

                        projects.Add(project);
                    }
                }
            }
        }

        return projects;
    }
}
