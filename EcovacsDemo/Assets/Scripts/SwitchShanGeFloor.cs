using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchShanGeFloor : MonoBehaviour
{
    public GameObject shangeFloor;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeFloor()
    {
        if (!shangeFloor.activeSelf)
        {
            shangeFloor.SetActive(true);
        }
        else
        {
            shangeFloor.SetActive(false);
        }
    }

}
