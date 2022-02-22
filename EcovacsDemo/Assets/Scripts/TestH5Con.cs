using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TestH5Con : MonoBehaviour
{
    private UniWebView uniWebView;
    string testStr = string.Empty;
    // Start is called before the first frame update
    void Start()
    {
        uniWebView = GetComponent<UniWebView>();
        if (uniWebView == null)
        {
            uniWebView = gameObject.AddComponent<UniWebView>();
        }
        uniWebView.SetAllowFileAccess(true);
        uniWebView.SetAllowFileAccessFromFileURLs(true);

        uniWebView.Frame = new Rect(0, 0, Screen.width, Screen.height);
        uniWebView.SetShowToolbar(true);
        uniWebView.OnMessageReceived += OnRecieveMsg;
     
        var url = UniWebViewHelper.StreamingAssetURLForPath("H5Con/index2.html");
        Debug.Log(url.ToString());
        uniWebView.Load(url);
        uniWebView.Show();

        string jsonfile = Application.streamingAssetsPath + "/test2.json";//JSON文件路径
        StreamReader streamreader = new StreamReader(jsonfile);//读取数据，转换成数据流
        string all = streamreader.ReadToEnd();
        //Dictionary<string, string> jdic = JsonConvert.DeserializeObject<Dictionary<string, string>>(all);
        uniWebView.OnPageFinished += UniWebView_OnPageFinished;
        //testStr = JsonConvert.SerializeObject(jdic);
        //testStr = all;
        testStr = writetest();
    }

    private void UniWebView_OnPageFinished(UniWebView webView, int statusCode, string url)
    {
        Debug.LogError("StartEvaluate:" + Time.realtimeSinceStartup);
        uniWebView.EvaluateJavaScript(@"h5Callback(" + testStr + ")", (payload) => {
            Debug.LogError("payload:" + Time.realtimeSinceStartup);
            Debug.Log(payload.data);
        });
    }

    public void OnRecieveMsg(UniWebView uniWebView,UniWebViewMessage uniWebViewMessage)
    {
        Debug.LogError("OnRecieveMsg:" + Time.realtimeSinceStartup);
        Debug.Log("Path:   " + uniWebViewMessage.Path);
        Debug.Log("arg:   key:" + "param2   "+ "   value:" + uniWebViewMessage.Args["param2"]);

        
    }
    private int testNum = 1000;
    public string writetest()
    {
        StringWriter sw = new StringWriter();
        JsonWriter JsonWriter = new JsonTextWriter(sw);
        JsonWriter.WriteStartObject();
        int index = 0;
        while (index < testNum)
        {

            JsonWriter.WritePropertyName("header" + index);
            JsonWriter.WriteStartObject();
            for (int i = 0; i < testNum; i++)
            {
                JsonWriter.WritePropertyName(index.ToString());
                JsonWriter.WriteValue(index.ToString());
            }
            JsonWriter.WriteEndObject();
       
            JsonWriter.WritePropertyName("body" + index);
            JsonWriter.WriteStartObject();
            for (int i = 0; i < testNum; i++)
            {
                JsonWriter.WritePropertyName(index.ToString());
                JsonWriter.WriteValue(index.ToString());
            }
            JsonWriter.WriteEndObject();
       

            JsonWriter.WritePropertyName("result" + index);
            JsonWriter.WriteStartObject();
            for (int i = 0; i < testNum; i++)
            {
                JsonWriter.WritePropertyName(index.ToString());
                JsonWriter.WriteValue(index.ToString());
            }
            JsonWriter.WriteEndObject();
            index++;
        }
        JsonWriter.WriteEndObject();
        JsonWriter.Flush();
        return sw.GetStringBuilder().ToString();
    }
}
