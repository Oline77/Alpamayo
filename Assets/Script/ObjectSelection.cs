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

    // Vitesse de déplacement de l'objet
    public float moveSpeed = 10f;

    private void Awake()
    {
        // Récupère la couleur par défaut de l'objet
        defaultColor = GetComponent<Renderer>().material.color;
    }

    private void Update()
    {
        // Vérifie si un toucher est détecté
        if (Input.touchCount > 0)
        {
            // Obtient la position du toucher sur l'écran
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
                        if (selectedObject != null)
                        {
                            DeselectObject();
                        }

                        // Stocke l'objet sélectionné
                        selectedObject = hit.transform;

                        // Change la couleur de l'objet sélectionné
                        selectedObject.GetComponent<Renderer>().material.color = highlightColor;
                    }
                }
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                // Vérifie si un objet est sélectionné
                if (selectedObject != null)
                {
                    // Obtient la distance de l'objet à la caméra
                    float distance = Vector3.Distance(Camera.main.transform.position, selectedObject.position);

                    // Calcul la position à laquelle l'objet doit être déplacé
                    Vector3 newPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, distance));

                    // Déplace l'objet à la nouvelle position
                    selectedObject.position = Vector3.Lerp(selectedObject.position, newPosition, Time.deltaTime * moveSpeed);
                    selectedObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                }
            }
        }
    }


    // Désélectionne l'objet actuellement sélectionné
    private void DeselectObject()
    {
        selectedObject.GetComponent<Renderer>().material.color = defaultColor;
        selectedObject = null;
    }
}
