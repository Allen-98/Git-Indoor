using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PixelEdit : MonoBehaviour
{
    Texture2D texture;
    Color color;

    // Start is called before the first frame update
    void Start()
    {
        color = new Color(1, 0, 0, 1);
        texture = Resources.Load<Texture>("floor1") as Texture2D;
        //texture = new Texture2D(GetComponent<RawImage>().texture.width, GetComponent<RawImage>().texture.height, TextureFormat.BGRA32, false);
        //texture = GetComponent<RawImage>().texture as Texture2D;
        texture.Apply();

        Debug.Log(texture.width);
        Debug.Log(texture.height);


    }

    // Update is called once per frame
    void Update()
    {
        for (int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x += 5)
            {

                texture.SetPixel(x, y, color);

            }
        }
    }
}
