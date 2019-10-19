using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    

    void OnMouseDown()
    {
        UserInterface.Instance.OpenMenu("StarMenu");
        UserInterface.Instance.CloseMenu("FreeRoamMenu");
        UserInterface.Instance.CloseMenu("PlanetMenu");
    }
}
