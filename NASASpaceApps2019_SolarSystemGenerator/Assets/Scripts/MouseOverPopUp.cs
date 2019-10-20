using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOverPopUp : MonoBehaviour
{
    [SerializeField] private GameObject popup;

    void OnMousEnter()
    {
        popup.SetActive(true);
    }
    
    void OnMouseExit()
    {
        popup.SetActive(false);
    }
}
