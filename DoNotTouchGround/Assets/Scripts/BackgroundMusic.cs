using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{

    public static BackgroundMusic instance;

    void Awake()
    {
    //removed to get different music in different scences
        //DontDestroyOnLoad(gameObject);

        //if (instance == null)
        //{
        //    instance = this;
        //}
        //else
        //{
        //    Destroy(gameObject);
        //    return;
        //}
    }
}
