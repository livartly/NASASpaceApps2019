using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : Sphere
{
    [Header("Planet Configurations:")]
    public int subDivisions;
    
    void Start()
    {
        InitAsIcosahedron();
        Subdivide(subDivisions);
        StartCoroutine(GenerateMeshPieceByPiece(0.02f, 10));
    }



    IEnumerator GenerateMeshPieceByPiece(float delay, int pieceSize)
    {
        Mesh mesh = new Mesh();
        //Assign data to mesh
        GetComponent<MeshFilter>().mesh = mesh;


        List<Triangle> currentTriangles = new List<Triangle>();
        
        for (int i = 0 ; i < (triangles.Count / pieceSize) ; i++)
        {
            mesh.Clear();
            for (int j = 0; j < pieceSize; j++)
            {
                Debug.Log((i * pieceSize) + j);
                currentTriangles.Add(triangles[(i * pieceSize) + j]);
            }
            mesh.vertices = vertices.ToArray();
            mesh.triangles = Triangle.TrianglesToIntArray(currentTriangles);
            mesh.RecalculateNormals();
            yield return new WaitForSeconds(delay);

        }
        int leftOver = triangles.Count % pieceSize;
        for (int i = 0; i < leftOver; i++)
        {
            currentTriangles.Add(triangles[triangles.Count - 1 - i]);
        }
        mesh.vertices = vertices.ToArray();
        mesh.triangles = Triangle.TrianglesToIntArray(currentTriangles);
        mesh.RecalculateNormals();
    }

    
}
