using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGridCreator : MonoBehaviour
{
    public int width=5;
    public int height=5;

    private List<int> triangles = new List<int>();
    private List<Vector3> triVerts = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {

        triVerts = new List<Vector3>();
        triVerts.Add(new Vector3(0, 0, 0));
        triVerts.Add(new Vector3(0, 111, 110));
        triVerts.Add(new Vector3(110, 0, 111));

        int[] indices = new int[] { 0, 1, 2 };
       

        Mesh triMesh = new Mesh();

        //新写法
        triMesh.SetVertices(triVerts);
        triMesh.SetIndices(indices, MeshTopology.Triangles, 0);
       
        triMesh.RecalculateNormals();

        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        meshFilter.mesh = triMesh;

        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.material = new Material(Shader.Find("Standard"));


    }

    
   

}
