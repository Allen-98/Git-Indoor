using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SceneColorChange : MonoBehaviour
{
    public GameObject[] lights;
    public Color color;

    // Start is called before the first frame update
    void Start()
    {
        //RenderSettings.ambientLight = color;
        foreach (GameObject light in lights)
        {
            light.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        RenderSettings.ambientLight = color;
    }
}
