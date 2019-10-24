using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    

    void OnMouseDown()
    {
        if (UserInterface.Instance.FindCurrentActiveMenu() != "FreeRoamMenu") return;

        UserInterface.Instance.OpenMenu("StarMenu");
        UserInterface.Instance.CloseMenu("FreeRoamMenu");
        UserInterface.Instance.currentFocus = gameObject;
        FocusCamera.Instance.focus = gameObject;
        FocusCamera.Instance.gameObject.SetActive(true);
    }
}
