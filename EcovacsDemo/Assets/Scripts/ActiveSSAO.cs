using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SSSAO;

public class ActiveSSAO : MonoBehaviour
{

    public Text statusText;
    public SimpleScreenSpaceAmbientOcclusion sssao;
    public Text lightText;
    public GameObject light;



    // Start is called before the first frame update
    void Start()
    {
        sssao.enabled = true;

        if (sssao.enabled)
        {
            statusText.text = "On";
        }
        else
        {
            statusText.text = "Off";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchSSSAOStatus()
    {
        if (sssao.enabled)
        {
            sssao.enabled = false;
            statusText.text = "Off";
        }
        else
        {
            sssao.enabled = true;
            statusText.text = "On";
        }
    }

    public void SwitchLightStatus()
    {

        if (!light.gameObject.activeSelf)
        {
            light.SetActive(true);
            lightText.text = "On";
        }
        else
        {
            light.SetActive(false);
            lightText.text = "Off";

        }

    }

}
