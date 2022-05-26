using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongPress : MonoBehaviour
{

    public GameManager gm;
    public bool IsStart = false;

    void Update()
    {
        if (IsStart)
        {

            IsStart = false;
        }
    }
    public void LPress(bool bStart)
    {

        IsStart = bStart;

    }
}
