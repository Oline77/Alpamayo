using UnityEngine;

public class ObjectSelection : MonoBehaviour
{
    public static GameObject selectedObject; // Variable statique pour stocker l'objet sélectionné
    private bool isDragging = false;
    private Vector3 touchOffset;
    public GameObject cameraEmpty; // Référence à l'objet vide contenant le script de déplacement de la caméra
    public GameObject selectionButton; // Référence à l'objet du bouton de sélection
   
    public Material selectionMaterial;

    private Renderer selectedRenderer;
    private Material originalMaterial;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (selectedObject != null)
                {
                    // Si l'objet est déjà sélectionné et que le raycast ne touche pas l'objet, le désélectionner
                    if (!Physics.Raycast(ray, out hit) || hit.collider.gameObject != selectedObject)
                    {
                        DeselectObject(selectedObject);
                        selectedObject = null;
                        return;
                    }
                }

                if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Selectable"))
                {
                    // Sélectionner l'objet si le raycast touche un objet avec le tag "Selectable"
                    selectedObject = hit.collider.gameObject;
                    SelectObject(selectedObject);
                    touchOffset = hit.point - selectedObject.transform.position;
                    isDragging = true;
                }
            }
            else if (touch.phase == TouchPhase.Moved && isDragging)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    selectedObject.transform.position = hit.point - touchOffset;
                }
            }
            else if (touch.phase == TouchPhase.Ended && isDragging)
            {
                isDragging = false;
            }
        }
    }

    private void SelectObject(GameObject obj)
    {
        cameraEmpty.SetActive(false);
        selectionButton.SetActive(true);

        // Stocker le matériau d'origine de l'objet sélectionné
        selectedRenderer = obj.GetComponent<Renderer>();
        if (selectedRenderer != null)
        {
            originalMaterial = selectedRenderer.material;
        }

        // Appliquer le matériau de sélection à l'objet
        if (selectedRenderer != null)
        {
            selectedRenderer.material = selectionMaterial;
        }
    }

    public void DeselectObject(GameObject obj)
    {
        cameraEmpty.SetActive(true);
        selectionButton.SetActive(false);

        // Restaurer le matériau d'origine de l'objet désélectionné
        if (selectedRenderer != null)
        {
            selectedRenderer.material = originalMaterial;
        }

        // Réinitialiser les variables de sélection
        selectedObject = null;
        selectedRenderer = null;
        originalMaterial = null;
    }
}
