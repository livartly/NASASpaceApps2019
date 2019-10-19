using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangle
{
    public List<int> vertices;

    public Triangle(int a, int b, int c)
    {
        vertices = new List<int>() {a, b, c};
    }

    public static int[] TrianglesToIntArray(List<Triangle> tL)
    {
        List<int> tris = new List<int>();
        foreach (Triangle tri in tL)
        {
            foreach (int i in tri.vertices)
            {
                tris.Add(i);
            }
        }

        return tris.ToArray();
    }
}