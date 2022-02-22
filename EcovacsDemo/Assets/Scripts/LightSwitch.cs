using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public GameObject[] lights;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MainLightChange()
    {
        if (!lights[0].gameObject.activeSelf)
        {
            lights[0].SetActive(true);
        }
        else
        {
            lights[0].SetActive(false);
        }
    }


    public void DinningLightChange()
    {
        if (!lights[1].gameObject.activeSelf)
        {
            lights[1].SetActive(true);
        }
        else
        {
            lights[1].SetActive(false);
        }
    }

    public void RoomLightChange()
    {
        if (!lights[2].gameObject.activeSelf)
        {
            lights[2].SetActive(true);
        }
        else
        {
            lights[2].SetActive(false);
        }
    }
}
