﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overheat : MonoBehaviour
{
    [SerializeField] private float currentHeat = 0;
    [SerializeField] private float coolRate = 20;
    [SerializeField] private float maxHeat = 200;
    [SerializeField] private float minHeat = 0;
    //[SerializeField] private float heatToAdd = 1;

    //[SerializeField] private BarScript barScript;

    // Update is called once per frame
    void Update ()
	{
	    currentHeat = Mathf.Max( (float) (currentHeat - Time.deltaTime * coolRate), 0);
	}

    public void AddHeat(float heatToAdd)
    {
        currentHeat += heatToAdd;
        currentHeat = Mathf.Min(currentHeat, maxHeat);
    }

    public bool isOverheated()
    {
        if (currentHeat >= maxHeat)
            return true;
        else
            return false;
        
    }

    public float getHeat()
    {
        return Mathf.Min(this.currentHeat, maxHeat);
    }

    public float getMaxHeat()
    {
        return  maxHeat;
    }

    public float getMinHeat()
    {
        return  minHeat;
    }

    public void resetHeat()
    {
        currentHeat = 0;
    }
}
