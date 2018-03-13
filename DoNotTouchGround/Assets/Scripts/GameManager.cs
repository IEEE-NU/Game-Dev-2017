using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;

public class GameManager : MonoBehaviour {
    // Singleton Design Pattern
    public static GameManager Instance;

    //This is Main Camera in the scene
    Camera m_MainCamera;

    //score component
    private int score = 0;

    //score component
    public int healthPoints = 1500;

    //text components for the HUD
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI restartText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI warningText;
    public TextMeshProUGUI highScore;

    //keeps track if game is over
    public bool gameover = false;

    // Modulate type of asteroid of spawn
    [SerializeField] private GameObject m_AsteroidPrefab;
    [SerializeField] private GameObject m_AsteroidPrefabMidSized;
	[SerializeField] private GameObject m_AsteroidPrefabBigSized;
	[SerializeField] private GameObject m_MotherShipPrefab;
	IList <GameObject> enemies = new List <GameObject>();
	IList <int> numEnemy= new List <int>();
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
	[SerializeField] private float spawnTime;
    // Resets the game such that there is only one asteroid
    public void ResetGame()
    {
        foreach (var e in FindObjectsOfType<Asteroid>())
        {
            Destroy(e.gameObject);
        }
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

		enemies.Add(m_AsteroidPrefab);
		enemies.Add(m_AsteroidPrefabMidSized);
		enemies.Add(m_AsteroidPrefabBigSized);
		numEnemy.Add(12);
		numEnemy.Add(6);
		numEnemy.Add(3);
		enemies.Add (m_MotherShipPrefab);
		numEnemy.Add (1);
    }

    // Called once per frame
   /* void Update () {

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
      /*  m_TimeSinceLastAsteroid += Time.deltaTime;

        // If the time since the last asteroid is less than the spawn interval, do nothing.
        if (m_TimeSinceLastAsteroid <= m_AddAsteroidTime) return; //BE CAREFUL OF THIS RETURN!!! IT WAS SCREWING UP MY OTHER CALLS

        // Otherwise, add an asteroid and reset the timer!
        m_TimeSinceLastAsteroid = 0;
        AddEnemy();



    }
	*/
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
	}

/*
	private void SpawnObj (IList<GameObject> enemies, IList<int> numEnemy, int waveNo)
	{
		Debug.Log ("Why are you not calling me?");
		int sizeEnemies = 0;
		foreach (GameObject e in enemies) sizeEnemies++;
		int numToSpawn = 0;
		Debug.Log ("Size enemies: " + sizeEnemies);
		for(int i=0;i<sizeEnemies;i++)
		{
			if (numEnemy [i] == 12)
				numToSpawn = 12 * waveNo - 3;
			else if (numEnemy [i] == 6)
				numToSpawn = 6 * waveNo - 3;
			else if (numEnemy [i] == 3)
				numToSpawn = 3 * waveNo - 2;
			else if (numEnemy [i] == 1)
				numToSpawn = 1 * waveNo - 3;
			for (int j = 0; j <= numToSpawn; j++)
			{
				Vector3 spawnPos = SpawnLocation ();
				Instantiate (enemies [i], spawnPos, Quaternion.identity);
			}
		//yield return new WaitForSeconds(1);
		}
		//put time here for the function to wait for the new enemy.
		//figure out how waitforseconds is going to work cause it doesnt like the yield before it
	}
	private void Wave()
	{
		//Debug.Log ("Wave is being called");
		int waveNo = 1;
		GameObject[] gos=GameObject.FindGameObjectsWithTag("Asteroid");
		//Debug.Log ("Num objs with Tag: " + gos.Length);
		if (gos.Length == 0) {
			waveNo++;
			//Debug.Log ("Calling spawn");
			SpawnObj (enemies, numEnemy, waveNo);
		}
	}
	//findAllObjectsOfType


    /*private void SpawnSingle(GameObject enemy, int amount)
    {
      for (int i = 0; i < amount; i++)
      {
        Vector3 spawnPos = SpawnLocation ();
        Instantiate(enemy, spawnPos, Quaternion.identity);
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
*/
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
        gameOverText.text = "Game Over"; //this is a bad way to do things. Enable and disable them in the future instead of assigning new values
        updateRestartText();
        gameover = true;
    }

    public void updateRestartText()
    {
        restartText.text = "Press 'P' to restart!";
    }

    //public void updateOverheatText(float heat)
    //{
    //    overheatText.text = "Overheat: " + Mathf.Round(heat);
    //    if (heat == 100f)
    //    {
    //        overheatText.color = new Color(1f, 0.0f, 0.0f);
    //    }
    //    else
    //    {
    //        overheatText.color = new Color(1f, 1f, 1f);
    //    }
    //}

    public void updateOverheatBar()
    {
        if (!overheatScript.isOverheated())
        {
            barScript.BarHandler(0, 1, overheatScript.getHeat() / overheatScript.getMaxHeat());
        }
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
