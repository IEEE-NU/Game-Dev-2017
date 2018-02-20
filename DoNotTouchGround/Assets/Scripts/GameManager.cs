using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
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
    public Text overheatText;
    public Text warningText;

    public TextMeshProUGUI highScore;

    //keeps track if game is over
    private bool gameover = false;

    // Modulate type of asteroid of spawn
    [SerializeField] private GameObject m_AsteroidPrefab;
    [SerializeField] private GameObject m_AsteroidPrefabMidSized;
	[SerializeField] private GameObject m_AsteroidPrefabBigSized;


	List<GameObject> enemies=new List<GameObject>();
	List<int> numEnemy=new List<int>();
	/*enemies.add (m_AsteroidPrefab);
	enemies.add (m_AsteroidPrefabMidSized);
	enemies.add (m_AsteroidPrefabBigSized);
	numEnemy.Add(12);
	numEnemy.Add(6);
	numEnemy.Add(3);*/
    // get player object
    [SerializeField] private GameObject m_player;

    //get the barScript so that we can change the bar fill
    [SerializeField] private BarScript barScript;

    //get the barScript so that we can change the bar fill
    [SerializeField] private Overheat overheatScript;

    // Max enemies for debugging intially ***
    private const int MAX_ASTEROIDS = 100;

    // Current amount of enemies in the game
	private int m_CurrentAsteroids = 0;

    // Time since the last asteroid was spawned
    private float m_TimeSinceLastAsteroid = 0;

    // How frequently an asteroid should spawn
    [SerializeField] private float m_AddAsteroidTime;
    [SerializeField] private float m_AddAsteroidTimeMidSized;
	[SerializeField] private float m_AddAsteroidTimeBigSized;

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
        scoreText.text = "Score: " + score;
        healthText.text = "Health: " + healthPoints;
        warningText.text = "";

        //Testing Line that resets HighScore
        //PlayerPrefs.DeleteKey("HighScore");
        highScore.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0);
    }

    // Called once per frame
    void Update () {

        //Updates the highscore.
        UpdateHighScore();

        //updates the overheat bar
        updateOverheatBar();

        //check if player is off the screen. If so, deactivate it and call gameover
        if (PlayerOffScreen())
        {
            Debug.Log("The Player is off screen");
            m_player.SetActive(false);
            GameOver();
        }

        // If player too close to edge, flash warning message
        if (PlayerCloseToEdge())
        {
            updateWarningText();
        }
        // If player is not too close to edge/no longer close to edge, clear text from screen
        else
        {
            warningText.text = "";
        }


        //enable restart if game has gone to gameover
        //some issues with using GetKeyDown. Maybe cause its not checking on the right frames?
        if (gameover)
        {
            //Debug.Log("Game over state is true");

            // Reset warning text so it's not in the way
            warningText.text = "";

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
        if (m_TimeSinceLastAsteroid <= m_AddAsteroidTime) return; //BE CAREFUL OF THIS RETURN!!! IT WAS SCREWING UP MY OTHER CALLS

        // Otherwise, add an asteroid and reset the timer!
        m_TimeSinceLastAsteroid = 0;
        AddEnemy();



    }

    // Return random vector3 values to spawn asteroids
    private Vector3 SpawnLocation()
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

        //Debug.Log("Ast Spawn loc : " + location);

        return location;

    }


    // Adds an asteroid to the game
    private void AddEnemy()
    {
        // If we already have the maximum number of enemies, do nothing.
        if (m_CurrentAsteroids >= MAX_ASTEROIDS) return;

        // Otherwise, add an asteroid to the game.
        GameObject temp = Instantiate(m_AsteroidPrefab, SpawnLocation(), Quaternion.identity);
        GameObject temp2 = Instantiate(m_AsteroidPrefabMidSized, SpawnLocation(), Quaternion.identity);//midsized
		GameObject temp3 =Instantiate(m_AsteroidPrefabBigSized, SpawnLocation(),Quaternion.identity);//bigsized
        Transform tempLoc = temp.GetComponent<Transform>();
        tempLoc.position = SpawnLocation(); // hack fix until reason behind is found why instantiate is not setting the given Vector3 object
        Transform tempLoc2 = temp2.GetComponent<Transform>();//hack fix for the midsized
        tempLoc2.position = SpawnLocation();
		Transform tempLoc3 = temp3.GetComponent<Transform> ();
		tempLoc3.position = SpawnLocation ();
        m_CurrentAsteroids += 1;
    }
	/*
	private void Spawn(List<GameObject> enemies, List<int> numEnemy)
	{
		int sizeEnemies = 0;
		foreach (GameObject e in enemies)
			sizeEnemies++;
		for(int i=0;i<sizeEnemies;i++)
		{
			Vector3 spawnPos = SpawnLocation ();
			Instantiate (enemies [i], spawnPos, Quaternion.identity);
		}
		//figure out how to grab values from the prefabs and move the time values to the prefabs.
		//int timeBetweenEnemies = enemies [1].timeForSpawn;
		//put time here for the function to wait for the new enemy.
		//figure out how waitforseconds is going to work cause it doesnt like the yield before it
		yield return WaitForSeconds(timeBetweenEnemies);
	}
*/

    private void SpawnSingle(GameObject enemy, int amount)
    {
      for (int i = 0; i < amount; i++)
      {
        Vector3 spawnPos = SpawnLocation ();
        Instantiate(enemy, spawnPos, Quaternion.identity);
        /*
        // access spawn time from given prefab
        if (enemy.GetComponent<Asteroid>().spawnTime != null)
        {
          // calculate milliseconds assuming input is in WaitForSeconds
          int waitTime = enemy.GetComponent<Asteroid>().spawnTime;
          waitTime = waitTime * 1000;
          Thread.Sleep(waitTime);
        }
        */
      }
    }

    private void SpawnMultiple(List<GameObject> enemies, List<int> numEnemy)
    {
      int currIndex = 0;
      foreach (GameObject e in enemies)
      {
        SpawnSingle(e, numEnemy[currIndex]);
        currIndex++;
      }
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

    void UpdateHighScore()
    {
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
            highScore.SetText("High Score: " + score);
        }
    }

    public void SetHealth(float newHealth)
    {
		healthPoints = (int)newHealth;
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

    public void updateOverheatText(float heat)
    {
        overheatText.text = "Overheat: " + Mathf.Round(heat);
        if (heat == 100f)
        {
            overheatText.color = new Color(1f, 0.0f, 0.0f);
        }
        else
        {
            overheatText.color = new Color(1f, 1f, 1f);
        }
    }

    public void updateOverheatBar()
    {
        //barScript.max = overheatScript.getMaxHeat();
        //barScript.min = overheatScript.getMinHeat();
        //barScript.currPercentage = overheatScript.getHeat() / overheatScript.getMaxHeat();

        barScript.BarHandler(0, 1, overheatScript.getHeat() / overheatScript.getMaxHeat());
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

    // If player comes within 60 units of screen edge, return true
    bool PlayerCloseToEdge()
    {
        Vector3 playerScreenPosition = Camera.main.WorldToScreenPoint(m_player.transform.position);

        if (Screen.height - playerScreenPosition.y <= 60 ||
                            playerScreenPosition.y <= 60 ||
             Screen.width - playerScreenPosition.x <= 60 ||
                            playerScreenPosition.x <= 60)
        {
            return true;
        }

        return false;

    }

    public void updateWarningText()
    {
        warningText.text = "WARNING: You are leaving the battlefield!";
    }



}
