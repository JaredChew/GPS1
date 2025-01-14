﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class MainMenu : MonoBehaviour {

    //[SerializeField] private bool debug = false;

    [SerializeField] private Global.Scenes sceneToLoad;

    [SerializeField] private GameObject optionMenu;
    [SerializeField] private GameObject continueMenu;
    [SerializeField] private GameObject credits;
    

    public void newGame() {

        if(File.Exists(Application.persistentDataPath + Global.saveFileName)) {
            File.Delete(Application.persistentDataPath + Global.saveFileName);
        }

        //Time.timeScale = 1f;
        SceneManager.LoadScene((int)sceneToLoad);
        //sound
        Global.audiomanager.getBGM("main_menu").stop();
        Global.audiomanager.getBGM("main_BGM").play();
    }

    public void continueGame() {

        if (File.Exists(Application.persistentDataPath + Global.saveFileName)) {
            //Time.timeScale = 1f;
            SceneManager.LoadScene((int)sceneToLoad);
            //sound
            Global.audiomanager.getBGM("main_menu").stop();
            Global.audiomanager.getBGM("main_BGM").play();
        }
        else if (!(File.Exists(Application.persistentDataPath + Global.saveFileName)))
        {
            continueMenu.SetActive(true);
        }

    }

    public void optionsMenu()
    {
        optionMenu.SetActive(true);
    }

    public void credit()
    {
        credits.SetActive(true);
        //sound
        Global.audiomanager.stopAllSFX();
        Global.audiomanager.getBGM("pause_screen").stop();
        Global.audiomanager.getBGM("main_BGM").stop();
        Global.audiomanager.getBGM("main_menu").stop();
        Global.audiomanager.getBGM("win_screen").play();
    }

    public void exitGame() {

        Application.Quit();

    }

    //sound
    private void Start()
    {
        
        Global.audiomanager.stopAllSFX();
        Global.audiomanager.getBGM("pause_screen").stop();
        Global.audiomanager.getBGM("main_BGM").stop();
        Global.audiomanager.getBGM("main_menu").play();

        
    }
}
