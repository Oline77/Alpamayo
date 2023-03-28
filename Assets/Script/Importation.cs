using UnityEngine;
using UnityEditor;
using System.IO;

public class Importation : MonoBehaviour
{
    public void ImportAndConvert()
    {
        string filePath = EditorUtility.OpenFilePanel("Import FBX", "", "fbx");

        if (filePath.Length != 0)
        {
            // Convert the FBX file to an AssetBundle
            string assetBundlePath = Path.Combine(Application.streamingAssetsPath, Path.GetFileNameWithoutExtension(filePath) + ".assetbundle");
            BuildPipeline.BuildAssetBundle(null, new[] { AssetDatabase.LoadAssetAtPath<GameObject>(filePath) }, assetBundlePath, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);

            // Load the AssetBundle
            AssetBundle assetBundle = AssetBundle.LoadFromFile(assetBundlePath);
            GameObject prefab = assetBundle.LoadAsset<GameObject>(Path.GetFileNameWithoutExtension(filePath));

            // Instantiate the prefab
            Instantiate(prefab);

            // Unload the AssetBundle
            assetBundle.Unload(false);
        }
    }
}
