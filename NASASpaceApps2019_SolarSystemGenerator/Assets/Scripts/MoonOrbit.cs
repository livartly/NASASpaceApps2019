using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonOrbit : MonoBehaviour
{
    public GameObject staticBody;

    /// <summary>
    /// Distance between movingBody and the surface of the star
    /// </summary>
    public float distanceFromSurface;

    /// <summary>
    /// Displayed speed of revolutions per second.
    /// </summary>
    public float speed;

    public void FixedUpdate()
    {
        float distance = distanceFromSurface + (staticBody.transform.lossyScale.x * 0.5f);
        var staticBodyPos = staticBody.transform.position;
        var movingBodyPos = transform.position;
        var deltaPos = staticBodyPos - movingBodyPos;
        // If the moving body is not at the correct distance from the static body 
        if (System.Math.Abs(deltaPos.sqrMagnitude - distance * distance) > 1)
        {
            // correct that.
            transform.position = (transform.position.normalized * distance) + staticBodyPos;
        }
        transform.RotateAround(staticBodyPos, Vector3.up, 360f * speed * Time.deltaTime);


    }
}
