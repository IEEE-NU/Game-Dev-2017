using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Something that causes damage to an attackable upon collision
public class Projectile : MonoBehaviour {

	public float m_damage = 1.0f;
	public string faction = "none";
	public GameObject PartEffect;

	void OnTriggerEnter2D(Collider2D col) // Destroy when collided with asteroid or player
	{
		if (col.gameObject.GetComponent<Attackable> () && 
			(faction == "none" || faction != col.gameObject.GetComponent<Attackable>().faction)) {
			col.gameObject.GetComponent<Attackable> ().TakeDamage (m_damage);
			if (PartEffect != null)
				Instantiate (PartEffect, transform.position,Quaternion.identity);
			Destroy (gameObject);

		}
		if (faction == "player" && col.gameObject.GetComponent<Planet_Script> ()) {
			if (PartEffect != null)
				Instantiate (PartEffect, transform.position,Quaternion.identity);
			Destroy (gameObject);
		}
	}
}
