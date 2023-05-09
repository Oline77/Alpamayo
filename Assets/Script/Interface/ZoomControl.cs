using UnityEngine;
using UnityEngine.EventSystems;

public class ZoomControl : MonoBehaviour
{
    public Zoom moveCameraScript; // Référence au script MoveCamera
    public GameObject uiObject; // Référence à votre interface
    private bool isMoveCameraEnabled = true; // Variable booléenne pour savoir si le script MoveCamera est activé ou désactivé

    void Update()
    {
        // Vérifier si la souris est sur l'interface
        if (EventSystem.current.IsPointerOverGameObject(0))
        {
            Debug.Log("UI is active. Disabling MoveCamera script.");
            if (isMoveCameraEnabled)
            {
                moveCameraScript.enabled = false; // Désactiver le script MoveCamera
                isMoveCameraEnabled = false; // Mettre la variable booléenne à false
            }
        }
        else
        {
            Debug.Log("UI is inactive. Enabling MoveCamera script.");
            if (!isMoveCameraEnabled)
            {
                moveCameraScript.enabled = true; // Activer le script MoveCamera
                isMoveCameraEnabled = true; // Mettre la variable booléenne à true
            }
        }
    }
}
