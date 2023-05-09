using UnityEngine;
using UnityEngine.EventSystems;

public class ZoomControl : MonoBehaviour
{
    public Zoom moveCameraScript; // R�f�rence au script MoveCamera
    public GameObject uiObject; // R�f�rence � votre interface
    private bool isMoveCameraEnabled = true; // Variable bool�enne pour savoir si le script MoveCamera est activ� ou d�sactiv�

    void Update()
    {
        // V�rifier si la souris est sur l'interface
        if (EventSystem.current.IsPointerOverGameObject(0))
        {
            Debug.Log("UI is active. Disabling MoveCamera script.");
            if (isMoveCameraEnabled)
            {
                moveCameraScript.enabled = false; // D�sactiver le script MoveCamera
                isMoveCameraEnabled = false; // Mettre la variable bool�enne � false
            }
        }
        else
        {
            Debug.Log("UI is inactive. Enabling MoveCamera script.");
            if (!isMoveCameraEnabled)
            {
                moveCameraScript.enabled = true; // Activer le script MoveCamera
                isMoveCameraEnabled = true; // Mettre la variable bool�enne � true
            }
        }
    }
}
