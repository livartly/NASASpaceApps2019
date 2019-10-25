using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : Sphere
{
    public List<GameObject> Moons;
    public float mass;
    public float numberOfMoons;
    public float distance;
    public Material mat;

    void Start()
    {
        distance = transform.parent.GetComponent<Orbit>().distanceFromSurface;
        mass = transform.localScale.x;
        numberOfMoons = 0;
        mat = GetComponent<MeshRenderer>().material;
    }



    void OnMouseDown()
    {
        if (UserInterface.Instance.FindCurrentActiveMenu() != "FreeRoamMenu") return;
        FocusCamera.Instance.focus = gameObject;
        UserInterface.Instance.currentFocus = gameObject;


        UserInterface.Instance.CloseMenu("FreeRoamMenu");
        UserInterface.Instance.OpenMenu("PlanetMenu");
        
        
        FocusCamera.Instance.gameObject.SetActive(true);
    }

}
