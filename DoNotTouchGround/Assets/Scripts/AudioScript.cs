using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioScript : MonoBehaviour
{

	public AudioClip DestroyAsteroidClip;
	public AudioSource DestroyAsteroidSource;
	
	public AudioClip ShootProjectileClip;
	public AudioSource ShootProjectileSource;
	
	public AudioClip ShootLaserClip;
	public AudioSource ShootLaserSource;
	
	public AudioClip PewClip;
	public AudioSource PewSource;


	// Use this for initialization
	void Start()
	{
		DestroyAsteroidSource.clip = DestroyAsteroidClip;
		ShootProjectileSource.clip = ShootProjectileClip;
		ShootLaserSource.clip = ShootLaserClip;
		PewSource.clip = PewClip;
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
