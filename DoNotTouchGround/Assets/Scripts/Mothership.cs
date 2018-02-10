using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mothership : MonoBehaviour {

	bool Available = true;
	bool m_targetingPoint = false;
	float m_delay = 0.0f;
	Vector3 m_currentTarget;
	Vector2 m_speed;
	float m_targetSpeed;

	const float ACCELERATION = 0.5f;
	const float CHASE_TOLERANCE = 0.2f;
	public float HoverDistance = 50f;

	public float SpawnInterval = 5.0f;
	public GameObject SpawnObject;
	float sinceLastSpawn = 0.0f;

	SpriteRenderer m_sprite;

	float timeOut = 0f;
	float targetTime = 0.6f;

	[SerializeField] private float m_spawnSpeed;

	void Start () {
		m_sprite = GetComponent<SpriteRenderer> ();
		m_currentTarget = new Vector2 ();
		m_speed = new Vector2 ();

		if (FindObjectOfType<Planet_Script> ()) {
			m_currentTarget = FindObjectOfType<Planet_Script> ().gameObject.transform.position;
		}
	}

	// Update is called once per frame
	void Update () {
		if (m_currentTarget != null) {
			float d = Vector3.Distance (m_currentTarget, transform.position);
			if (d > HoverDistance) {
				chaseTarget ();
			} else {
				spawnStuff ();
			}
		}
	}

	void chaseTarget() {
		if (Vector3.Distance (transform.position, m_currentTarget) > CHASE_TOLERANCE) {
			m_speed.x += ACCELERATION * Time.deltaTime * Mathf.Sign (m_currentTarget.x - transform.position.x);
			m_speed.y += ACCELERATION * Time.deltaTime * Mathf.Sign (m_currentTarget.y - transform.position.y);
		} else {
			m_targetingPoint = false;
		}
		timeOut += Time.deltaTime;
		if (timeOut > targetTime) {
			m_targetingPoint = false;
		}
		m_speed *= 0.99f;
		orientToSpeed (m_speed);
		transform.Translate (m_speed,Space.World);
	}

	void orientToSpeed(Vector2 speed) {
		m_sprite.transform.rotation = Quaternion.Euler (new Vector3(0f,0f,Mathf.Rad2Deg * Mathf.Atan2 (speed.y, speed.x)));
	}

	void spawnStuff() {
		if (sinceLastSpawn > SpawnInterval) {
			spawnItem ();
		} else {
			sinceLastSpawn += Time.deltaTime;
		}
	}

	void spawnItem() {
		// Otherwise, add an asteroid to the game.
		GameObject temp = Instantiate(SpawnObject, transform.position, Quaternion.identity);
		Transform tempLoc = temp.GetComponent<Transform>();
		tempLoc.position = transform.position;
		sinceLastSpawn = 0.0f;
	}
}
