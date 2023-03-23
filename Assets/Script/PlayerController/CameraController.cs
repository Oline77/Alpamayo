using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float sensitivity = 100f;
    [SerializeField] private float movementSpeed = 1f;
    private float xRotation = 0f;
    private float yRotation = 0f;
    private Vector2 lastTouchPosition;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        // Vérifie si l'utilisateur interagit avec l'interface utilisateur
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.GetMouseButtonDown(0))
        {
            lastTouchPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            Vector2 deltaPosition = new Vector2(Input.mousePosition.x - lastTouchPosition.x, Input.mousePosition.y - lastTouchPosition.y);
            xRotation -= deltaPosition.y / Screen.height * sensitivity;
            yRotation += deltaPosition.x / Screen.width * sensitivity;
            transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
            lastTouchPosition = Input.mousePosition;
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = transform.right * horizontal + transform.forward * vertical;
        movement.y = 0f;
        movement.Normalize();

        Vector2 movement2D = new Vector2(movement.x, movement.z);
        movement2D -= new Vector2(lastTouchPosition.x, lastTouchPosition.y) / Screen.dpi;
        movement2D.Normalize();
        movement2D *= movementSpeed * Time.deltaTime;

        transform.position += new Vector3(movement2D.x, 0f, movement2D.y);
    }
}
