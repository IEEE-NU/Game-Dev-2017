using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    // Singleton Design Pattern
    public static GameManager Instance;

    //This is Main Camera in the scene
    Camera m_MainCamera;

    // Modulate type of asteroid of spawn
    [SerializeField] private GameObject m_AsteroidPrefab;

    // Max enemies for debugging intially ***
    private const int MAX_ASTEROIDS = 5;

    // Current amount of enemies in the game
	private int m_CurrentAsteroids = 0;

    // Time since the last asteroid was spawned
    private float m_TimeSinceLastAsteroid = 0;

    // How frequently an asteroid should spawn
    private float m_AddAsteroidTime = 2;

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
        float height = m_MainCamera.orthographicSize;
        float width = height * m_MainCamera.aspect;
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
