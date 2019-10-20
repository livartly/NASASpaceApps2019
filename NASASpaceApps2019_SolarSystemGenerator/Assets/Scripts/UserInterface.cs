using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using TMPro;
using System.IO;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    private UserInterface() { }
    public static UserInterface Instance { get; private set; }

    [Header("References to Menu GameObjects")]
    [SerializeField] private GameObject freeRoamMenu;
    [SerializeField] private GameObject starMenu;
    [SerializeField] private GameObject planetMenu;

    [Header("References to Input Field GameObjects")]
    [SerializeField] private TMP_InputField input;

    private string currentMenu;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }


    public string FindCurrentActiveMenu()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).name.Contains("Menu") && transform.GetChild(i).gameObject.activeSelf)
            {
                return transform.GetChild(i).name;
            }
        }
        return "";
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) EscapeKeyPressed();
    }

    
    private void EscapeKeyPressed()
    {
        string currentMenu = FindCurrentActiveMenu();
        switch (currentMenu)
        {
            case "FreeRoamMenu":
                ExitGame();
                break;
            case "StarMenu":
                CloseMenu(currentMenu);
                OpenMenu("FreeRoamMenu");
                ClearCameraFocus();
                FocusCamera.Instance.gameObject.SetActive(false);
                break;
            case "PlanetMenu":
                CloseMenu(currentMenu);
                OpenMenu("FreeRoamMenu");
                FocusCamera.Instance.gameObject.SetActive(false);
                break;
            default:
                Debug.Log("ERROR: No escape case set for this menu state");
                break;
        }
    }
    private void ClearCameraFocus()
    {
        FocusCamera.Instance.focus = null;
    }

    public void OpenMenu(string menu)
    {
        switch (menu)
        {
            case "FreeRoamMenu":
                freeRoamMenu.SetActive(true);
                break;
            case "StarMenu":
                starMenu.SetActive(true);
                break;
            case "PlanetMenu":
                planetMenu.SetActive(true);
                break;
            default:
                Debug.Log("Menu could not be opened: " + menu);
                break;
        }
    }

    public void CloseMenu(string menu)
    {
        switch (menu)
        {
            case "FreeRoamMenu":
                freeRoamMenu.SetActive(false);
                break;
            case "StarMenu":
                starMenu.SetActive(false);
                break;
            case "PlanetMenu":
                planetMenu.SetActive(false);
                break;
            default:
                Debug.Log("Menu could not be closed: " + menu);
                break;
        }
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

}
