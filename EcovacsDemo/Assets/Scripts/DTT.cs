using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DTT : MonoBehaviour
{
    [Header("Game Objects")]
    public GameObject window;
    public GameObject yellowFW;



    [Header("Materials")]
    public Material mt_window;
    public Material mt_redFW;
    public Material mt_yellowFW;
    public Material mt_blueFW;


    [Header("Scripts")]
    public SceneChanges sc;




    private float windowValue;
    private float yellowValue;


    // Start is called before the first frame update
    void Start()
    {
        mt_window.SetFloat("_FrostIntensity", 1f);


        mt_yellowFW.SetColor("_Color", new Color(1, 1, 1, 1));

    }

    // Update is called once per frame
    void Update()
    {

        //WindowRemove();
        //YellowFWRemove();

    }

    public void WindowRemove()
    {
        windowValue = mt_window.GetFloat("_FrostIntensity");

        if (windowValue > 0)
        {
            mt_window.SetFloat("_FrostIntensity", windowValue - 0.01f);
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
            yellowValue -= 0.01f;

            mt_yellowFW.SetColor("_Color", new Color(1, 1, 1, yellowValue));

        }
        else
        {
            yellowFW.SetActive(false);
        }


    }

}
