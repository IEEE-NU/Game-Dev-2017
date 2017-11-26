using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    // Maximum speed allowed for an enemy
    [SerializeField] private float m_MaxSpeed;

    // Called when the enemy is initialized
    private void Start()
    {
        /*
		 * Add a random force to the enemy, effectively moving them in a
		 * random direction at a random speed.
		 */
        var xForce = Random.Range(-1f, 1f) * m_MaxSpeed;
        var yForce = Random.Range(-1f, 1f) * m_MaxSpeed;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(xForce, yForce));
    }
}