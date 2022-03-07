using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class GenMesh : MonoBehaviour
{
    public bool isThread = false;
    public bool DrawSeparated = false;

    [Tooltip("mesh的父物体")]
    public GameObject MeshMap;
    [Tooltip("mesh的父物体Position")]
    public Vector3 MeshPos;
    [Tooltip("mesh的父物体Rotation")]
    public Vector3 MeshRot;
    [Tooltip("mesh的父物体Scale")]
    public Vector3 MeshSac = new Vector3(1, 1, 1);
    //800*800���� key��pieceID value:100*100դ��
    private Dictionary<int, int[,]> gridData;
    //ֵ��Ӧ��ɫ�ֵ�
    Dictionary<int, Color> areaColor = new Dictionary<int, Color>();
    //����������ɫ��
    Stack<Color> colorStack = new Stack<Color> { };
    //户型栅格数据边框范围
    int xmax, ymax = 0;
    int xmin = int.MaxValue;
    int ymin = int.MaxValue;

    //mesh数据边框范围
    int mesh_xmax, mesh_ymax = 0;
    int mesh_xmin = int.MaxValue;
    int mesh_ymin = int.MaxValue;
    //100*100դ����� ������
    private List<string> pieces = new List<string> { "20", "27", "28", "35", "36", "43", "44" };
    //private List<string> pieces = new List<string> { "1"};
    //��������
    private Dictionary<Vector2Int, int> totalGridData = new Dictionary<Vector2Int, int>();
    //���ݺ�ͼƬ�ı���
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
        if (DrawSeparated)
        {
            return;
        }
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
                            //过滤
                            if (value == 0)
                                continue;
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
        print("栅格数据计算完毕，开始计算mesh顶点索引数据：");

        //
    
        CalMeshData();

    }

    //点集合
    List<Vector3> verts = new List<Vector3>();
    //生成mesh上对应的新的坐标
    Dictionary<Vector2Int, int> areaDic = new Dictionary<Vector2Int, int>();
    //栅格点-颜色 字典
    Dictionary<Vector2Int, int> indexDic = new Dictionary<Vector2Int, int>();
    //颜色-索引数组  字典
    Dictionary<int, List<int>> ColorIndices = new Dictionary<int, List<int>>();

    //计算mesh数据
    private void CalMeshData()
    {
        areaDic.Clear();
        Vector2Int mapCenter = new Vector2Int(800  / 2, 800  / 2);
        foreach (var Pos in totalGridData)
        {
            Vector2Int offset = new Vector2Int(Pos.Key.x - dataCenter.x, Pos.Key.y - dataCenter.y);
            int StartPosX = mapCenter.x +  offset.x;
            int StartPosY = mapCenter.y +  offset.y;
            //记录当前点的数据
            areaDic.Add(new Vector2Int(StartPosX,StartPosY),Pos.Value);
        }
        //计算出户型实际的区域
        mesh_xmin = mapCenter.x + xmin - dataCenter.x;
        mesh_xmax = mapCenter.x + xmax - dataCenter.x;
        mesh_ymin = mapCenter.y + ymin - dataCenter.y;
        mesh_ymax = mapCenter.y + ymax - dataCenter.y;
        int x_length = mesh_xmax - mesh_xmin + 1;
        int y_length = mesh_ymax - mesh_ymin + 1;
        //遍历整个正方形区域内单位正方形的点坐标，生成点集合和index集合
        //索引计数
        verts.Clear();
        for (int i = 0; i <= x_length; i+=1)
        {
            for (int j = 0; j <= y_length; j+=1)
            {
                verts.Add(new Vector3(i,j,0));
            }
        }
        ColorIndices.Clear();
        int index = 0;
        //绘制单位栅格
        lock(lock_2)
        {
            for (int i = 0; i < x_length; i += 1)
            {
                for (int j = 0; j < y_length; j += 1)
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
                    Vector2Int curPos = new Vector2Int(i + mesh_xmin, j + mesh_ymin);
                    int colorVal = -1;
                    if (areaDic.ContainsKey(curPos))
                    {
                        colorVal = areaDic[curPos];
                    }
                    if (colorVal > 0)
                    {
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
            }
            index++;
        }
        print("开始mesh绘制：");
        if (isThread)
        {
            if (DrawSeparated)
            {
                Loom.QueueOnMainThread(DrawSeparateMesh);
            }
            else
            {
                Loom.QueueOnMainThread(DrawMesh);
            }
        }
        else
        {
            if (DrawSeparated)
            {
                DrawSeparateMesh();
            }
            else
            {
                DrawMesh();
            }
        }
    }

    private int GetVextrexIndex(int x,int y)
    {
        int colunmNum = mesh_ymax - mesh_ymin + 1;
        return ((colunmNum + 1) * x + y);
    }
    private Object lock_1 = new Object();
    private Object lock_2 = new Object();
    private void DrawMesh()
    {
        lock(lock_1)
        {
            Mesh mesh = null;
            MeshFilter mf = null;
            MeshRenderer mr = null;
            GameObject go = GameObject.Find("MeshObj");
            if (go == null)
            {
                go = new GameObject();
                go.name = "MeshObj";
                go.transform.SetParent(MeshMap.transform);
                go.transform.position = new Vector3(0, 0, 0);
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
            if (verts.Count > 60000)
            {
                mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
            }
            else
            {
                mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt16;
            }

            mesh.name = "bigMesh";
            mf.mesh = mesh;
            //绘制不同颜色的SubMesh
            mesh.subMeshCount = ColorIndices.Count;
            mesh.SetVertices(verts);
            int index = 0;
           
            List<Material> materialList = new List<Material>();
            for (int i = 0; i < ColorIndices.Count; i++)
            {
                Material mat = null;
                mat = new Material(Shader.Find("Standard"));
                materialList.Add(mat);
            }
            mr.materials = materialList.ToArray() ;
            foreach (var item in ColorIndices)
            {
                Color pixelColor = areaColor[item.Key];
                mr.materials[index].color = pixelColor;
                mesh.SetIndices(item.Value, MeshTopology.Quads, index);
                index++;
            }
            mesh.RecalculateBounds();
            print("mesh绘制结束：" + Time.realtimeSinceStartup);
        }

        SetParentTrans();
    }

    private void DrawSeparateMesh()
    {
        foreach (var item in ColorIndices)
        {
            if (item.Key > 0)
            {
                DrawOnColorMesh(item);
            }
        }
        SetParentTrans();
    }

    private void DrawOnColorMesh(KeyValuePair<int, List<int>> ColorIndicesKVP)
    {
        lock (lock_1)
        {
            Mesh mesh = null;
            MeshFilter mf = null;
            MeshRenderer mr = null;
            GameObject go = GameObject.Find("MeshObj_" + ColorIndicesKVP.Key);
            if (go == null)
            {
                go = new GameObject();
                go.name = "MeshObj_" + ColorIndicesKVP.Key;
                go.transform.SetParent(MeshMap.transform);
                go.transform.position = new Vector3(0, 0, 0);
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
            int verCnt = ColorIndicesKVP.Value.Count / 4;

            if (verts.Count > 60000)
            {
                mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
            }
            else
            {
                mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt16;
            }

            mesh.name = ColorIndicesKVP.Key.ToString();
            mf.mesh = mesh;
            //绘制不同颜色的SubMesh
            mesh.subMeshCount = ColorIndices.Count;
            mesh.SetVertices(verts);

            List<Material> materialList = new List<Material>();

            Material mat = new Material(Shader.Find("Standard"));
            materialList.Add(mat);

            mr.materials = materialList.ToArray();

            Color pixelColor = areaColor[ColorIndicesKVP.Key];
            mr.materials[0].color = pixelColor;
            mesh.SetIndices(ColorIndicesKVP.Value, MeshTopology.Quads, 0);
            mesh.RecalculateBounds();
            print("mesh绘制结束：" + Time.realtimeSinceStartup);
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

    private void SetParentTrans()
    {
        
        MeshMap.transform.localPosition = MeshPos;
        MeshMap.transform.localRotation = Quaternion.Euler(MeshRot);
        MeshMap.transform.localScale = MeshSac;
    }
}
