﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public int level = 0;

    private void Awake() {
        DontDestroyOnLoad(this);
    }

    public void NextLevel() {
        level++;
        Debug.Log("Level_" + level);
        SceneManager.LoadScene("Level_Hard_" + level, LoadSceneMode.Single);
    }

    public void RestartLevel() {
       SceneManager.LoadScene("Level_Hard_" + level, LoadSceneMode.Single);
    }
}
