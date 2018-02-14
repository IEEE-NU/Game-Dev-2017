using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attackable : MonoBehaviour {

	// Health allowed for an enemy
	[SerializeField] protected float m_MaxHealth;
	protected float m_currHealth;
	public string faction = "enemy";
    [SerializeField] private GameObject explosion;

    void Start () {
		m_currHealth = m_MaxHealth;
	}

	public virtual void TakeDamage(float damage) {
		m_currHealth -= damage;
	    if (m_currHealth <= 0)
	    {
	        GameObject expl = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
	        Destroy(expl, 3);
		    // play sound on destruction
		    //FindObjectOfType<AudioScript>().DestroyAsteroidSource.Play();
        }
	}
}
