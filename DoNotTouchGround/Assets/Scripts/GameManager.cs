using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    // Singleton Design Pattern
    public static GameManager Instance;

    // Modulate type of asteroid of spawn
    [SerializeField] private GameObject m_AsteroidPrefab;

    // Max enemies for debugging intially ***
    private const int MAX_ASTEROIDS = 5;

    // Current amount of enemies in the game
	private int m_CurrentAsteroids = 0;

    // Time since the last asteroid was spawned
    private float m_TimeSinceLastAsteroid = 0;

    // How frequently an asteroid should spawn
    private float m_AddAsteroidTime = 10;

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
        // Set the singleton instance
        if (Instance == null)
            Instance = this;

        // Reset the game to its initial state
        ResetGame();
    }

    // Called once per frame
    void Update () {
        /*
		 * Increment time since last asteroid by the time that has elapsed since
		 * the last frame.
		 */
        m_TimeSinceLastAsteroid += Time.deltaTime;

        // If the time since the last asteroid is less than the spawn interval, do nothing.
        if (m_TimeSinceLastAsteroid <= m_AddAsteroidTime) return;

        // Otherwise, add an asteroid and reset the timer!
        m_TimeSinceLastAsteroid = 0;
        AddEnemy();
    }

    // Return random vector3 values to spawn asteroids
    private Vector3 spawnLocation()
    {
        float x, y, z;
        // Choose to spawn either in x or y bounds
        if (Random.Range(0.0f, 1.0f) > 0.5f) // > 0.5 spawn X else spawn Y
        {
            x = Random.Range(0.0f, 1.0f) * Screen.width;
            y = 0.0f;
            z = 0.0f;
        }
        else
        {
            x = 0.0f;
            y = Random.Range(0.0f, 1.0f) * Screen.height;
            z = 0.0f;
        }
        Vector3 location = new Vector3(x, y, z);
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
}
