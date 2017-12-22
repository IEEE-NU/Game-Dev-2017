using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MultipleTargetsCamera : MonoBehaviour
{

    public List<Transform> targets;

    public Vector3 offset;
    public float smoothtime = .5f;

    private Vector3 velocity;

    public float minCamSize = 30f;
    public float maxCamSize = 100f;

    public float CamSizeLimiter = 100f;

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        Move();
        Zoom();
    }

    void Zoom()
    {
        //Debug.Log("Greatest Distance is " + GetGreatestDistance());
        //Debug.Log("Cam size limiter is " + CamSizeLimiter);
        //Debug.Log("Division is " + GetGreatestDistance() / CamSizeLimiter);

        float newZoom = Mathf.Lerp(minCamSize, maxCamSize, GetGreatestDistance() / CamSizeLimiter);

        //Debug.Log("New zoom is " + newZoom);


        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, newZoom, Time.deltaTime);
    }

    void Move()
    {
        Vector3 center = GetCenterPoint();

        Vector3 newPosition = center + offset;

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothtime);
    }

    float GetGreatestDistance()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 1; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return Mathf.Max(bounds.size.x, bounds.size.y);
    }

    Vector3 GetCenterPoint()
    {
        if (targets.Count == 1)
        {
            return targets[0].position;
        }

        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 1; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.center;

    }
}
