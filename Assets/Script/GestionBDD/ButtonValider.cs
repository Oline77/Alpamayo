/// @file: ButtonValider.cs
/// @brief: Permet que lorsqu'on click sur le bouton valider, les informations du formulaire sont envoy� dans la class DatabaseHelper pour �tre trait�
/// @author: Barbaud M.
/// @date: 04/04/2023
/// @update: ~

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ButtonValider : MonoBehaviour
{
    // D�claration des champs de saisie dans l'interface graphique de l'utilisateur
    public InputField nomProjetInput;
    public InputField nomClientInput;
    public InputField numeroChantierInput;
    public InputField adresseInput;
    public InputField voieInput;
    public InputField codePostalInput;
    public InputField villeInput;
    public InputField descriptionInput;



    // Fonction qui est appel�e lorsque l'utilisateur clique sur le bouton Valider
    public void OnValiderButtonClick()
    {
        Project newProject = new Project();

        // Attribution des valeurs entr�es par l'utilisateur aux propri�t�s de l'objet Project
        newProject.NomProjet = nomProjetInput.text;
        newProject.NomClient = nomClientInput.text;
        newProject.NumeroChantier = int.Parse(numeroChantierInput.text);
        newProject.Adresse = adresseInput.text;
        newProject.Voie = int.Parse(voieInput.text);
        newProject.CodePostale = int.Parse(codePostalInput.text);
        newProject.Ville = villeInput.text;
        newProject.Description = descriptionInput.text;

        try
        {
            // Cr�ation d'un objet helper pour interagir avec la base de donn�es
            DatabaseHelper helper = new DatabaseHelper();

            // Insertion des donn�es pour �tre trait�es
            helper.InsertProject(newProject);
        }
        catch (System.Exception e)
        {
            // En cas d'erreur lors de l'insertion, afficher un message d'erreur
            Debug.LogError("Erreur lors de l'insertion du projet : " + e.Message);
        }
    }
}

