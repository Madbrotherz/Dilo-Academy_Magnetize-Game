﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{

    public void loadScene(string sceneName) 
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
    }
}
