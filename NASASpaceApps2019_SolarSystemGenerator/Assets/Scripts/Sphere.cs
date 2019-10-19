using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    protected List<Triangle> triangles;
    protected List<Vector3> vertices;

    protected void InitAsIcosahedron()
    {
        triangles = new List<Triangle>();
        vertices = new List<Vector3>();

        // An icosahedron has 12 vertices, and
        // since it's completely symmetrical the
        // formula for calculating them is kind of
        // symmetrical too:

        float t = (1.0f + Mathf.Sqrt(5.0f)) / 2.0f;

        vertices.Add(new Vector3(-1, t, 0).normalized);
        vertices.Add(new Vector3(1, t, 0).normalized);
        vertices.Add(new Vector3(-1, -t, 0).normalized);
        vertices.Add(new Vector3(1, -t, 0).normalized);
        vertices.Add(new Vector3(0, -1, t).normalized);
        vertices.Add(new Vector3(0, 1, t).normalized);
        vertices.Add(new Vector3(0, -1, -t).normalized);
        vertices.Add(new Vector3(0, 1, -t).normalized);
        vertices.Add(new Vector3(t, 0, -1).normalized);
        vertices.Add(new Vector3(t, 0, 1).normalized);
        vertices.Add(new Vector3(-t, 0, -1).normalized);
        vertices.Add(new Vector3(-t, 0, 1).normalized);

        // And here's the formula for the 20 sides,
        // referencing the 12 vertices we just created.
        triangles.Add(new Triangle(0, 11, 5));
        triangles.Add(new Triangle(0, 5, 1));
        triangles.Add(new Triangle(0, 1, 7));
        triangles.Add(new Triangle(0, 7, 10));
        triangles.Add(new Triangle(0, 10, 11));
        triangles.Add(new Triangle(1, 5, 9));
        triangles.Add(new Triangle(5, 11, 4));
        triangles.Add(new Triangle(11, 10, 2));
        triangles.Add(new Triangle(10, 7, 6));
        triangles.Add(new Triangle(7, 1, 8));
        triangles.Add(new Triangle(3, 9, 4));
        triangles.Add(new Triangle(3, 4, 2));
        triangles.Add(new Triangle(3, 2, 6));
        triangles.Add(new Triangle(3, 6, 8));
        triangles.Add(new Triangle(3, 8, 9));
        triangles.Add(new Triangle(4, 9, 5));
        triangles.Add(new Triangle(2, 4, 11));
        triangles.Add(new Triangle(6, 2, 10));
        triangles.Add(new Triangle(8, 6, 7));
        triangles.Add(new Triangle(9, 8, 1));
    }

    protected void Subdivide(int recursions)
    {
        var midPointCache = new Dictionary<int, int>();

        for (int i = 0; i < recursions; i++)
        {
            var newTris = new List<Triangle>();
            foreach (var tri in triangles)
            {
                int a = tri.vertices[0];
                int b = tri.vertices[1];
                int c = tri.vertices[2];
                // Use GetMidPointIndex to either create a
                // new vertex between two old vertices, or
                // find the one that was already created.
                int ab = GetMidPointIndex(midPointCache, a, b);
                int bc = GetMidPointIndex(midPointCache, b, c);
                int ca = GetMidPointIndex(midPointCache, c, a);
                // Create the four new Triangles using our original
                // three vertices, and the three new midpoints.
                newTris.Add(new Triangle(a, ab, ca));
                newTris.Add(new Triangle(b, bc, ab));
                newTris.Add(new Triangle(c, ca, bc));
                newTris.Add(new Triangle(ab, bc, ca));
            }
            // Replace all our old Triangles with the new set of
            // subdivided ones.
            triangles = newTris;
        }
    }

    protected int GetMidPointIndex(Dictionary<int, int> cache, int indexA, int indexB)
    {
        // We create a key out of the two original indices
        // by storing the smaller index in the upper two bytes
        // of an integer, and the larger index in the lower two
        // bytes. By sorting them according to whichever is smaller
        // we ensure that this function returns the same result
        // whether you call
        // GetMidPointIndex(cache, 5, 9)
        // or...
        // GetMidPointIndex(cache, 9, 5)
        int smallerIndex = Mathf.Min(indexA, indexB);
        int greaterIndex = Mathf.Max(indexA, indexB);
        int key = (smallerIndex << 16) + greaterIndex;
        // If a midpoint is already defined, just return it.
        int ret;
        if (cache.TryGetValue(key, out ret))
            return ret;
        // If we're here, it's because a midpoint for these two
        // vertices hasn't been created yet. Let's do that now!
        Vector3 p1 = vertices[indexA];
        Vector3 p2 = vertices[indexB];
        Vector3 middle = Vector3.Lerp(p1, p2, 0.5f).normalized;

        ret = vertices.Count;
        vertices.Add(middle);

        cache.Add(key, ret);
        return ret;
    }

    protected IEnumerator GeneratePieceByPiece(Mesh mesh, float delay, int pieceSize)
    {
        List<Triangle> currentTriangles = new List<Triangle>();

        for (int i = 0; i < (triangles.Count * pieceSize); i++)
        {
            mesh.Clear();
            for (int j = 0; j < pieceSize; j++)
            {
                currentTriangles.Add(triangles[(i * 4) + j]);
            }
            mesh.vertices = vertices.ToArray();
            mesh.triangles = Triangle.TrianglesToIntArray(currentTriangles);
            mesh.RecalculateNormals();
            yield return new WaitForSeconds(delay);

        }

        
    }
}
