using UnityEngine;

public class LockYPosition : MonoBehaviour
{
    public Transform target; // l'objet fixe dans le monde

    private void LateUpdate()
    {
        Vector3 position = transform.position; // position actuelle de l'enfant
        position.y = target.position.y; // verrouille la position Y par rapport � l'objet fixe dans le monde
        transform.position = position; // met � jour la position de l'enfant
    }
}
