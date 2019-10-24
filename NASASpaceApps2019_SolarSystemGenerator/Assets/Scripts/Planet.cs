using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : Sphere
{
    public List<GameObject> Moons;
    
    //[Header("Planet Configurations:")] public int subDivisions;

    //void Start()
    //{
    //    InitAsIcosahedron();
    //    Subdivide(subDivisions);
    //    StartCoroutine(GenerateMeshPieceByPiece(0.02f, 10));
    //}



    void OnMouseDown()
    {
        if (UserInterface.Instance.FindCurrentActiveMenu() != "FreeRoamMenu") return;

        UserInterface.Instance.OpenMenu("PlanetMenu");
        UserInterface.Instance.CloseMenu("FreeRoamMenu");
        UserInterface.Instance.currentFocus = gameObject;
        FocusCamera.Instance.focus = gameObject;
        FocusCamera.Instance.gameObject.SetActive(true);
    }

}
