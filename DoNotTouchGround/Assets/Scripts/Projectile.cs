using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Something that causes damage to an attackable upon collision
public class Projectile : MonoBehaviour {

	public float m_damage = 1.0f;
	public string faction = "none";

	void OnTriggerEnter2D(Collider2D col) // Destroy when collided with asteroid or player
	{
		if (col.gameObject.GetComponent<Attackable> () && 
			(faction == "none" || faction != col.gameObject.GetComponent<Attackable>().faction)) {
			col.gameObject.GetComponent<Attackable> ().TakeDamage (m_damage);
			Destroy (gameObject);
		}
		if (faction == "player" && col.gameObject.GetComponent<Planet_Script> ()) {
			Destroy (gameObject);
		}
	}
}
