using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    /// <summary>
    /// Speed in revolutions / second.
    /// </summary>
    public float speed;

    public Vector3 rotationAxis = Vector3.up;

    private void Update()
    {
        transform.RotateAround(transform.position, rotationAxis, 360f * Time.deltaTime * speed);
    }
}
