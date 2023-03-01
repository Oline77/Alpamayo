using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
public class DownloadAssetBundle : MonoBehaviour
{
    // Start is called before the first frame updat
    public UnityEngine.UI.Button loadButton;
    public void OnLoadButtonClick()
    {
        StartCoroutine(DownloadAssetBundleFromServer());
    }
    private IEnumerator DownloadAssetBundleFromServer()
    {
        GameObject go = null;
        string url = "http://localhost/projet/arbre";
        using (UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(url))
        {
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogWarning("Errro on the get request at : " + url + " " + www.error);
            }
            else
            {
                AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
                go = bundle.LoadAsset(bundle.GetAllAssetNames()[0]) as GameObject;
                bundle.Unload(false);
                yield return new WaitForEndOfFrame();
            }
            www.Dispose();
        }
        InstantiateGameObjectFromAssetBundle(go);
    }

    private void InstantiateGameObjectFromAssetBundle(GameObject go)
    {
        if (go != null)
        {
            GameObject instanceGo = Instantiate(go);
            instanceGo.transform.position = Vector3.zero;
        }
        else
        {
            Debug.LogWarning("Your asset budnle is null");
        }
    }
}
           