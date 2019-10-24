using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : MonoBehaviour
{
    public void TogglePopup()
    {
        if (gameObject.activeSelf) gameObject.SetActive(false);
        else gameObject.SetActive(true);
    }

    public void EnabledPopup()
    {
        gameObject.SetActive(true);
    }

    public void DisablePopup()
    {
        gameObject.SetActive(false);
    }
}
