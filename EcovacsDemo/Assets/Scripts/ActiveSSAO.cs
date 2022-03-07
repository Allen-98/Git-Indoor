using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SSSAO;

public class ActiveSSAO : MonoBehaviour
{

    public SimpleScreenSpaceAmbientOcclusion sssao;


    // Start is called before the first frame update
    void Start()
    {
        sssao.enabled = true;

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

        }
        else
        {
            sssao.enabled = true;
        }
    }


}
