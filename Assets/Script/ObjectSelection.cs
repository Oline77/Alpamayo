using UnityEngine;

public class ObjectSelection : MonoBehaviour
{
    // Tag utilisé pour identifier les objets sélectionnables
    public string selectableTag = "Selectable";

    // Couleur de l'objet lorsqu'il est sélectionné
    public Color highlightColor = Color.yellow;

    // Couleur par défaut de l'objet
    private Color defaultColor;

    // Stocke l'objet actuellement sélectionné
    private Transform selectedObject;

    public Zoom cameraZoom; // Référence au script Zoom de la caméra

    void Start()
    {
        cameraZoom = Camera.main.GetComponent<Zoom>();
        // Récupère la couleur par défaut de l'objet
        defaultColor = GetComponent<Renderer>().material.color;
    }

    void Update()
    {
        // Vérifie si un toucher est détecté
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            // Vérifie si l'utilisateur a appuyé sur l'écran
            if (touch.phase == TouchPhase.Began)
            {
                // Cast un rayon à partir de la caméra vers la position du toucher pour détecter les objets sélectionnables
                RaycastHit hit;
                if (Physics.Raycast(touchPosition, Camera.main.transform.forward, out hit))
                {
                    // Vérifie si l'objet détecté a le tag "selectable"
                    if (hit.collider.CompareTag(selectableTag))
                    {
                        // Si un objet était déjà sélectionné, le désélectionne
                        if (selectedObject != null && selectedObject != hit.transform)
                        {
                            // Remettre l'objet précédemment sélectionné à sa couleur par défaut
                            selectedObject.GetComponent<Renderer>().material.color = defaultColor;
                        }

                        // Stocke l'objet sélectionné
                        selectedObject = hit.transform;

                        // Change la couleur de l'objet sélectionné
                        selectedObject.GetComponent<Renderer>().material.color = highlightColor;
                        // Désactiver le script Zoom de la caméra
                        cameraZoom.enabled = false;
                    }
                }
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                // Vérifie si un objet est sélectionné
                if (selectedObject != null)
                {
                    // Vérifie si le toucher se produit directement sur l'objet sélectionné
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit) && hit.transform == selectedObject)
                    {
                        // Convertit la position de l'écran en rayon à partir de la caméra
                        ray = Camera.main.ScreenPointToRay(touch.position);

                        // Calcule la distance le long du rayon où l'objet doit être déplacé
                        float distanceAlongRay = (selectedObject.position - ray.origin).magnitude;

                        // Calcule la nouvelle position en utilisant le rayon et la distance calculée
                        Vector3 newPosition = ray.GetPoint(distanceAlongRay);

                        // Déplace l'objet à la nouvelle position
                        selectedObject.position = newPosition;
                        selectedObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    }
                }
            }

            else if (touch.phase == TouchPhase.Ended)
            {
                // Désélectionne l'objet lorsque le toucher se termine
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