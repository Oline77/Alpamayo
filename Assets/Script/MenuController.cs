using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    // Tableau contenant les 10 boutons qui ouvrent les menus
    public Button[] menuButtons;

    // Tableau contenant les 10 animators qui contrôlent les menus
    public Animator[] menuAnimators;

    // Index du menu actuellement ouvert (-1 si aucun menu n'est ouvert)
    private int currentMenuIndex = -1;

    // Méthode appelée lorsqu'un bouton de menu est cliqué
    public void OnMenuButtonClick(int index)
    {

        // Si le même bouton est cliqué deux fois, on ferme le menu correspondant
        if (currentMenuIndex == index)
        {
            CloseMenu();
            return;
        }

        // Ferme le menu actuellement ouvert (s'il y en a un)
        CloseMenu();

        // Ouvre le nouveau menu
        menuAnimators[index].gameObject.SetActive(true);
        menuAnimators[index].SetTrigger("Open");

        // Met à jour l'index du menu actuellement ouvert
        currentMenuIndex = index;
    }

    // Méthode appelée lorsqu'on veut fermer le menu actuellement ouvert
    public void CloseMenu()
    {
        Debug.Log("CLIQUEOQUFOQUFUQE");
        if (currentMenuIndex >= 0)
        {
            menuAnimators[currentMenuIndex].SetTrigger("Close");
            currentMenuIndex = -1;
        }
    }
}