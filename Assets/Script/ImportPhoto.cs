using UnityEngine;
using UnityEngine.UI;

public class ImportPhoto : MonoBehaviour
{
    public GameObject planObject; // objet plan de votre sc�ne
    public Renderer planRenderer; // renderer du plan
    public Texture2D defaultTexture; // texture par d�faut si aucune image n'est s�lectionn�e
    public float scale = 1f; // �chelle de l'image import�e

    public void ChoosePhoto()
    {
        // Ouvre la galerie de photos
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        {
            if (path != null)
            {
                // Charge la photo en tant que texture
                Texture2D texture = NativeGallery.LoadImageAtPath(path);

                // Met � jour la texture du plan avec la nouvelle texture import�e
                Material planMaterial = planRenderer.material;
                planMaterial.mainTexture = texture;
                planMaterial.mainTextureScale = new Vector2(scale, scale);
            }
        }, "Choisir une image");

        Debug.Log("Permission result: " + permission);
    }

    private void Awake()
    {
        // V�rifie si le composant Renderer est attach� � l'objet Plan
        /*if (planObject != null)
        {
            planRenderer = planObject.GetComponent<Renderer>();
        }*/
    }

    private void Start()
    {
        // Initialise la texture par d�faut
        /*if (defaultTexture == null)
        {
            defaultTexture = new Texture2D(12, 12);
            defaultTexture.SetPixel(0, 0, Color.gray);
            defaultTexture.SetPixel(0, 1, Color.gray);
            defaultTexture.SetPixel(1, 0, Color.gray);
            defaultTexture.SetPixel(1, 1, Color.gray);
            defaultTexture.Apply();
        }*/

        // Initialise la texture du plan
        if (planRenderer != null)
        {
            Material planMaterial = planRenderer.material;
            planMaterial.mainTexture = defaultTexture;
            planMaterial.mainTextureScale = new Vector2(scale, scale);
        }
    }
}
