using UnityEngine;

public class ObjectDeletion : MonoBehaviour
{
    public GameObject cameraEmpty; // Référence à l'objet vide contenant le script de déplacement de la caméra

    public void DeleteSelectedObject()
    {
        // Vérifier si un objet est actuellement sélectionné
        if (ObjectSelection.selectedObject != null)
        {
            // Supprimer l'objet sélectionné
            Destroy(ObjectSelection.selectedObject);
            ObjectSelection.selectedObject = null;
            cameraEmpty.SetActive(true);
        }
    }
}
