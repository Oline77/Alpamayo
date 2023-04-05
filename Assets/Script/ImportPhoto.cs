using UnityEngine;
using UnityEngine.UI;

public class ImportPhoto : MonoBehaviour
{
    public GameObject planObject; // objet plan de votre scène
    public Renderer planRenderer; // renderer du plan
    public Texture2D defaultTexture; // texture par défaut si aucune image n'est sélectionnée
    public float scale = 1f; // échelle de l'image importée

    public void ChoosePhoto()
    {
        // Ouvre la galerie de photos
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        {
            if (path != null)
            {
                // Charge la photo en tant que texture
                Texture2D texture = NativeGallery.LoadImageAtPath(path);

                // Met à jour la texture du plan avec la nouvelle texture importée
                Material planMaterial = planRenderer.material;
                planMaterial.mainTexture = texture;
                planMaterial.mainTextureScale = new Vector2(scale, scale);
            }
        }, "Choisir une image");

        Debug.Log("Permission result: " + permission);
    }

    private void Awake()
    {
        // Vérifie si le composant Renderer est attaché à l'objet Plan
        /*if (planObject != null)
        {
            planRenderer = planObject.GetComponent<Renderer>();
        }*/
    }

    private void Start()
    {
        // Initialise la texture par défaut
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
