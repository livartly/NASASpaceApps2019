using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : Sphere
{
    [Header("Planet Configurations:")] public int subDivisions;

    void Start()
    {
        InitAsIcosahedron();
        Subdivide(subDivisions);
        GenerateMesh();
    }

    private void GenerateMesh()
    {
        Mesh mesh = new Mesh();
        //Assign data to mesh
        GetComponent<MeshFilter>().mesh = mesh;
        mesh.vertices = vertices.ToArray();
        mesh.triangles =
            Triangle.TrianglesToIntArray(triangles); //Could add a loop and delay for a nice generating effect
        mesh.RecalculateNormals(); //Update normals for light mapping
    }
}