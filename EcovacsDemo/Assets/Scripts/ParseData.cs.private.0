using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class ParseData : MonoBehaviour
{
    private bool isInit;
    public bool isThread = false;
    public GameObject MeshMap;

    public Material[] materials;
    Texture2D texture;
    //800*800���� key��pieceID value:100*100դ��
    private Dictionary<int, int[,]> gridData;
    //ֵ��Ӧ��ɫ�ֵ�
    Dictionary<int, Color> areaColor = new Dictionary<int, Color>();
    //����������ɫ��
    Stack<Color> colorStack = new Stack<Color> { };
    //դ�����ݱ�Ե����
    int xmax, ymax = 0;
    int xmin = int.MaxValue;
    int ymin = int.MaxValue;
    //100*100դ����� ������
    private List<string> pieces = new List<string> { "20", "27", "28", "35", "36", "43", "44" };
    //private List<string> pieces = new List<string> { "1"};
    //��������
    private Dictionary<Vector2Int, int> totalGridData = new Dictionary<Vector2Int, int>();
    //���ݺ�ͼƬ�ı���
    int size = 200;
    private int mapScale = 1;
    private Vector2Int dataCenter;
    private float timing;
    private int intervalTime = 2;

    private int compNum;
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
       
        //MeshMap.transform.SetParent(pos.transform);
        //MeshMap.transform.localScale = new Vector3(1, 1, 1);

        //ResetTexture();
        Debug.Log("解析数据：" + Time.realtimeSinceStartup);
        HandleData();
        Debug.Log("解析数据完毕，开始计算栅格数据：" + Time.realtimeSinceStartup);
        if (isThread)
        {
            Loom.RunAsync(CalGridData);
        }
        else
        {
            CalGridData();
        }
    }

    // Update is called once per frame
    void Update()
    {
        timing = timing + Time.deltaTime;
        if (timing > intervalTime)
        {
            timing = intervalTime - timing;
            //HandleData();
            if (isThread)
            {
                Loom.RunAsync(CalGridData);
            }
            else
            {
                CalGridData();
            }
        }
       
        if (compNum == 4)
        {
            for (int i = 0; i < 4; i++)
            {
                DrawMesh();
            }
            compNum = 0;
        }
    }

    //解析数据
    private void HandleData()
    {
        gridData = new Dictionary<int, int[,]>();
        foreach (var item in pieces)
        {
            TextAsset content = Resources.Load<TextAsset>(item);
            int[] str = JsonConvert.DeserializeObject<int[]>(content.text);
            int[,] b = new int[100, 100];

            for (int i = 0; i < 10000; i++)
                b[i / 100, i % 100] = str[i];
            gridData.Add(int.Parse(item), b);
        }
    }

    /// <summary>
    /// 拼合栅格数据
    /// </summary>
    public void CalGridData()
    {
        //Debug.Log("213123123123123");
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
                            if (!areaColor.ContainsKey(value))
                            {
                                if (colorStack.Count == 0)
                                {
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
                index++;
            }
        }
        //�������ĵ�
        dataCenter = CalCenter();
        print("栅格数据计算完毕，开始计算mesh顶点索引数据：" + Time.realtimeSinceStartup);

        //
        if (isThread)
        {
            Loom.QueueOnMainThread(CalMeshData);
        }
        else
        {
            CalMeshData();
        }
       
    }

    private void MatchTexture()
    {
        Vector2Int mapCenter = new Vector2Int(texture.width / 2, texture.height / 2);
        //��ͼ
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

    //点集合
    List<Vector3> verts = new List<Vector3>();
    //生成mesh上对应的新的坐标
    Dictionary<Vector2Int, int> areaDic = new Dictionary<Vector2Int, int>();
    //栅格点-颜色 字典
    Dictionary<Vector2Int, int> indexDic = new Dictionary<Vector2Int, int>();
    //颜色-索引数组  字典
    Dictionary<int, List<int>> ColorIndices = new Dictionary<int, List<int>>();


    List<Dictionary<int, List<int>>> AreaColorList = new List<Dictionary<int, List<int>>>();
    List<MeshGridData> meshGridDatas;
    private void CalMeshData()
    {
        areaDic.Clear();
        Vector2Int mapCenter = new Vector2Int(800 * mapScale / 2, 800 * mapScale / 2);
        foreach (var Pos in totalGridData)
        {
            Vector2Int offset = new Vector2Int(Pos.Key.x - dataCenter.x, Pos.Key.y - dataCenter.y);
            int StartPosX = mapCenter.x + mapScale * offset.x;
            int StartPosY = mapCenter.y + mapScale * offset.y;
            //记录当前点的数据
            areaDic.Add(new Vector2Int(StartPosX,StartPosY),Pos.Value);
        }

        //遍历整个正方形区域内单位正方形的点坐标，生成点集合和index集合
        //索引计数
        verts.Clear();
        for (int i = 0; i <=  size; i+=1)
        {
            for (int j = 0; j <= size; j+=1)
            {
                verts.Add(new Vector3(i,j,0));
            }
        }
        meshGridDatas =new List<MeshGridData>();
        ColorIndices.Clear();
        int index = 0;
        //绘制单位栅格
        //由于Mesh最多画65000个顶点，所有800*800拆成16个200*200的小mesh
        for (int gridIndexX = 1; gridIndexX < 3; gridIndexX++)
        {
            for (int gridIndexY = 1; gridIndexY < 3; gridIndexY++)
            {
                lock(lock_2)
                {
                    ColorIndices.Clear();
                    for (int i = 0; i < 200; i += 1)
                    {
                        for (int j = 0; j < 200; j += 1)
                        {
                            //当前点对应索引公式：index = (size+1)*x + y + 1
                            //左下角（当前点）
                            int index0 = GetVextrexIndex(i, j);
                            //左上角
                            int index1 = GetVextrexIndex(i, j + 1);
                            //右上角
                            int index2 = GetVextrexIndex(i + 1, j + 1);
                            //右下角
                            int index3 = GetVextrexIndex(i + 1, j);
                            //根据颜色区分SubMesh
                            Vector2Int curPos = new Vector2Int(size * gridIndexX + i, size * gridIndexY + j);

                            int colorVal = 0;
                            if (areaDic.ContainsKey(curPos))
                            {
                                colorVal = areaDic[curPos];
                            }
                            if (!ColorIndices.ContainsKey(colorVal))
                            {
                                ColorIndices.Add(colorVal, new List<int>());
                            }
                            List<int> curIndices = ColorIndices[colorVal];
                            //扩充Index
                            curIndices.Add(index0);
                            curIndices.Add(index1);
                            curIndices.Add(index2);
                            curIndices.Add(index3);
                            ColorIndices[colorVal] = curIndices;
                        }
                    }
                    AreaColorList.Add(ColorIndices);
                    MeshGridData meshGridData = new MeshGridData(gridIndexX, gridIndexY, index);
                    meshGridDatas.Add(meshGridData);
                    index++;
                    //print(gridIndexX + " " + gridIndexY +  "count??? " + ColorIndices.Count);
                    if (isThread)
                    {
                        compNum++;
                    }
                    else
                    {
                        DrawMesh();
                    }
                }
            }
        }
        print("mesh绘制完毕：" + Time.realtimeSinceStartup);
        if (!isInit)
        {
            MeshMap.transform.localPosition = new Vector3(-23.543f, 0.3f, -21.99f);
            MeshMap.transform.localRotation = Quaternion.Euler(new Vector3(90, 0, 0));
            MeshMap.transform.localScale = new Vector3(0.0563145f, 0.0563145f, 0.0563145f);
            isInit = true;
        }
        
    }

    private int GetVextrexIndex(int x,int y)
    {
        return ((size + 1) * x + y);
    }
    private Object lock_1 = new Object();
    private Object lock_2 = new Object();
    private void DrawMesh()
    {
        lock(lock_1)
        {
            if (meshGridDatas.Count == 0)
            {
                Debug.LogError("Mesh数据有问题");
                return;
            }

            MeshGridData meshGridData = meshGridDatas[0];
            meshGridDatas.RemoveAt(0);
            Mesh mesh = null;
            MeshFilter mf = null;
            MeshRenderer mr = null;
            GameObject go = GameObject.Find("MeshMap/" + meshGridData.GridIndexX + " " + meshGridData.GridIndexY);
            if (go == null)
            {
                go = new GameObject();
                go.name = meshGridData.GridIndexX + " " + meshGridData.GridIndexY;
                go.transform.position = new Vector3(meshGridData.GridIndexX * 200, meshGridData.GridIndexY * 200, 0);
                go.transform.SetParent(MeshMap.transform);
                mesh = new Mesh();
                mf = go.AddComponent<MeshFilter>();
                mr = go.AddComponent<MeshRenderer>();
            }
            else
            {
                mf = go.GetComponent<MeshFilter>();
                mr = go.GetComponent<MeshRenderer>();
                mesh = mf.mesh;
                mesh.Clear();
            }
            mesh.name = meshGridData.GridIndexX + " " + meshGridData.GridIndexY;
            mf.mesh = mesh;
            mr.materials = materials;
            //绘制不同颜色的SubMesh
            mesh.subMeshCount = AreaColorList[meshGridData.AreaIndex].Count;
            mesh.SetVertices(verts);
            int index = 0;
            //Debug.LogError("meshGridData:" + meshGridData.GridIndexX + "   " + meshGridData.GridIndexY + "  ColorIndices count!! " + AreaColorList[meshGridData.AreaIndex].Count);
            foreach (var item in AreaColorList[meshGridData.AreaIndex])
            {
                Color pixelColor = areaColor[item.Key];
                mr.materials[index].color = pixelColor;
                mesh.SetIndices(item.Value, MeshTopology.Quads, index);
                index++;
            }
            mesh.RecalculateBounds();
            

        }
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
        //Debug.LogError(xmax + "  " + xmin  + "  " + ymax + "  " + ymin);
        Vector2Int center = new Vector2Int((xmax + xmin)/2 ,(ymax + ymin)/2);
        //Debug.LogError("Center: " +center.x + "  " + center.y );
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


public class MeshGridData
{
    public int GridIndexX;
    public int GridIndexY;
    public int AreaIndex;

    public MeshGridData(int gridIndexX, int gridIndexY, int areaIndex)
    {
        GridIndexX = gridIndexX;
        GridIndexY = gridIndexY;
        AreaIndex = areaIndex;
    }
}
