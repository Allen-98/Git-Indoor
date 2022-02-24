using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


public class SceneChanges : MonoBehaviour
{
    public GameObject dayLights;
    public GameObject nightLights;

    public GameObject dayFloor;
    public GameObject nightFloor;

    public bool isDay;


    // Start is called before the first frame update
    void Start()
    {
        isDay = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SceneChange()
    {
        if (isDay)
        {
            isDay = false;
            dayFloor.SetActive(false);
            dayLights.SetActive(false);
            nightFloor.SetActive(true);
            nightLights.SetActive(true);

            RenderSettings.ambientLight = new Color(102/255f, 100 / 255f, 99 / 255f);
        }
        else
        {
            isDay = true;
            dayFloor.SetActive(true);
            dayLights.SetActive(true);
            nightFloor.SetActive(false);
            nightLights.SetActive(false);

            RenderSettings.ambientLight = new Color(1, 1, 1);
        }
    }



}
