using UnityEngine;
using UnityEngine.UI;

public class ObjectSelection : MonoBehaviour
{
    public static GameObject selectedObject;
    private bool isDragging = false;
    private Vector3 touchOffset;
    public GameObject cameraEmpty;
    public GameObject selectionButton;
    public GameObject rotateButton;
    public Material selectionMaterial;

    public GameObject Slider;
    public Slider scaleSlider;
    public float minScale = 0.5f;
    public float maxScale = 3f;

    private Renderer selectedRenderer;
    private Material originalMaterial;

    private Vector3 initialScale;
    private Vector3 centerPosition;


    private void Start()
    {
        GameObject[] selectableObjects = GameObject.FindGameObjectsWithTag("Selectable");

        foreach (GameObject selectableObject in selectableObjects)
        {
            initialScale = selectableObject.transform.localScale;
            centerPosition = selectableObject.transform.position;
        }
    }

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
                    if (!Physics.Raycast(ray, out hit) || hit.collider.gameObject != selectedObject)
                    {
                        DeselectObject(selectedObject);
                        selectedObject = null;
                        return;
                    }
                }

                if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Selectable"))
                {
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
        else if (selectedObject != null && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (!Physics.Raycast(ray, out hit) || hit.collider.gameObject != selectedObject)
            {
                DeselectObject(selectedObject);
                selectedObject = null;
            }
        }

    }

    private void SelectObject(GameObject obj)
    {
        cameraEmpty.SetActive(false);
        selectionButton.SetActive(true);
        rotateButton.SetActive(true);
        Slider.SetActive(true);

        // Stocker le matériau d'origine de l'objet sélectionné, sauf s'il a déjà été sélectionné
        selectedRenderer = obj.GetComponent<Renderer>();
        if (selectedRenderer != null && selectedRenderer.material != selectionMaterial)
        {
            originalMaterial = selectedRenderer.material;
            Debug.Log("Storing original material: " + originalMaterial.name);
        }

        // Appliquer le matériau de sélection à l'objet, sauf s'il a déjà été sélectionné
        if (selectedRenderer != null && selectedRenderer.material != selectionMaterial)
        {
            selectedRenderer.material = selectionMaterial;
        }
    }

    public void DeselectObject(GameObject obj)
    {
        Debug.Log("Deselecting object: " + obj.name);

        cameraEmpty.SetActive(true);
        selectionButton.SetActive(false);
        rotateButton.SetActive(false);
        Slider.SetActive(false);

        // Restaurer le matériau d'origine de l'objet désélectionné
        if (selectedRenderer != null && selectedRenderer.material != selectionMaterial)
        {
            selectedRenderer.material = originalMaterial;
        }

        // Réinitialiser les variables de sélection
        selectedObject = null;
        selectedRenderer = null;
        originalMaterial = null;
    }

    public void OnSliderChanged()
    {
        if(selectedObject != null) { 
            float newScale = Mathf.Lerp(minScale, maxScale, scaleSlider.value);
            selectedObject.transform.localScale = initialScale * newScale;
            selectedObject.transform.position = centerPosition - (selectedObject.transform.localScale - initialScale) / 2f;
        }
    }

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

    public void RotateSelectedObjects()
    {
        if (selectedObject != null)
        {
            selectedObject.transform.Rotate(0f, 20f, 0f);
        }
    }
}