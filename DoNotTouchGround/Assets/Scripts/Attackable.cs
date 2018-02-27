using System.Collections;
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
	        if (gameObject.tag == "Asteroid")
	            gameManager.AddScore(20);
	        else
	        {
	            gameManager.AddScore(100);
            }
	        GameObject expl = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
	        Destroy(expl, 3);
		    // play sound on destruction
		    FindObjectOfType<AudioScript>().DestroyAsteroidSource.Play();
        }
	}
}
