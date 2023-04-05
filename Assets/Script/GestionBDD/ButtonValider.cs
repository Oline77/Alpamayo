/// @file: ButtonValider.cs
/// @brief: Permet que lorsqu'on click sur le bouton valider, les informations du formulaire sont envoyé dans la class DatabaseHelper pour être traité
/// @author: Barbaud M.
/// @date: 04/04/2023
/// @update: 05/04/2023

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class ButtonValider : MonoBehaviour
{
    // Déclaration des champs de saisie dans l'interface graphique de l'utilisateur
    public TextMeshProUGUI nomProjetInput;
    public TextMeshProUGUI nomClientInput;
    public TextMeshProUGUI numeroChantierInput;
    public TextMeshProUGUI adresseInput;
    public TextMeshProUGUI voieInput;
    public TextMeshProUGUI codePostalInput;
    public TextMeshProUGUI villeInput;
    public TextMeshProUGUI descriptionInput;

    // Fonction qui est appelée lorsque l'utilisateur clique sur le bouton Valider
    public void OnValiderButtonClick()
    {
        /*
        // Vérifier que le champ "numeroChantierInput" contient un nombre valide;
        if (!int.TryParse(numeroChantierInput.text, out numeroChantier))
        {
            Debug.LogError("Le champ Numero Chantier doit contenir un nombre valide.");
        }

        // Vérifier que le champ "voieInput" contient un nombre valide
        if (!int.TryParse(voieInput.text, out voie))
        {
            Debug.LogError("Le champ Voie doit contenir un nombre valide.");
        }

        // Vérifier que le champ "codePostalInput" contient un nombre valide
        if (!int.TryParse(codePostalInput.text, out codePostal))
        {
            Debug.LogError("Le champ Code Postal doit contenir un nombre valide.");
        }*/

        Project newProject = new Project();

        // Attribution des valeurs entrées par l'utilisateur aux propriétés de l'objet Project
        newProject.NomProjet = nomProjetInput.text;
        newProject.NomClient = nomClientInput.text;
        //newProject.NumeroChantier = int.Parse(numeroChantierInput.text);
        newProject.Adresse = adresseInput.text;
        //newProject.Voie = int.Parse(voieInput.text);
        //newProject.CodePostale = int.Parse(codePostalInput.text);
        newProject.Ville = villeInput.text;
        newProject.Description = descriptionInput.text;

        try
        {
            // Création d'un objet helper pour interagir avec la base de données
            DatabaseHelper databaseHelper = new DatabaseHelper();

            // Insertion des données pour être traitées
            databaseHelper.InsertProject(newProject);
        }
        catch (System.Exception e)
        {
            // En cas d'erreur lors de l'insertion, afficher un message d'erreur
            Debug.LogError("Erreur lors de l'insertion du projet : " + e.Message);
        }
    }
}

