using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void PlayGame()
    {
		Debug.Log ("Trying to play game");
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

	public void goToStory()
	{
		Debug.Log ("Trying to load story");
		SceneManager.LoadScene("StoryMenu");
	}

    public void goToSettings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void goToMenu()
	{
		SceneManager.LoadScene("StartMenu");
	}

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
	
}
