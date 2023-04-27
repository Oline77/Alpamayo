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
        int numeroChantier, voie, codePostal;

        Project newProject = new Project();

        // Attribution des valeurs entrées par l'utilisateur aux propriétés de l'objet Project
        newProject.NomProjet = nomProjetInput.text;
        newProject.NomClient = nomClientInput.text;
        newProject.Adresse = adresseInput.text;
        newProject.Ville = villeInput.text;
        newProject.Description = descriptionInput.text;

        try
        {
            // Conversion des valeurs de type string en entiers
            numeroChantier = Int32.Parse(numeroChantierInput.text);
            voie = Int32.Parse(voieInput.text);
            codePostal = Int32.Parse(codePostalInput.text);

            // Attribution des valeurs converties aux propriétés de l'objet Project
            newProject.NumeroChantier = numeroChantier;
            newProject.Voie = voie;
            newProject.CodePostale = codePostal;

            // Création d'un objet helper pour interagir avec la base de données
            DatabaseHelper databaseHelper = new DatabaseHelper();

            // Insertion des données pour être traitées
            databaseHelper.InsertProject(newProject);

        }
        catch (FormatException e)
        {
            // En cas d'erreur de conversion, afficher un message d'erreur
            Debug.LogError("Erreur de conversion : " + e.Message);
        }
        catch (System.Exception e)
        {
            // En cas d'erreur lors de l'insertion, afficher un message d'erreur
            Debug.LogError("Erreur lors de l'insertion du projet : " + e.Message);
        }
    }
}

