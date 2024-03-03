using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class Player : Entity
{
    private int level;
    private float currentLevelExperince;
    private float experienceToLevel;
    
    
    public Slider expBar;
    public Text expText;
    
    private void Start()
    {
        LevelUp();
        expBar.maxValue = experienceToLevel;
        expBar.value = currentLevelExperince;
        expText.text = "Level:" + level;

    }
    

    public void AddExperience(float exp)
    {
        currentLevelExperince += exp;
        if (currentLevelExperince >= experienceToLevel)
        {
            currentLevelExperince -= experienceToLevel;
            LevelUp();
        }

        expBar.maxValue = experienceToLevel;
        expBar.value = currentLevelExperince;
        expText.text = "Level: " + level;
        //   Debug.Log("Exp: " + currentLevelExperince + "      Level: " + level);
    }

    private void LevelUp()
    {
        level++;
        experienceToLevel = level * 50 + Mathf.Pow(level * 2,2);
        
        AddExperience((0));
    }
    
}
