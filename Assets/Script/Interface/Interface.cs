using UnityEngine;
using UnityEngine.UI;

public class Interface : MonoBehaviour
{
    public GameObject[] objectsToToggle;

    public void ToggleObjects()
    {
        foreach (GameObject obj in objectsToToggle)
        {
            obj.SetActive(!obj.activeSelf);
        }
    }
}