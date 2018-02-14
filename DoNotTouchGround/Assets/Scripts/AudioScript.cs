using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioScript : MonoBehaviour
{

	public AudioClip DestroyAsteroidClip;

	public AudioSource DestroyAsteroidSource;

	// Use this for initialization
	void Start()
	{
		DestroyAsteroidSource.clip = DestroyAsteroidClip;
	}

	// Update is called once per frame
	void Update()
	{
		// Checks to see if asteroid is destroyed in Projectile.cs
//		if (GetComponent<Projectile>().isAsteroidDestroyed)
//		{
//				
//			DestroyAsteroidSource.Play();
//		}

	}
}
