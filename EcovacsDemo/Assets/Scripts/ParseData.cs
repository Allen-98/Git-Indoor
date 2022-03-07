using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class ParseData : MonoBehaviour
{
    public Texture2D texture;
    //800*800数据 key：pieceID value:100*100栅格
    private Dictionary<int, int[,]> gridData;
    //值对应颜色字典
    Dictionary<int, Color> areaColor = new Dictionary<int, Color>();
    //备用区域颜色库
    Stack<Color> colorStack = new Stack<Color> { };
    //栅格数据边缘数据
    int xmax, ymax = 0;
    int xmin = int.MaxValue;
    int ymin = int.MaxValue;
    //100*100栅格序号 测试用
    private List<string> pieces = new List<string> { "20", "27", "28", "35", "36", "43", "44" };
    //所有数据
    private Dictionary<Vector2Int, int> totalGridData = new Dictionary<Vector2Int, int>();
    //数据和图片的比例
    private int mapScale = 5;
    private Vector2Int dataCenter;
    private float timing;
    private int intervalTime = 2;
    // Start is called before the first frame update
    void Start()
    {
        colorStack.Push(new Color(175 / 255f, 72 / 255f, 64 / 255f));
        colorStack.Push(Color.blue);
        colorStack.Push(Color.green);
        colorStack.Push(Color.black);
        colorStack.Push(Color.yellow);
        colorStack.Push(new Color(255 / 255f, 120 / 255f, 9 / 255f));
        colorStack.Push(Color.grey);
        colorStack.Push(new Color(123 / 255f, 120 / 255f, 9 / 255f));
        colorStack.Push(new Color(2 / 255f, 245 / 255f, 123 / 255f));
        colorStack.Push(new Color(127 / 255f, 3 / 255f, 9 / 255f));

        texture = Resources.Load<Texture>("floor1") as Texture2D;
        
        ResetTexture();
        //DrawArea();
    }

    // Update is called once per frame
    void Update()
    {
        timing = timing + Time.deltaTime;
        if (timing > intervalTime)
        {
            timing = intervalTime - timing;

            HandleData();
            Loom.RunAsync(DrawArea);
            //DrawArea();
            Debug.LogError("11111122222");
        }

        //MatchTexture();
    }

    private void HandleData()
    {
        gridData = new Dictionary<int, int[,]>();
        foreach (var item in pieces)
        {
            TextAsset content = Resources.Load<TextAsset>(item);
            int[] str = JsonConvert.DeserializeObject<int[]>(content.text);
            Debug.Log(str.ToString());

            int[,] b = new int[100, 100];

            for (int i = 0; i < 10000; i++)
                b[i / 100, i % 100] = str[i];
            gridData.Add(int.Parse(item), b);
        }
    }

    public void DrawArea()
    {
        int index = 0;
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (gridData.ContainsKey(index))
                {
                    int[,] meshGrid = gridData[index];

                    for (int x = 0; x < 100; x++)
                    {
                        for (int y = 0; y < 100; y++)
                        {
                            int value = meshGrid[x, y];
                            if (value > 0)
                            {
                                if (!areaColor.ContainsKey(value))
                                {
                                    if (colorStack.Count == 0)
                                    {
                                        Debug.Log("颜色不够用了");
                                        continue;
                                    }
                                    areaColor.Add(value, colorStack.Pop());
                                }
                                int PosX = 100 * i;
                                int PosY = 100 * j;
                                HandleBound(PosX + x, PosY + y);
                                Vector2Int PosKey = new Vector2Int(PosX + x, PosY + y);
                                if (!totalGridData.ContainsKey(PosKey))
                                {
                                    totalGridData.Add(new Vector2Int(PosX + x, PosY + y), value);
                                }
                                else
                                {
                                    totalGridData[PosKey] = value;
                                }
                            }
                        }
                    }
                }
                index++;
            }
        }
        //计算中心点
        dataCenter = CalCenter();
        Loom.QueueOnMainThread(MatchTexture);
    }

    private void MatchTexture()
    {
        Vector2Int mapCenter = new Vector2Int(texture.width / 2, texture.height / 2);
        //画图
        foreach (var Pos in totalGridData)
        {
            Vector2Int offset = new Vector2Int(Pos.Key.x - dataCenter.x, Pos.Key.y - dataCenter.y);
            int StartPosX = mapCenter.x + mapScale * offset.x;
            int StartPosY = mapCenter.y + mapScale * offset.y;
            Color pixelColor = areaColor[Pos.Value];
            for (int i = StartPosX; i < StartPosX + mapScale; i++)
            {
                for (int j = StartPosY; j < StartPosY + mapScale; j++)
                {
                    texture.SetPixel(i, j, pixelColor);
                }
            }
        }
        texture.Apply();
    }
    private void HandleBound(int x,int y)
    {
        if (x > xmax) 
        {
            //Debug.Log("xmax");
            xmax = x;
        }
        if (x < xmin) 
        {
            //Debug.Log("xmin");
            xmin = x;
        }
        if (y > ymax) 
        {
            //Debug.Log("ymax");
            ymax = y;
        }
        if (y < ymin) 
        {
            //Debug.Log("ymin");
            ymin = y;
        }
    }

    private Vector2Int CalCenter()
    {
        Debug.LogError(xmax + "  " + xmin  + "  " + ymax + "  " + ymin);
        Vector2Int center = new Vector2Int((xmax + xmin)/2 ,(ymax + ymin)/2);
        Debug.LogError("Center: " +center.x + "  " + center.y );
        return center;
    }

    private void ResetTexture()
    {
        for (int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x ++)
            {
                texture.SetPixel(x, y, Color.white);
            }
        }
    }
}
