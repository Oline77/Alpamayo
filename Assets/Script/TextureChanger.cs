using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextureChanger : MonoBehaviour
{
    public TMP_Text buttonText;

    // Variables for tracking texture mode
    private int currentMode = 0;
    private string[] modeNames = {"Dégradé", "Moyen", "Élevé"};

    public void ChangeTextureMode()
    {
        // Increment texture mode and wrap around
        currentMode = (currentMode + 1) % modeNames.Length;

        // Update button text with new texture mode name
        buttonText.text = "Mode de texture : " + modeNames[currentMode];

        // Call function to change texture mode in your scene
        ChangeTextureModeInScene(currentMode);
    }

    private void ChangeTextureModeInScene(int mode)
    {
        // Implement the code to change texture mode in your scene based on the selected mode
        // This could involve changing the resolution, compression, or quality of the textures
    }
}
