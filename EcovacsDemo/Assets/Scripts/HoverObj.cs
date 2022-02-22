using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BestHTTP;

public class HoverObj : MonoBehaviour
{
    private Transform uiRoot;
    private GameObject pref;
    private Vector3 screenPos;
    private Vector3 threeDPos;
    private Camera UICamera;
    public Vector3 ThreeDPos { get => threeDPos; }


    private void Start()
    {
        uiRoot = GameObject.Find("Canvas/UIRoot").transform;
        UICamera = GameObject.Find("UICamera").GetComponent<Camera>() ;
        pref = GameObject.Instantiate(Resources.Load<GameObject>("HoverUI")) ;
        pref.SetActive(false);
        pref.transform.SetParent(uiRoot);
        pref.transform.localPosition = Vector3.zero;
        pref.transform.localRotation = Quaternion.identity;
        pref.transform.localScale = Vector3.one;
        MeshFilter MeshFilter = GetComponent<MeshFilter>();
        Debug.Log("模型：" + gameObject.name + "  高度：" + MeshFilter.sharedMesh.bounds.size.y);
        float threeDPosY = MeshFilter.sharedMesh.bounds.size.y * transform.localScale.y + 0.1f;
        threeDPos = new Vector3(transform.position.x, threeDPosY, transform.position.z);
        Debug.Log("threeDPos：" + threeDPos.ToString());
    }



    public void Update()
    {
        bool isInView = IsInView(transform.position);
        //Debug.Log(isInView? "在":"不在");
        if (isInView)
        {
            pref.SetActive(true);
            screenPos = Camera.main.WorldToScreenPoint(threeDPos);
            //Debug.Log("screenPos：" + screenPos.ToString());

            pref.transform.position = screenPos;
        }
        else
        {
            pref.SetActive(false);
        }
    }

    public bool IsInView(Vector3 worldPos)
    {
        Transform camTransform = Camera.main.transform;
        Vector2 viewPos = Camera.main.WorldToViewportPoint(worldPos);
        Vector3 dir = (worldPos - camTransform.position).normalized;
        float dot = Vector3.Dot(camTransform.forward, dir);//判断物体是否在相机前面

        if (dot > 0 && viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1)
            return true;
        else
            return false;
    }

}


