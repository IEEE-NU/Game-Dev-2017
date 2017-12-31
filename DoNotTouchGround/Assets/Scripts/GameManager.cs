using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    // Singleton Design Pattern
    public static GameManager Instance;

    //This is Main Camera in the scene
    Camera m_MainCamera;

    //score component
    private int score = 0;

    //score component
    public int healthPoints = 100;

    //text components for the HUD
    public Text scoreText;
    public Text healthText;
    public Text restartText;
    public Text gameOverText;

    //keeps track if game is over
    private bool gameover = false;

    // Modulate type of asteroid of spawn
    [SerializeField] private GameObject m_AsteroidPrefab;

    // get player object
    [SerializeField] private GameObject m_player;

    // Max enemies for debugging intially ***
    private const int MAX_ASTEROIDS = 100;

    // Current amount of enemies in the game
	private int m_CurrentAsteroids = 0;

    // Time since the last asteroid was spawned
    private float m_TimeSinceLastAsteroid = 0;

    // How frequently an asteroid should spawn
    [SerializeField] private float m_AddAsteroidTime;

    // Resets the game such that there is only one asteroid
    public void ResetGame()
    {
        foreach (var e in FindObjectsOfType<Asteroid>())
        {
            Destroy(e.gameObject);
        }
        m_CurrentAsteroids = 0;
        m_TimeSinceLastAsteroid = 0;
        AddEnemy();
    }

    // Called upon initialization
    private void Awake()
    {
        //This gets the Main Camera from the scene
        m_MainCamera = Camera.main;

        // Set the singleton instance
        if (Instance == null)
            Instance = this;
        // Reset the game to its initial state
        gameOverText.text = "";
        restartText.text = "";
    }

    // Called once per frame
    void Update () {

        //check if player is off the screen. If so, deactivate it and call gameover
        if (PlayerOffScreen())
        {
            Debug.Log("The PLayer is off screen");
            m_player.SetActive(false);
            GameOver();
        }


        //enable restart if game has gone to gameover
        //some issues with using GetKeyDown. Maybe cause its not checking on the right frames?
        if (gameover)
        {
            //Debug.Log("Game over state is true");

            if (Input.GetKey(KeyCode.P))
            {
                Debug.Log("Game over state is true and p is pressed");
                gameover = false;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        /*
		 * Increment time since last asteroid by the time that has elapsed since
		 * the last frame.
		 */
        m_TimeSinceLastAsteroid += Time.deltaTime;

        // If the time since the last asteroid is less than the spawn interval, do nothing.
        if (m_TimeSinceLastAsteroid <= m_AddAsteroidTime) return; //BECAREFUL OF THIS RETURN!!! IT WAS SCREWING UP MY OTHER CALLS

        // Otherwise, add an asteroid and reset the timer!
        m_TimeSinceLastAsteroid = 0;
        AddEnemy();

        
        

       
       
    }

    // Return random vector3 values to spawn asteroids
    private Vector3 spawnLocation()
    {
        float x, y, z;
        float height = 175f; //m_MainCamera.orthographicSize;
        float width = 275f; //height * m_MainCamera.aspect;
        // Choose to spawn either in x or y bounds
        if (Random.Range(0f, 1f) > 0.5f) // > 0.5 spawn X else spawn Y
        {
            x = Random.Range(0f, 1f) * width;
            if (Random.Range(-1f, 1f) > 0)
                y = height;
            else
                y = -height;
            z = 0f;

            
        }
        else
        {
            if (Random.Range(-1f, 1f) > 0)
                x = width;
            else
                x = -width;
            y = Random.Range(0f, 1f) * height;
            z = 0f;
        }
        Vector3 location = new Vector3(x, y, z);

        Debug.Log("Ast Spawn loc : " + location);

        return location;

    }


    // Adds an asteroid to the game
    private void AddEnemy()
    {
        // If we already have the maximum number of enemies, do nothing.
        if (m_CurrentAsteroids >= MAX_ASTEROIDS) return;

        // Otherwise, add an asteroid to the game.
        Instantiate(m_AsteroidPrefab, spawnLocation(), Quaternion.identity);
        m_CurrentAsteroids += 1;
    }

    public void AddScore(int scoreAdded)
    {
        score += scoreAdded;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void SubtractHealth(int healthSubtracted)
    {
        healthPoints -= healthSubtracted;
        UpdateHealth();
    }

    void UpdateHealth()
    {
        healthText.text = "Health: " + healthPoints;
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over. Thanks for playing!!!";
        updateRestartText();
        gameover = true;
    }

    public void updateRestartText()
    {
        restartText.text = "Please hold p to restart!";
    }


    bool PlayerOffScreen()
    {
        Vector3 playerScreenPosition = Camera.main.WorldToScreenPoint(m_player.transform.position);

        if (playerScreenPosition.y >= Screen.height || playerScreenPosition.y <= 0 || playerScreenPosition.x >= Screen.width || playerScreenPosition.x <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

}

