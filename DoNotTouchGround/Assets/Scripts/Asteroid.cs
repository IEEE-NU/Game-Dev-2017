using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    // Maximum speed allowed for an enemy
    [SerializeField] private float m_MaxSpeed;
    Vector2 centerPoint = new Vector2(0f, 0f);

    // Called when the enemy is initialized
    private void Start()
    {
        // Get initial point immediately upon spawn
        Vector2 spawnPoint = GetComponent<Transform>().localPosition;
        // Calculate direction to center of screen (Earth)
        Vector2 asteroidDirection = centerPoint - spawnPoint;
        // Get unit vector
        asteroidDirection = asteroidDirection / asteroidDirection.magnitude;
        Debug.Log(asteroidDirection.x);
        Debug.Log(asteroidDirection.y);

        /*
		 * Add a random force to the enemy, effectively moving them in a
		 * random direction at a random speed.
		 */

        float magnitude = Random.Range(-0f, 1f) * m_MaxSpeed;

        var xForce = asteroidDirection.x * magnitude;
        var yForce = asteroidDirection.y * magnitude;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(xForce, yForce));
    }
}