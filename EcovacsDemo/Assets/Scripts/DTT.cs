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
    public Light[] lights;
    public GameObject pivot;
    public GameObject airbot;
    public GameObject blanket;
    public GameObject socks;
    public GameObject shoes;


    [Header("Materials")]
    public Material mt_window;
    public Material mt_redFW;
    public Material mt_yellowFW;
    public Material mt_blueFW;
    public Material mt_blanket;



    private float windowValue;
    private float yellowValue;
    private float blueValue;
    private float redValue;
    private float blanketValue;

    private float sceneColorValue;
    private float lightIntensity;
    private float pivotValue;

    private bool over;


    // Start is called before the first frame update
    void Start()
    {
        StartSettings();

    }

    // Update is called once per frame
    void Update()
    {

        SceneChange1();

        if (over)
        {
            if (airbot.transform.position.x < 0.6)
            {
                airbot.transform.Translate(Vector3.right * Time.deltaTime);

            }
        }

    }

    public void StartSettings()
    {
        over = false;

        mt_window.SetFloat("_FrostIntensity", 1f);
        mt_yellowFW.SetColor("_Color", new Color(1, 1, 1, 1));
        mt_blueFW.SetColor("_Color", new Color(1, 1, 1, 1));
        mt_redFW.SetColor("_Color", new Color(1, 1, 1, 1));
        mt_blanket.SetFloat("_AdvancedDissolveCutoutStandardClip", 0f);

        foreach (Light i in lights)
        {
            i.intensity = 0;
        }
        lightIntensity = 0;

        RenderSettings.ambientLight = new Color(1, 1, 1);
        sceneColorValue = 1f;

        pivot.transform.position = new Vector3(0, 3.3f, 0);
        pivotValue = pivot.transform.position.y;

        airbot.transform.position = new Vector3(-1.5f, 0.03f, 0.3f);
    }

    public void WindowRemove()
    {
        windowValue = mt_window.GetFloat("_FrostIntensity");

        if (windowValue > 0)
        {
            mt_window.SetFloat("_FrostIntensity", windowValue - 0.003f);
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
            yellowValue -= 0.003f;

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
            blueValue -= 0.003f;

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
            redValue -= 0.003f;

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
            sceneColorValue -= 0.002f;
            RenderSettings.ambientLight = new Color(sceneColorValue, sceneColorValue, sceneColorValue);

        } 
        else if (sceneColorValue < 0.99)
        {
            dayLights.SetActive(false);
            nightLights.SetActive(true);

            if (lightIntensity < 2.25)
            {
                lightIntensity += 0.005f;
                foreach (Light i in lights)
                {
                    i.intensity = lightIntensity;
                }

            }

            if (pivotValue > 0)
            {
                pivot.transform.position = new Vector3(0, pivotValue -= 0.01f, 0);
            }
            else
            {
                over = true;
            }
        } 


    }

    public void BlanketRemove()
    {
        blanketValue = mt_blanket.GetFloat("_AdvancedDissolveCutoutStandardClip");

        if (blanketValue < 1)
        {
            blanketValue += 0.003f;
            mt_blanket.SetFloat("_AdvancedDissolveCutoutStandardClip", blanketValue);
        }
        else
        {
            blanket.SetActive(false);
            shoes.SetActive(false);
            socks.SetActive(false);
        }
    }

    public void SceneChange1()
    {
        WindowRemove();
        YellowFWRemove();
        BlueFWRemove();
        RedFWRemove();
        BlanketRemove();
        SceneLightChange();
    }

}
