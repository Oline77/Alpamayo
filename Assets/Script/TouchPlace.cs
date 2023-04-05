using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.EventSystems;
public class TouchPlace : MonoBehaviour
{
    public UnityEngine.UI.Button loadButton;
    private AssetBundle bundle;
    private GameObject go;
    public void OnLoadButtonClick()
    {
        StartCoroutine(DownloadAssetBundleFromServer());
    }

    private IEnumerator DownloadAssetBundleFromServer()
    {
        string url = "http://localhost/projet/arbre";
        using (UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(url))
        {
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogWarning("Error on the get request at : " + url + " " + www.error);
            }
            else
            {
                bundle = DownloadHandlerAssetBundle.GetContent(www);
                go = bundle.LoadAsset(bundle.GetAllAssetNames()[0]) as GameObject;
                bundle.Unload(false);
                yield return new WaitForEndOfFrame();
            }
            www.Dispose();
        }
    }

    void Update()
    {
    
        if (Input.GetMouseButtonDown(0) && go != null && !EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Instantiate(go, hit.point, Quaternion.Euler(-90, 0, 0));
                go = null;
            }
        }
    }
}
