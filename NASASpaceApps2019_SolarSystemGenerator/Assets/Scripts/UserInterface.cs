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
    [SerializeField] private GameObject escapeMenu;

    [Header("Star Input")]
    [SerializeField] private Slider StarMassSlider;
    [SerializeField] private Slider AgeSlider;
    [SerializeField] private Slider TemperatureSlider;

    [Header("Planet Input")]
    [SerializeField] private Toggle ArgonAtmo;
    [SerializeField] private Toggle OxygenAtmo;
    [SerializeField] private Toggle MethaneAtmo;
    [SerializeField] private Toggle AmmoniaAtmo;
    [SerializeField] private Toggle CarbonDioxideAtmo;
    [SerializeField] private Toggle WaterOnPlanet;
    [SerializeField] private Toggle AmmoniaGiantClass;
    [SerializeField] private Toggle WaterGiantClass;
    [SerializeField] private Toggle CloudlessClass;
    [SerializeField] private Toggle AlkaliClass;
    [SerializeField] private Toggle SilicateClass;
    [SerializeField] private Slider PlanetMassSlider;
    [SerializeField] private Slider MoonsSlider;
    [SerializeField] private Slider DistanceSlider;
    private string currentMenu;

    public GameObject currentFocus;
    [SerializeField] private GameObject moon;


    [Header("Popups:")]
    [SerializeField] private List<GameObject> popups;


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

    
    public void EscapeKeyPressed()
    {
        string currentMenu = FindCurrentActiveMenu();
        switch (currentMenu)
        {
            case "FreeRoamMenu":
                CloseMenu(currentMenu);
                OpenMenu("EscapeMenu");
                break;
            case "StarMenu":
                CloseMenu(currentMenu);
                OpenMenu("FreeRoamMenu");
                ClearCameraFocus();
                FocusCamera.Instance.gameObject.SetActive(false);
                ClosePopups();
                break;
            case "PlanetMenu":
                CloseMenu(currentMenu);
                OpenMenu("FreeRoamMenu");
                FocusCamera.Instance.gameObject.SetActive(false);
                ClosePopups();
                break;
            case "EscapeMenu":
                CloseMenu(currentMenu);
                OpenMenu("FreeRoamMenu");
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
    private void ClosePopups()
    {
        foreach (GameObject go in popups) 
        {
            go.SetActive(false);
        }
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
            case "EscapeMenu":
                escapeMenu.SetActive(true);
                Time.timeScale = 0;
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
            case "EscapeMenu":
                escapeMenu.SetActive(false);
                Time.timeScale = 1;
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

    public void TogglePlanetWaterButton()
    {
        if (WaterOnPlanet.interactable == true) WaterOnPlanet.interactable = false;
        else WaterOnPlanet.interactable = true;
    }

    public void SetPlanetWaterToFalse()
    {
        WaterOnPlanet.isOn = false;
    }

    public void ToggleButtonInteraction(string button)
    {
        switch (button)
        {
            case "Arg":
                if (ArgonAtmo.interactable)
                {
                    ArgonAtmo.interactable = false;
                }
                else ArgonAtmo.interactable = true;
                break;
            case "Oxy":
                if (OxygenAtmo.interactable) OxygenAtmo.interactable = false;
                else OxygenAtmo.interactable = true;
                break;
            case "Met":
                if (MethaneAtmo.interactable) MethaneAtmo.interactable = false;
                else MethaneAtmo.interactable = true;
                break;
            case "Amm":
                if (AmmoniaAtmo.interactable) AmmoniaAtmo.interactable = false;
                else AmmoniaAtmo.interactable = true;
                break;
            case "Car":
                if (CarbonDioxideAtmo.interactable) CarbonDioxideAtmo.interactable = false;
                else CarbonDioxideAtmo.interactable = true;
                break;
            default:
                Debug.Log("ERROR: No escape case set for this menu state");
                break;
        }
    }


    public void OnSliderChange(string slider)
    {
        switch (slider)
        {
            case "StarMass":
                currentFocus.transform.localScale = new Vector3(1,1,1) * StarMassSlider.value;
                //clear planet trails
                foreach (GameObject go in GameObject.FindGameObjectsWithTag("Planet"))
                {
                    go.transform.Find("ActualPlanet").GetComponent<TrailRenderer>().Clear();
                }
                break;
            case "StarAge":
                currentFocus.GetComponent<Renderer>().material.SetColor("_EmissionColor", (Color.white / AgeSlider.value));
                break;
            case "StarTemperature":
                currentFocus.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_EmissionColor", (Color.white / 10) * TemperatureSlider.value);
                break;
            case "PlanetMass":
                currentFocus.transform.localScale = new Vector3(1, 1, 1) * PlanetMassSlider.value;
                break;
            case "PlanetMoons":
                //Delete current moons
                foreach (GameObject go in currentFocus.GetComponent<Planet>().Moons)
                {
                    Destroy(go);
                }
                currentFocus.GetComponent<Planet>().Moons.Clear();
                //Instantiate proper number of new moons
                for (int i = 0; i < MoonsSlider.value; i++)
                {
                    GameObject go = Instantiate(moon, currentFocus.transform.parent);
                    currentFocus.GetComponent<Planet>().Moons.Add(go);
                    go.GetComponent<MoonOrbit>().staticBody = currentFocus;
                    go.GetComponent<MoonOrbit>().distanceFromSurface = .25f + (i * 0.3f);
                    go.GetComponent<MoonOrbit>().speed =  (0.003f + UnityEngine.Random.value) / 3;
                    float scale = 0.1f + (UnityEngine.Random.value / 2);
                    go.transform.localScale = new Vector3(scale, scale, scale);

                }
                break;
            case "PlanetDistance":
                currentFocus.transform.parent.GetComponent<Orbit>().distanceFromSurface = 1 * DistanceSlider.value;
                break;
        }
    }

    public void OpenURL(string URL)
    {
        Application.OpenURL(URL);
    }

    public void PushButton(string button)
    {
        switch (button)
        {
            case "ArgonAtmo":
                break;
        }
    }
    


}
