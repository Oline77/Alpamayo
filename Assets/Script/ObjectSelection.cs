using UnityEngine;

public class ObjectSelection : MonoBehaviour
{
    // Tag utilis� pour identifier les objets s�lectionnables
    public string selectableTag = "Selectable";

    // Couleur de l'objet lorsqu'il est s�lectionn�
    public Color highlightColor = Color.yellow;

    // Couleur par d�faut de l'objet
    private Color defaultColor;

    // Stocke l'objet actuellement s�lectionn�
    private Transform selectedObject;

    public Zoom cameraZoom; // R�f�rence au script Zoom de la cam�ra

    void Start()
    {
        cameraZoom = Camera.main.GetComponent<Zoom>();
        // R�cup�re la couleur par d�faut de l'objet
        defaultColor = GetComponent<Renderer>().material.color;
    }

    void Update()
    {
        // V�rifie si un toucher est d�tect�
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            // V�rifie si l'utilisateur a appuy� sur l'�cran
            if (touch.phase == TouchPhase.Began)
            {
                // Cast un rayon � partir de la cam�ra vers la position du toucher pour d�tecter les objets s�lectionnables
                RaycastHit hit;
                if (Physics.Raycast(touchPosition, Camera.main.transform.forward, out hit))
                {
                    // V�rifie si l'objet d�tect� a le tag "selectable"
                    if (hit.collider.CompareTag(selectableTag))
                    {
                        // Si un objet �tait d�j� s�lectionn�, le d�s�lectionne
                        if (selectedObject != null && selectedObject != hit.transform)
                        {
                            // Remettre l'objet pr�c�demment s�lectionn� � sa couleur par d�faut
                            selectedObject.GetComponent<Renderer>().material.color = defaultColor;
                        }

                        // Stocke l'objet s�lectionn�
                        selectedObject = hit.transform;

                        // Change la couleur de l'objet s�lectionn�
                        selectedObject.GetComponent<Renderer>().material.color = highlightColor;
                        // D�sactiver le script Zoom de la cam�ra
                        cameraZoom.enabled = false;
                    }
                }
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                // V�rifie si un objet est s�lectionn�
                if (selectedObject != null)
                {
                    // V�rifie si le toucher se produit directement sur l'objet s�lectionn�
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit) && hit.transform == selectedObject)
                    {
                        // Convertit la position de l'�cran en rayon � partir de la cam�ra
                        ray = Camera.main.ScreenPointToRay(touch.position);

                        // Calcule la distance le long du rayon o� l'objet doit �tre d�plac�
                        float distanceAlongRay = (selectedObject.position - ray.origin).magnitude;

                        // Calcule la nouvelle position en utilisant le rayon et la distance calcul�e
                        Vector3 newPosition = ray.GetPoint(distanceAlongRay);

                        // D�place l'objet � la nouvelle position
                        selectedObject.position = newPosition;
                        selectedObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    }
                }
            }

            else if (touch.phase == TouchPhase.Ended)
            {
                // D�s�lectionne l'objet lorsque le toucher se termine
                if (selectedObject != null)
                {
                    selectedObject.GetComponent<Renderer>().material.color = defaultColor;
                    selectedObject = null;
                    cameraZoom.enabled = true;
                }
            }
        }
    }
}