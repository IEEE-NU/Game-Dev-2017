using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    // Singleton Design Pattern
    public static GameManager Instance;

    // Modulate type of enemy of spawn
    [SerializeField] private GameObject m_EnemyPrefab;

    // Max enemies for debugging intially ***
    private const int MAX_ENEMIES = 5;

    // Current amount of enemies in the game
	private int m_CurrentEnemies = 0;

    // Time since the last enemy was spawned
    private float m_TimeSinceLastEnemy = 0;

    // How frequently an enemy should spawn
    private float m_AddEnemyTime = 10;

    // Resets the game such that there is only one enemy
    public void ResetGame()
    {
        foreach (var e in FindObjectsOfType<Enemy>())
        {
            Destroy(e.gameObject);
        }
        m_CurrentEnemies = 0;
        m_TimeSinceLastEnemy = 0;
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
		 * Increment time since last enemy by the time that has elapsed since
		 * the last frame.
		 */
        m_TimeSinceLastEnemy += Time.deltaTime;

        // If the time since the last enemy is less than the spawn interval, do nothing.
        if (m_TimeSinceLastEnemy <= m_AddEnemyTime) return;

        // Otherwise, add an enemy and reset the timer!
        m_TimeSinceLastEnemy = 0;
        AddEnemy();
    }

    // Return random vector3 location to spawn asteroids
    private Vector3 randomLocation()
    {
        // To be done;
    }


    // Adds an enemy to the game
    private void AddEnemy()
    {
        // If we already have the maximum number of enemies, do nothing.
        if (m_CurrentEnemies >= MaxEnemies) return;

        // Otherwise, add an enemy to the game.
        Instantiate(m_EnemyPrefab, new Vector3(0, 2, 0), Quaternion.identity);
        m_CurrentEnemies += 1;
    }
}
