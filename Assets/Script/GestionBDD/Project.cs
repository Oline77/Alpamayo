/// @file: Projects.cs
/// @brief: Intialise les variables du formulaire pour cr�er un projet et v�rifie si les valeurs sont valides.
/// @author: Marin B.
/// @date: 30/03/2023
/// @update: ~

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Project
{
    public int Id { get; set; }
    public string NomProjet { get; set; }
    public string NomClient { get; set; }
    public int NumeroChantier { get; set; }
    public string Adresse { get; set; }
    public int Voie { get; set; }
    public int CodePostale { get; set; }
    public string Ville { get; set; }
    public string Description { get; set; }

    // Permet de v�rifier si les donn�es sont valides avant de les ins�rer dans la base de donn�e
    public bool IsValid()
    {
        if (string.IsNullOrEmpty(NomProjet) || string.IsNullOrEmpty(NomClient) || string.IsNullOrEmpty(Adresse) || string.IsNullOrEmpty(Ville) || string.IsNullOrEmpty(Description))
        { 
            return false;
        }

        if (NumeroChantier <= 0 || Voie <= 0 || CodePostale <= 0)
        {
            return false;
        }

        return true;
    }
}
