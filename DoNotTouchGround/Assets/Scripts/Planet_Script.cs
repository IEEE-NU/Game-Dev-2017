using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet_Script : MonoBehaviour {
    public int life = 100;
    private GameManager gameManager;
    private int damage = 20;

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

        GetComponent<SpriteRenderer>().color = new Color(0f, 1f, 0f);

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlanetHitByAsteroid()  
        {
            Debug.Log("The planet was hit by an asteroid");

            life -= damage;
            gameManager.SubtractHealth(damage);

            greenIncrment += .2f;
            GetComponent<SpriteRenderer>().color = new Color(0f, 1f - greenIncrment, 0f);

            if (life <= 0)
                {
                    gameManager.GameOver();
                    gameObject.SetActive(false);
                }
        }

    
}
