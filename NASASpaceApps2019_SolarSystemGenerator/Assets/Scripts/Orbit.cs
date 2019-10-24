using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
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

    bool firstcall = true;

    public void FixedUpdate()
    {
        float distance = distanceFromSurface + (staticBody.transform.lossyScale.x * 0.5f);
        var staticBodyPos = staticBody.transform.localPosition;
        var movingBodyPos = transform.localPosition;
        var deltaPos = staticBodyPos - movingBodyPos;
        // If the moving body is not at the correct distance from the static body 
        if (System.Math.Abs(deltaPos.sqrMagnitude - distance * distance) > 1)
        {
            // correct that.
            transform.localPosition = transform.localPosition.normalized * distance;
        }
        transform.RotateAround(staticBodyPos, Vector3.up, 360f * speed * Time.deltaTime);

        //Clear trails on first call
        if (firstcall)
        {
            transform.Find("ActualPlanet").GetComponent<TrailRenderer>().Clear();
            firstcall = false;
        }

    }
}
