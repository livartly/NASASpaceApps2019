using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private Camera() { }
    public static Camera Instance { get; private set; }
    
    public float speedNormal = 10.0f;
    public float speedFast = 50.0f;

    public float mouseSensitivityX = 5.0f;
    public float mouseSensitivityY = 5.0f;

    float rotY = 0.0f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }


    private void Start()
    {
        if (GetComponent<Rigidbody>()) GetComponent<Rigidbody>().freezeRotation = true;
    }


    void Update()
    {
        if (UserInterface.Instance.FindCurrentActiveMenu() == "FreeRoamMenu")
        {
            //FreeMovement
            float speed;
            if (Input.GetKey(KeyCode.LeftShift)) speed = speedFast;
            else speed = speedNormal;

            if (Input.GetKey(KeyCode.W))
            {
                GetComponent<Rigidbody>().AddForce(transform.forward * speed);
            }
            if (Input.GetKey(KeyCode.S))
            {
                GetComponent<Rigidbody>().AddForce(-transform.forward * speed);
            }
            if (Input.GetKey(KeyCode.A))
            {
                GetComponent<Rigidbody>().AddForce(-transform.right * speed);
            }
            if (Input.GetKey(KeyCode.D))
            {
                GetComponent<Rigidbody>().AddForce(transform.right * speed);
            }
            if (Input.GetMouseButton(1))
            {
                float rotX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * mouseSensitivityX;
                rotY += Input.GetAxis("Mouse Y") * mouseSensitivityY;
                rotY = Mathf.Clamp(rotY, -89.5f, 89.5f);
                transform.localEulerAngles = new Vector3(-rotY, rotX, 0.0f);
            }
        }

        


    }
}
