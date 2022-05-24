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
    public GameObject gameLight;
    public Light[] lights;

    public GameObject airbot;
    public GameObject deebot;

    public GameObject blanket;
    public GameObject socks;
    public GameObject shoes;

    public GameObject boom;
    public GameObject pivot;
    public GameObject indoor;

    public GameObject player;
    public GameObject ship;
    public GameObject tailFire;

    public GameObject sceneCamera;
    public GameObject playerCamera;
    public GameObject asteroidsList;

    public Transform firePos; 
    

    public AdvancedDissolveGeometricCutoutController circle;


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
    public Material mt_ship;

    private float removeValue;
    private float airbotValue;
    private float boomValue;
    private float deebotScaleValue;
    private float deebotValue;
    private float deebotPosValue;
    private float sceneColorValue;
    private float lightIntensity;
    private float radius;
    private float shipValue;
    private float pivotY;

    private int step = 0;
        
    // Start is called before the first frame update
    void Start()
    {
        StartSettings();

    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (step == 0)
        {
            SceneLightChange();
            
        }

        if (step == 1)
        {
            ThingsRemove();
        }

        if (step == 2)
        {
            WallRemove();
        }

        if (step == 3)
        {
            AirbotRemove();
        }



    }

    public void StartSettings()
    {
        // open at start
        sceneCamera.SetActive(true);
        dayLights.SetActive(true);
        indoor.SetActive(true);
        deebot.SetActive(true);
        airbot.SetActive(true);

        // close at start
        nightLights.SetActive(false);
        gameLight.SetActive(false);
        player.SetActive(false);
        ship.SetActive(false);
        playerCamera.SetActive(false);
        tailFire.SetActive(false);
        asteroidsList.SetActive(false);

        // numbers setting 

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
        deebotPosValue = 2.4f;
        mt_deebot_main.SetFloat("_AdvancedDissolveCutoutStandardClip", deebotValue);
        mt_deebot_text.SetFloat("_AdvancedDissolveCutoutStandardClip", deebotValue);

        shipValue = 1f;
        mt_ship.SetFloat("_AdvancedDissolveCutoutStandardClip", shipValue);

        step = 0;


    }

    public void ThingsRemove()
    {
        removeValue = mt_window.GetFloat("_FrostIntensity");

        if (removeValue > 0)
        {
            removeValue -= 0.01f;

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
            step = 2;
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
            nightLights.SetActive(true);

            if (lightIntensity < 2.6f)
            {
                lightIntensity += 0.03f;
                foreach (Light i in lights)
                { 
                    i.intensity = lightIntensity;
                }

            }
            else
            {
                step = 1;
            }

        } 


    }

    public void WallRemove()
    {
        if (pivotY > -1)
        {
            pivotY -= 0.02f;
            pivot.transform.localPosition = new Vector3(0, pivotY, 0);
        }
        else
        {
            pivot.SetActive(false);
            step = 3;
        }
    }

    public void AirbotRemove()
    {
        if (airbot.transform.position.x < 0.6)
        {
            airbot.transform.Translate(Vector3.right * Time.deltaTime);

        }
        else
        {

            if (boomValue < 100)
            {
                boomValue += 0.8f;
                boom.transform.localScale = new Vector3(boomValue, boomValue, boomValue);
            }
            else
            {
                boom.SetActive(false);
            }

            if (airbotValue < 1)
            {
                airbotValue += 0.01f;
                mt_airbot_top.SetFloat("_AdvancedDissolveCutoutStandardClip", airbotValue);
                mt_airbot_main.SetFloat("_AdvancedDissolveCutoutStandardClip", airbotValue);
                mt_airbot_text.SetFloat("_AdvancedDissolveCutoutStandardClip", airbotValue);
            }
            else
            {
                airbot.SetActive(false);
            }

            IndoorRemove();
            DeebotRemove();

        }
    }

    public void IndoorRemove()
    {
        radius = circle.target1Radius;

        if (radius < 380)
        {

            if (radius < 8)
            {
                radius += 0.01f;
                circle.target1Radius = radius;
            } 
            else
            {
                radius += 5f;
                circle.target1Radius = radius;
            }

        }
        else
        {
            circle.gameObject.SetActive(false);
            indoor.SetActive(false);
            gameLight.SetActive(true);
            PlayerShow();
        }
    }

    public void DeebotRemove()
    {
        if (deebotPosValue > 0.6)
        {
            deebotPosValue -= 0.01f;
            deebot.transform.localPosition = new Vector3(deebotPosValue, 0.04f, 0);
        }
        else
        {
            if (deebotScaleValue < 1)
            {
                deebotScaleValue += 0.002f;
                deebot.transform.localScale = new Vector3(deebotScaleValue, deebotScaleValue, deebotScaleValue);

            }
            else
            {
                if (deebotValue < 1)
                {
                    deebotValue += 0.01f;
                    mt_deebot_main.SetFloat("_AdvancedDissolveCutoutStandardClip", deebotValue);
                    mt_deebot_text.SetFloat("_AdvancedDissolveCutoutStandardClip", deebotValue);
                }
                else
                {
                    deebot.SetActive(false);
                }
            }
        }

    }

    public void PlayerShow()
    {
        player.SetActive(true);
        ship.SetActive(true);

        if (shipValue > 0)
        {
            shipValue -= 0.008f;
            mt_ship.SetFloat("_AdvancedDissolveCutoutStandardClip", shipValue);

        }
        else
        {
            playerCamera.SetActive(true);
            sceneCamera.SetActive(false); 
            tailFire.SetActive(true);
        }
    }




}
