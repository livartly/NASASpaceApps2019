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
    
}
