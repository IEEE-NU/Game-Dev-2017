using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    private LineRenderer lineRenderer;
    [SerializeField] private Transform laserHit;
    [SerializeField] private int distance = 10000;

    // Use this for initialization
    void Start () {

	    lineRenderer = GetComponent<LineRenderer>();
	    lineRenderer.enabled = false;
	    lineRenderer.useWorldSpace = true; // not sure if i want dis
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetButtonDown("Jump"))
        {
            StopCoroutine("FireLaser");
            StartCoroutine("FireLaser");
        }

    }

    IEnumerator FireLaser()
    {
        lineRenderer.enabled = true;

        while (Input.GetButton("Jump"))
        {
            Ray2D ray = new Ray2D(transform.position, transform.up);
            RaycastHit2D hit;

            lineRenderer.SetPosition(0, ray.origin);

            hit = Physics2D.Raycast(ray.origin, transform.up, distance);

            if (hit.collider)
            {
                lineRenderer.SetPosition(1, hit.point);
                if (hit.rigidbody)
                {
                    Debug.Log("I'm Hitting something with my laser");
                    hit.rigidbody.AddForceAtPosition(transform.up * 50, hit.point);
                }
            }
            else
                lineRenderer.SetPosition(1, ray.GetPoint(distance));



            yield return null;
        }

        lineRenderer.enabled = false;


    }
}
