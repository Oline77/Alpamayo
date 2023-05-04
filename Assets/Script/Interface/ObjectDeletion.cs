using UnityEngine;

public class ObjectDeletion : MonoBehaviour
{
    public GameObject cameraEmpty; // R�f�rence � l'objet vide contenant le script de d�placement de la cam�ra

    public void DeleteSelectedObject()
    {
        // V�rifier si un objet est actuellement s�lectionn�
        if (ObjectSelection.selectedObject != null)
        {
            // Supprimer l'objet s�lectionn�
            Destroy(ObjectSelection.selectedObject);
            ObjectSelection.selectedObject = null;
            cameraEmpty.SetActive(true);
        }
    }
}
