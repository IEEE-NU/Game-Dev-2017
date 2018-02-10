using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {
	
	bool Available = true;
	bool m_targetingPoint = false;
	float m_delay = 0.0f;
	Vector3 m_currentTarget;
	Vector2 m_speed;
	float m_targetSpeed;
	GameObject playerTarget;

	const float ACCELERATION = 0.5f;
	const float CHASE_TOLERANCE = 0.2f;
	const float PURSUE_DISTANCE = 200f;

	SpriteRenderer m_sprite;

	ParticleSystem.MainModule part;
	TrailRenderer trail;
	Color red;
	Color blue;

	float timeOut = 0f;
	float targetTime = 0.6f;

	[SerializeField] private float m_spawnSpeed;

	void Start () {
		m_sprite = GetComponent<SpriteRenderer> ();
		m_currentTarget = new Vector2 ();
		m_speed = new Vector2 ();

		red = new Color(1f, 0f, 0f, 1f);
		blue = new Color(0f, 0.5f, 1f, 1f);

		trail = GetComponent<TrailRenderer> ();
		if (FindObjectOfType<Player> ()) {
			playerTarget = FindObjectOfType<Player> ().gameObject;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (playerTarget != null) {
			m_currentTarget = playerTarget.transform.position;
			if (Vector3.Distance (m_currentTarget, transform.position) < PURSUE_DISTANCE) {
				chaseTarget ();
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

	public void TargetPoint(Vector3 absLocation,float delay) {
		m_currentTarget = absLocation;
		m_delay = delay;
		//m_targetSpeed = speed;
		m_targetingPoint = true;
		SetActive (true);
		timeOut = 0f;
	}

	public void SetActive(bool active) {
		if (active) {
			Available = false;
			part.startColor = red;
			trail.startColor = red;
			trail.endColor = red;
		} else {
			part.startColor = blue;
			trail.startColor = blue;
			trail.endColor = blue;
			m_targetingPoint = false;
		}
	}
}