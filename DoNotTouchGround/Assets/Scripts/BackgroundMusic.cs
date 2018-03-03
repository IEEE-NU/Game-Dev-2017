using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusic : MonoBehaviour
{

    public static BackgroundMusic instance;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void LateUpdate()
    {
        Debug.Log("Scene name is: " + Camera.main.gameObject.scene.name);
        if (Camera.main.gameObject.scene.name == "MainScene")
        {
            Destroy(gameObject);
        }
    }
}
