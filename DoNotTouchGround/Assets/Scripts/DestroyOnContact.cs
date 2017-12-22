using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnContact : MonoBehaviour {

    private GameManager gameManager;
    private Planet_Script planet;
    public int scoreValue;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameManager");
        if (gameControllerObject != null)
        {
            gameManager = gameControllerObject.GetComponent<GameManager>();
        }
        else
        {
            Debug.Log("cant fine 'GameManager' object");
        }

        GameObject planetObject = GameObject.FindWithTag("Planet");
        if (planetObject != null)
        {
            planet = planetObject.GetComponent<Planet_Script>();
        }
        else
        {
            Debug.Log("cant fine 'Planet' object");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.SetActive(false);
            gameManager.GameOver();
        }
        if (other.tag == "Projectile")
        {
            //Debug.Log("I hit a Projectile");
            gameManager.AddScore(10);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        if (other.tag == "Asteroid")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        if (other.tag == "Planet")
        {
            Debug.Log("asteroid knows it hit the planet");
            Destroy(gameObject);
            planet.PlanetHitByAsteroid();
        }

        //Debug.Log("something collided but didnt trigger is statements");

        //if (hitCount >= 1)
        //{
        //    gameManager.AddScore(scoreValue);
        //    Destroy(other.gameObject);
        //    Destroy(gameObject);
        //}
        //else
        //{
        //    hitCount++;
        //    Destroy(other.gameObject);
        //}


    }
}
