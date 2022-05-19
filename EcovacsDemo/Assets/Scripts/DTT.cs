using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using AmazingAssets.AdvancedDissolve;

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
    public GameObject airbot;
    public GameObject blanket;
    public GameObject socks;
    public GameObject shoes;
    public AdvancedDissolveGeometricCutoutController circle;
    public GameObject boom;
    public GameObject pivot;
    public GameObject indoor;
    public GameObject deebot;
    public GameObject ship;
    

    public Light[] lights;


    [Header("Materials")]
    public Material mt_window;
    public Material mt_redFW;
    public Material mt_yellowFW;
    public Material mt_blueFW;
    public Material mt_blanket;
    public Material mt_shoes;
    public Material mt_socks;
    public Material mt_airbot_main;
    public Material mt_airbot_top;
    public Material mt_airbot_text;
    public Material mt_deebot_main;
    public Material mt_deebot_text;

    private float removeValue;
    private float airbotValue;
    private float boomValue;
    private float deebotScaleValue;
    private float deebotValue;
    private float sceneColorValue;
    private float lightIntensity;
    private float radius;

    private bool over;
    private bool over1;

    private float pivotY;


    // Start is called before the first frame update
    void Start()
    {
        StartSettings();

    }

    // Update is called once per frame
    void Update()
    {
        SceneLightChange();
        
        if (over)
        {
            ThingsRemove();
        }



        if (over1)
        {
            if (airbot.transform.position.x < 0.6)
            {
                airbot.transform.Translate(Vector3.right * Time.deltaTime);

            }
            else
            {
                radius = circle.target1Radius;

                if(pivotY > -10)
                {
                    pivotY -= 0.005f;
                    pivot.transform.localPosition = new Vector3(0, pivotY, 0);
                }
                else
                {
                    pivot.SetActive(false);
                }


                if (radius < 7)
                {
                    radius += 0.005f;
                    circle.target1Radius = radius;

                    if (airbotValue < 1)
                    {
                        airbotValue += 0.001f;
                        mt_airbot_top.SetFloat("_AdvancedDissolveCutoutStandardClip", airbotValue);
                        mt_airbot_main.SetFloat("_AdvancedDissolveCutoutStandardClip", airbotValue);
                        mt_airbot_text.SetFloat("_AdvancedDissolveCutoutStandardClip", airbotValue);
                    }else
                    {
                        airbot.SetActive(false);
                    }

                    if (boomValue < 100)
                    {
                        boomValue += 0.8f;
                        boom.transform.localScale = new Vector3(boomValue, boomValue, boomValue);
                    }
                    else
                    {
                        boom.SetActive(false);
                    }

                    if (deebotScaleValue < 1)
                    {
                        deebotScaleValue += 0.008f;
                        deebot.transform.localScale = new Vector3(deebotScaleValue, deebotScaleValue, deebotScaleValue);

                    }
                    else
                    {
                        if (deebotValue < 1)
                        {
                            deebotValue += 0.001f;
                            mt_deebot_main.SetFloat("_AdvancedDissolveCutoutStandardClip", deebotValue);
                            mt_deebot_text.SetFloat("_AdvancedDissolveCutoutStandardClip", deebotValue);
                        }
                        else
                        {
                            deebot.SetActive(false);
                        }
                    }


                } else if (radius >= 7 && radius < 380)
                {
                    radius += 5f;
                    circle.target1Radius = radius;
                }
                else
                {
                    circle.gameObject.SetActive(false);
                    indoor.SetActive(false);
                }
            }
        }

    }

    public void StartSettings()
    {
        over = false;
        over1 = false;

        pivotY = 3.8f;
        pivot.transform.localPosition = new Vector3(0, pivotY, 0);

        airbotValue = 0;
        mt_airbot_main.SetFloat("_AdvancedDissolveCutoutStandardClip", airbotValue);
        mt_airbot_text.SetFloat("_AdvancedDissolveCutoutStandardClip", airbotValue);
        mt_airbot_top.SetFloat("_AdvancedDissolveCutoutStandardClip", airbotValue);

        mt_window.SetFloat("_FrostIntensity", 1f);
        mt_yellowFW.SetColor("_Color", new Color(1, 1, 1, 1));
        mt_blueFW.SetColor("_Color", new Color(1, 1, 1, 1));
        mt_redFW.SetColor("_Color", new Color(1, 1, 1, 1));

        foreach (Light i in lights)
        {
            i.intensity = 0;
        }
        lightIntensity = 0;

        RenderSettings.ambientLight = new Color(1, 1, 1);
        sceneColorValue = 1f;

        airbot.transform.position = new Vector3(-1.5f, 0.03f, 0.3f);

        circle.target1Radius = 0;

        boomValue = 0;
        boom.transform.localScale = new Vector3(0, 0, 0);

        deebotScaleValue = 0.1f;
        deebot.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        deebotValue = 0f;
        mt_deebot_main.SetFloat("_AdvancedDissolveCutoutStandardClip", deebotValue);
        mt_deebot_text.SetFloat("_AdvancedDissolveCutoutStandardClip", deebotValue);



    }

    public void ThingsRemove()
    {
        removeValue = mt_window.GetFloat("_FrostIntensity");

        if (removeValue > 0)
        {
            removeValue -= 0.005f;

            mt_window.SetFloat("_FrostIntensity", removeValue);
            mt_yellowFW.SetColor("_Color", new Color(1, 1, 1, removeValue));
            mt_blueFW.SetColor("_Color", new Color(1, 1, 1, removeValue));
            mt_redFW.SetColor("_Color", new Color(1, 1, 1, removeValue));

        }
        else
        {
            window.SetActive(false);
            yellowFW.SetActive(false);
            blueFW.SetActive(false);
            blueFW2.SetActive(false);
            redFW.SetActive(false);
            redFw2.SetActive(false);
            over1 = true;
        }

    }


    public void SceneLightChange()
    {
        dayLights.SetActive(false);

        if (sceneColorValue > 0.4)
        {
            sceneColorValue -= 0.005f;
            RenderSettings.ambientLight = new Color(sceneColorValue, sceneColorValue, sceneColorValue);

        } 
        else
        {
            dayLights.SetActive(false);

            if (lightIntensity < 2.4)
            {
                lightIntensity += 0.01f;
                foreach (Light i in lights)
                { 
                    i.intensity = lightIntensity;
                }

            }
            else
            {
                over = true;
            }
        } 


    }


}
