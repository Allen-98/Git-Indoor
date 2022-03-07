using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchFloor : MonoBehaviour
{
    public GameObject shanGeFloor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchShanGeFloor()
    {
        if (!shanGeFloor.activeSelf)
        {
            shanGeFloor.SetActive(true);
        }
        else
        {
            shanGeFloor.SetActive(false);
        }
    }

}
