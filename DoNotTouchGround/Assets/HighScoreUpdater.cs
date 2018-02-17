using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScoreUpdater : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI highScore;

	// Use this for initialization
	void Start () {
		highScore.SetText("High Score: " + PlayerPrefs.GetInt("HighScore", 0));
	}

    public void ResetHighScore()
    {
        PlayerPrefs.DeleteKey("HighScore");
        highScore.SetText("High Score: " + PlayerPrefs.GetInt("HighScore", 0));
    }
}
