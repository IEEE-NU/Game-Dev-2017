using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet_Script : Attackable {
    private GameManager gameManager;

    private float greenIncrment = 0f;

    // Use this for initialization
    void Start () {
        GameObject gameControllerObject = GameObject.FindWithTag("GameManager");
        if (gameControllerObject != null)
        {
            gameManager = gameControllerObject.GetComponent<GameManager>();
        }
        else
        {
            Debug.Log("cant fine 'GameManager' object");
        }
        //GetComponent<SpriteRenderer>().color = new Color(0f, 1f, 0f);
		m_currHealth = m_MaxHealth;
    }
	
	// Update is called once per frame
	void Update () {}

	public override void TakeDamage(float damage)  
        {
			m_currHealth -= damage;
			gameManager.SetHealth(m_currHealth);
			float ratio = (m_currHealth / m_MaxHealth);
			GetComponent<SpriteRenderer>().color = new Color(ratio,ratio,ratio);

			if (m_currHealth <= 0)
                {
                    //added code to make the explosion happen on planet death
                    GameObject expl = Instantiate(explosion, transform.position, Quaternion.identity);
                    Destroy(this.gameObject);
                    Destroy(expl, 3);
                    gameManager.GameOver();
                    gameObject.SetActive(false);
                    
                }
        }    
}