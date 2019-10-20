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
        StartCoroutine(GenerateMeshPieceByPiece(0.02f, 10));
    }



    void OnMouseDown()
    {
        if (UserInterface.Instance.FindCurrentActiveMenu() == "PlanetMenu") return;
        if (UserInterface.Instance.FindCurrentActiveMenu() == "StarMenu") return;


        UserInterface.Instance.OpenMenu("PlanetMenu");
        UserInterface.Instance.CloseMenu("FreeRoamMenu");
        FocusCamera.Instance.focus = gameObject;
        FocusCamera.Instance.gameObject.SetActive(true);
    }

}
