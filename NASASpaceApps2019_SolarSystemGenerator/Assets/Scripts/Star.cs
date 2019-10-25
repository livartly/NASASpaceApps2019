using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    public float age;
    public float temperature;

    void OnMouseDown()
    {
        if (UserInterface.Instance.FindCurrentActiveMenu() != "FreeRoamMenu") return;
        FocusCamera.Instance.focus = gameObject;
        UserInterface.Instance.currentFocus = gameObject;
        UserInterface.Instance.OpenMenu("StarMenu");
        UserInterface.Instance.CloseMenu("FreeRoamMenu");
        
        
        FocusCamera.Instance.gameObject.SetActive(true);
    }
}
