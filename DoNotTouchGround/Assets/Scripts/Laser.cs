﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    private LineRenderer lineRenderer;
    [SerializeField] private Transform laserHit;
    [SerializeField] private int distance = 10000;
	[SerializeField] private float laserDamageRate = 2.0f;
	[SerializeField] private GameObject LaserPrefab;

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
            
            // Play pew sound on initial laser firing
            FindObjectOfType<AudioScript>().PewSource.Play();
        }
        

    }

    //CoRoutine
    IEnumerator FireLaser()
    {   
        //make the line visible
        lineRenderer.enabled = true;
        
        
        
        while (Input.GetButton("Jump"))
        {   
            //Makes a new ray that the laser will be rendered upon
            Ray2D ray = new Ray2D(transform.position, transform.up);
            RaycastHit2D hit;

            lineRenderer.SetPosition(0, ray.origin);

            hit = Physics2D.Raycast(ray.origin, transform.up, distance);

            if (hit.collider)
            {
                lineRenderer.SetPosition(1, hit.point);
                if (hit.rigidbody)
                {
                    //if the raycast hits something, change the end point of the laser
                    // Debug.Log("I'm Hitting something with my laser");
                    hit.rigidbody.AddForceAtPosition(transform.up * 50, hit.point);
					hit.rigidbody.gameObject.GetComponent<Attackable> ().TakeDamage (Time.deltaTime * laserDamageRate);
					Instantiate (LaserPrefab, hit.point, Quaternion.Euler(transform.eulerAngles));
                    
                    // Play sound when laser hits enemy 
                    FindObjectOfType<AudioScript>().DestroyAsteroidSource.Play();

                }
            }
            else
            {
                //if there is no hit, render along the ray
                lineRenderer.SetPosition(1, ray.GetPoint(distance));
            }
            
            // Play sound on laser being fired
            FindObjectOfType<AudioScript>().ShootLaserSource.PlayOneShot
                (FindObjectOfType<AudioScript>().ShootLaserClip); 


            yield return null;
        }

        lineRenderer.enabled = false;


    }
}
