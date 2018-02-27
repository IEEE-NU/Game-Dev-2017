using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

	public enum SpawnState { SPAWNING, WAITING, COUNTING };

	[System.Serializable]
	public class Wave
	{
		public string name;
		public Transform enemy;
		public int count;
		public float rate;
	}

	public Wave[] waves;
	private int nextWave = 0;

	public float timeBetweenWaves = 5f;
	public float waveCountdown;

	private float searchCountdown = 1f;

	private SpawnState state = SpawnState.COUNTING;

	// Use this for initialization
	void Start ()
	{
		waveCountdown = timeBetweenWaves;
	}

	// Update is called once per frame
	void Update ()
	{

		if (state == SpawnState.WAITING)
		{
			// Check if enemies are still alive
			if (!EnemyIsAlive())
			{
				// Begin a new round
				WaveCompleted();
			}
			else
			{
				return;
			}
		}

		if (waveCountdown <= 0)
		{
				if (state != SpawnState.SPAWNING)
				{
					// Start spawning wave
					StartCoroutine( SpawnWave ( waves[nextWave] ) );
				}
		}
		else
		{
			waveCountdown -= Time.deltaTime;
		}
	}

	void WaveCompleted()
	{
		Debug.Log("Wave completed!");

		state = SpawnState.COUNTING;
		waveCountdown = timeBetweenWaves;

		if (nextWave + 1 > waves.Length - 1)
		{
			nextWave = 0;
			Debug.Log("ALL WAVES COMPLETE! Looping...");
			// can add multiplier, add game complete screen, add new scene etc..
		}
		else
		{
			nextWave++;
		}
	}

	bool EnemyIsAlive()
	{
		searchCountdown -= Time.deltaTime;
		if (searchCountdown <= 0f)
		{
			searchCountdown = 1f;
			if (GameObject.FindGameObjectWithTag("Asteroid") == null)
			{
				return false;
			}
		}

		return true;
	}

	IEnumerator SpawnWave(Wave _wave)
	{
		Debug.Log("Spawning Wave: " + _wave.name);
		state = SpawnState.SPAWNING;

		// Spawn
		for (int i = 0; i < _wave.count; i++)
		{
			SpawnEnemy(_wave.enemy);
			yield return new WaitForSeconds( 1f/_wave.rate );
		}

		state = SpawnState.WAITING;

		yield break;
	}

	void SpawnEnemy (Transform _enemy)
	{
		Debug.Log("Spawning Enemy: " + _enemy.name);
		// Spawn _enemy
		Instantiate (_enemy, SpawnLocation(), transform.rotation);
	}

	// Return random vector3 values to spawn asteroids
	private Vector3 SpawnLocation()
	{
			float x, y, z;
			float height = 165f; //m_MainCamera.orthographicSize;
			float width = 265f; //height * m_MainCamera.aspect;
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
}
