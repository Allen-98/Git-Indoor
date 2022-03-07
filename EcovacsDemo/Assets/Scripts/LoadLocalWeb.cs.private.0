using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadLocalWeb : MonoBehaviour
{
    public Button button;

    public List<GameObject> Objs;

    private UniWebView uniWebView;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("ssss");
        button = GameObject.Find("H5 Button").GetComponent<Button>();
        button.onClick.AddListener(OnClickOpenWeb);
        //  AIBtn.onClick.AddListener(OnClickAI);

    }

    public void OnClickOpenWeb()
    {
        uniWebView = GetComponent<UniWebView>();
        if (uniWebView == null)
        {
            uniWebView = gameObject.AddComponent<UniWebView>();
        }
        uniWebView.SetAllowFileAccess(true);
        uniWebView.SetAllowFileAccessFromFileURLs(true);

        uniWebView.Frame = new Rect(0, 0, Screen.width, Screen.height);
        uniWebView.OnPageErrorReceived += OnError;
        uniWebView.SetShowToolbar(true);
        uniWebView.OnPageStarted += (view, url) => { Debug.Log("url started!" + url.ToString()); };
        uniWebView.OnShouldClose += (view) => {
            view = null;
            foreach (var item in Objs)
            {
                item.SetActive(true);

            }
            return true;
        };
        uniWebView.OnPageFinished += (view, statuscode, url) => {
            Debug.Log("url finished!" + url.ToString());
            Debug.Log("statuscode !             " + statuscode.ToString());

            foreach (var item in Objs)
            {
                item.SetActive(false);

            }
        };
        var url = UniWebViewHelper.StreamingAssetURLForPath("dist/index.html");
        Debug.Log(url.ToString());
        uniWebView.Load(url, false);
        uniWebView.Show();
    }

    public void OnError(UniWebView webView, int errorCode, string errorMessage)
    {
        Debug.LogError("errorCode:" + errorCode);
        Debug.LogError("errorMessage:" + errorMessage);
    }
}
