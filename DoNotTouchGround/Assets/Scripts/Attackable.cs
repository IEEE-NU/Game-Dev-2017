﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attackable : MonoBehaviour {

	// Health allowed for an enemy
	[SerializeField] protected float m_MaxHealth;
	protected float m_currHealth;
	public string faction = "enemy";
    private GameManager gameManager;
    [SerializeField] public GameObject explosion;

    void Start () {
		m_currHealth = m_MaxHealth;
        GameObject gameControllerObject = GameObject.FindWithTag("GameManager");
        if (gameControllerObject != null)
        {
            gameManager = gameControllerObject.GetComponent<GameManager>();
        }
    }

    public virtual void TakeDamage(float damage) {
		m_currHealth -= damage;
	    if (m_currHealth <= 0)
	    {
	        if (!gameManager.gameover)
	        {
	            if (gameObject.tag == "Asteroid")
	                gameManager.AddScore(20);
	            if (gameObject.tag == "Missile")
	                gameManager.AddScore(50);
	            if (gameObject.tag == "Giant Asteroid")
	                gameManager.AddScore(50);
	            if (gameObject.tag == "Mothership")
	                gameManager.AddScore(100);
	            if (gameObject.tag == "Player")
	                gameManager.AddScore(0);
	            else
	            {
	                gameManager.AddScore(20);
	            }
	        }
	        GameObject expl = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
	        Destroy(expl, 3);
		    // play sound on destruction of asteroids
		    FindObjectOfType<AudioScript>().DestroyAsteroidSource.Play();
        }
	}
}
