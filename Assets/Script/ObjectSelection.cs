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

    // Vitesse de d�placement de l'objet
    public float moveSpeed = 10f;

    private void Awake()
    {
        // R�cup�re la couleur par d�faut de l'objet
        defaultColor = GetComponent<Renderer>().material.color;
    }

    private void Update()
    {
        // V�rifie si un toucher est d�tect�
        if (Input.touchCount > 0)
        {
            // Obtient la position du toucher sur l'�cran
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
                        if (selectedObject != null)
                        {
                            DeselectObject();
                        }

                        // Stocke l'objet s�lectionn�
                        selectedObject = hit.transform;

                        // Change la couleur de l'objet s�lectionn�
                        selectedObject.GetComponent<Renderer>().material.color = highlightColor;
                    }
                }
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                // V�rifie si un objet est s�lectionn�
                if (selectedObject != null)
                {
                    // Obtient la distance de l'objet � la cam�ra
                    float distance = Vector3.Distance(Camera.main.transform.position, selectedObject.position);

                    // Calcul la position � laquelle l'objet doit �tre d�plac�
                    Vector3 newPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, distance));

                    // D�place l'objet � la nouvelle position
                    selectedObject.position = Vector3.Lerp(selectedObject.position, newPosition, Time.deltaTime * moveSpeed);
                    selectedObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                }
            }
        }
    }


    // D�s�lectionne l'objet actuellement s�lectionn�
    private void DeselectObject()
    {
        selectedObject.GetComponent<Renderer>().material.color = defaultColor;
        selectedObject = null;
    }
}
