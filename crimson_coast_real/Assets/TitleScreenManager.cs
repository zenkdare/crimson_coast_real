﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{

	public string mainScene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToGame(){
    	SceneManager.LoadScene(mainScene, LoadSceneMode.Single);
    }

    public void QuitGame(){
        Application.Quit();
    }
}
