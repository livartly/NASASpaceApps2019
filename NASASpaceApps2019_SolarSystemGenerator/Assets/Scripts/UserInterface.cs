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
    
    [Header("Planet Materials:")]
    [SerializeField] private List<Material> planetMaterials;



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
                CloseMenu("PlanetMenu");
                OpenMenu("FreeRoamMenu");
                FocusCamera.Instance.gameObject.SetActive(false);
                ClosePopups();
                break;
            case "EscapeMenu":
                CloseMenu("EscapeMenu");
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
                StarMassSlider.value = currentFocus.transform.localScale.x;
                TemperatureSlider.value = currentFocus.GetComponent<Star>().temperature;
                AgeSlider.value = currentFocus.GetComponent<Star>().age;
                break;
            case "PlanetMenu":
                planetMenu.SetActive(true);
                PlanetMassSlider.value = currentFocus.GetComponent<Planet>().mass;
                MoonsSlider.value = currentFocus.GetComponent<Planet>().numberOfMoons;
                DistanceSlider.value = currentFocus.GetComponent<Planet>().distance;
                //set toggles
                    
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
            case "ArgAtmo":
                if (ArgonAtmo.interactable)
                {
                    ArgonAtmo.interactable = false;
                    if (WaterOnPlanet.isOn)
                    {
                        currentFocus.GetComponent<MeshRenderer>().material = planetMaterials[4];
                        currentFocus.GetComponent<Planet>().mat = planetMaterials[4];
                    }
                    else 
                    { 
                        currentFocus.GetComponent<MeshRenderer>().material = planetMaterials[5];
                        currentFocus.GetComponent<Planet>().mat = planetMaterials[5];
                    }
                }
                else ArgonAtmo.interactable = true;
                break;
            case "OxyAtmo":
                if (OxygenAtmo.interactable)
                {
                    OxygenAtmo.interactable = false;
                    if (WaterOnPlanet.isOn)
                    {
                        currentFocus.GetComponent<MeshRenderer>().material = planetMaterials[11];
                        currentFocus.GetComponent<Planet>().mat = planetMaterials[11];
                    }
                    else
                    {
                        currentFocus.GetComponent<MeshRenderer>().material = planetMaterials[12];
                        currentFocus.GetComponent<Planet>().mat = planetMaterials[12];
                    }
                }
                else OxygenAtmo.interactable = true;
                break;
            case "MetAtmo":
                if (MethaneAtmo.interactable)
                {
                    MethaneAtmo.interactable = false;
                    if (WaterOnPlanet.isOn)
                    {
                        currentFocus.GetComponent<MeshRenderer>().material = planetMaterials[9];
                        currentFocus.GetComponent<Planet>().mat = planetMaterials[9];
                    }
                    else
                    {
                        currentFocus.GetComponent<MeshRenderer>().material = planetMaterials[10];
                        currentFocus.GetComponent<Planet>().mat = planetMaterials[10];
                    }
                }
                else MethaneAtmo.interactable = true;
                break;
            case "AmmAtmo":
                if (AmmoniaAtmo.interactable)
                {
                    AmmoniaAtmo.interactable = false;
                    if (WaterOnPlanet.isOn)
                    {
                        currentFocus.GetComponent<MeshRenderer>().material = planetMaterials[1];
                        currentFocus.GetComponent<Planet>().mat = planetMaterials[1];
                    }
                    else
                    {
                        currentFocus.GetComponent<MeshRenderer>().material = planetMaterials[2];
                        currentFocus.GetComponent<Planet>().mat = planetMaterials[2];
                    }
                }
                else AmmoniaAtmo.interactable = true;
                break;
            case "CarAtmo":
                if (CarbonDioxideAtmo.interactable)
                {
                    CarbonDioxideAtmo.interactable = false;
                    if (WaterOnPlanet.isOn)
                    {
                        currentFocus.GetComponent<MeshRenderer>().material = planetMaterials[6];
                        currentFocus.GetComponent<Planet>().mat = planetMaterials[6];
                    }
                    else
                    {
                        currentFocus.GetComponent<MeshRenderer>().material = planetMaterials[7];
                        currentFocus.GetComponent<Planet>().mat = planetMaterials[7];
                    }
                }
                else CarbonDioxideAtmo.interactable = true;
                break;
            case "AlkGas":
                if (AlkaliClass.interactable)
                {
                    AlkaliClass.interactable = false;
                    currentFocus.GetComponent<MeshRenderer>().material = planetMaterials[0];
                    currentFocus.GetComponent<Planet>().mat = planetMaterials[0];
                }
                else AlkaliClass.interactable = true;
                break;
            case "AmmGas":
                if (AmmoniaGiantClass.interactable)
                {
                    AmmoniaGiantClass.interactable = false;
                    currentFocus.GetComponent<MeshRenderer>().material = planetMaterials[3];
                    currentFocus.GetComponent<Planet>().mat = planetMaterials[3];
                }
                else AmmoniaGiantClass.interactable = true;
                break;
            case "CloGas":
                if (CloudlessClass.interactable)
                {
                    CloudlessClass.interactable = false;
                    currentFocus.GetComponent<MeshRenderer>().material = planetMaterials[8];
                    currentFocus.GetComponent<Planet>().mat = planetMaterials[8];
                }
                else CloudlessClass.interactable = true;
                break;
            case "SilGas":
                if (SilicateClass.interactable)
                {
                    SilicateClass.interactable = false;
                    currentFocus.GetComponent<MeshRenderer>().material = planetMaterials[13];
                    currentFocus.GetComponent<Planet>().mat = planetMaterials[13];
                }
                else SilicateClass.interactable = true;
                break;
            case "WatGas":
                if (WaterGiantClass.interactable)
                {
                    WaterGiantClass.interactable = false;
                    currentFocus.GetComponent<MeshRenderer>().material = planetMaterials[14];
                    currentFocus.GetComponent<Planet>().mat = planetMaterials[14];
                }
                else WaterGiantClass.interactable = true;
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
                currentFocus.GetComponent<Star>().age = AgeSlider.value;
                break;
            case "StarTemperature":
                currentFocus.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_EmissionColor", (Color.white / 10) * TemperatureSlider.value);
                currentFocus.GetComponent<Star>().temperature = TemperatureSlider.value;
                break;
            case "PlanetMass":
                currentFocus.transform.localScale = new Vector3(1, 1, 1) * PlanetMassSlider.value;
                currentFocus.GetComponent<Planet>().mass = PlanetMassSlider.value;
                break;
            case "PlanetMoons":
                currentFocus.GetComponent<Planet>().numberOfMoons = MoonsSlider.value;
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
                currentFocus.GetComponent<Planet>().distance = DistanceSlider.value;
                break;
        }
    }

    public void OpenURL(string URL)
    {
        Application.OpenURL(URL);
    }

    public void OnWaterToggleChangeValue()
    {
        if (WaterOnPlanet.isOn)
        {
            if (currentFocus.GetComponent<Planet>().mat == planetMaterials[1])
            {
                currentFocus.GetComponent<Planet>().mat = planetMaterials[2];
            }
            else if (currentFocus.GetComponent<Planet>().mat == planetMaterials[4])
            {
                currentFocus.GetComponent<Planet>().mat = planetMaterials[5];
            }
            else if (currentFocus.GetComponent<Planet>().mat == planetMaterials[6])
            {
                currentFocus.GetComponent<Planet>().mat = planetMaterials[7];
            }
            else if (currentFocus.GetComponent<Planet>().mat == planetMaterials[9])
            {
                currentFocus.GetComponent<Planet>().mat = planetMaterials[10];
            }
            else if (currentFocus.GetComponent<Planet>().mat == planetMaterials[11])
            {
                currentFocus.GetComponent<Planet>().mat = planetMaterials[12];
            }
        }
        else
        {

        }
    }
}
