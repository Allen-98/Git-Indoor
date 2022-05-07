using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DTT : MonoBehaviour
{
    public GameObject main;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        main.transform.Rotate(Vector3.up, 1);
    }
}
