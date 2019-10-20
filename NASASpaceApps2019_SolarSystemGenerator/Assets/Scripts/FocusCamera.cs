using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusCamera : MonoBehaviour
{
    private FocusCamera() { }
    public static FocusCamera Instance { get; private set; }

    public GameObject focus;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Update()
    {

        if (focus != null) transform.position = focus.transform.position + new Vector3(0, 0, -3);
    }

}
