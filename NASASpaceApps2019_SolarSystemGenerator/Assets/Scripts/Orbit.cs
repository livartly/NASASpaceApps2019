using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    [SerializeField] private GameObject staticBody;

    /// <summary>
    /// Distance between movingBody and staticBody in world space coordinates
    /// </summary>
    public int distance;

    /// <summary>
    /// Displayed speed of revolutions per second.
    /// </summary>
    public float speed;

    public void Update()
    {
        var staticBodyPos = staticBody.transform.position;
        var movingBodyPos = transform.position;
        var deltaPos = staticBodyPos - movingBodyPos;
        // If the moving body is not at the correct distance from the static body 
        if (System.Math.Abs(deltaPos.sqrMagnitude - distance * distance) > 1)
        {
            // correct that.
            transform.position = transform.position.normalized * distance;
        }
        transform.RotateAround(staticBodyPos, Vector3.up, 360f * speed * Time.deltaTime);
    }
}
