using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigsizedAsteroid : MonoBehaviour
{
	// Maximum speed allowed for an enemy
	[SerializeField] float m_MaxSpeed;

	// Health allowed for an enemy
	[SerializeField]float m_MaxHealth;
	float m_CurrHealth = 0;

	// Called when the enemy is initialized
	private void Start()
	{
		// Get initial point immediately upon spawn
		Vector2 spawnPoint = GetComponent<Transform>().localPosition;
		// Calculate direction to center of screen (Earth)
		Vector2 asteroidDirection = -spawnPoint;
		// Get unit vector
		asteroidDirection = asteroidDirection / asteroidDirection.magnitude;

		/*
         * Add a random force to the enemy, effectively moving them in a
         * random direction at a random speed.
         */

		float magnitude = Random.Range(0.5f, 1f) * m_MaxSpeed;

		var xForce = asteroidDirection.x * magnitude;
		var yForce = asteroidDirection.y * magnitude;
		GetComponent<Rigidbody2D>().AddForce(new Vector2(xForce, yForce));
	}

	void OnCollisionEnter2D(Collision2D col) // Destroy when collided with asteroid or player
	{
		if (col.gameObject.name.Contains("Projectile"))
		{
			Destroy(col.gameObject);
			TakeDamage(1f);
		}
		else
		{
			Debug.Log("An Asteroid got hit!");
			Destroy(this.gameObject);
		}


	}

	public void TakeDamage(float damage)
	{
		m_CurrHealth += damage;
		if (m_CurrHealth >= m_MaxHealth)
			Destroy(this.gameObject);
	}

	void OnBecameInvisible()
	{
		Destroy(this.gameObject);
	}
}