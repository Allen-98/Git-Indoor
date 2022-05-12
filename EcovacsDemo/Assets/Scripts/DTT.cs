using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DTT : MonoBehaviour
{
    [Header("Game Objects")]
    public GameObject window;
    public GameObject yellowFW;
    public GameObject blueFW;
    public GameObject blueFW2;
    public GameObject redFW;
    public GameObject redFw2;
    public GameObject dayLights;
    public GameObject nightLights;




    [Header("Materials")]
    public Material mt_window;
    public Material mt_redFW;
    public Material mt_yellowFW;
    public Material mt_blueFW;



    private float windowValue;
    private float yellowValue;
    private float blueValue;
    private float redValue;

    private float sceneColorValue;


    // Start is called before the first frame update
    void Start()
    {
        mt_window.SetFloat("_FrostIntensity", 1f);
        mt_yellowFW.SetColor("_Color", new Color(1, 1, 1, 1));
        mt_blueFW.SetColor("_Color", new Color(1, 1, 1, 1));
        mt_redFW.SetColor("_Color", new Color(1, 1, 1, 1));



        RenderSettings.ambientLight = new Color(1, 1, 1);
        sceneColorValue = 1f;

    }

    // Update is called once per frame
    void Update()
    {

        WindowRemove();
        YellowFWRemove();
        BlueFWRemove();
        RedFWRemove();
        SceneLightChange();

    }

    public void WindowRemove()
    {
        windowValue = mt_window.GetFloat("_FrostIntensity");

        if (windowValue > 0)
        {
            mt_window.SetFloat("_FrostIntensity", windowValue - 0.005f);
        }
        else
        {
            window.SetActive(false);
        }


    }

    public void YellowFWRemove()
    {
        yellowValue = mt_yellowFW.GetColor("_Color").a;

        if (yellowValue > 0)
        {
            yellowValue -= 0.005f;

            mt_yellowFW.SetColor("_Color", new Color(1, 1, 1, yellowValue));

        }
        else
        {
            yellowFW.SetActive(false);
        }


    }

    public void BlueFWRemove()
    {
        blueValue = mt_blueFW.GetColor("_Color").a;

        if (blueValue > 0)
        {
            blueValue -= 0.005f;

            mt_blueFW.SetColor("_Color", new Color(1, 1, 1, blueValue));

        }
        else
        {
            blueFW.SetActive(false);
            blueFW2.SetActive(false);
        }


    }

    public void RedFWRemove()
    {
        redValue = mt_redFW.GetColor("_Color").a;

        if (redValue > 0)
        {
            redValue -= 0.005f;

            mt_redFW.SetColor("_Color", new Color(1, 1, 1, redValue));

        }
        else
        {
            redFW.SetActive(false);
            redFw2.SetActive(false);
        }


    }

    public void SceneLightChange()
    {

        if (sceneColorValue > 0.4)
        {
            sceneColorValue -= 0.001f;
            RenderSettings.ambientLight = new Color(sceneColorValue, sceneColorValue, sceneColorValue);

        }
        else
        {
            dayLights.SetActive(false);
            nightLights.SetActive(true);
        }


    }

}
