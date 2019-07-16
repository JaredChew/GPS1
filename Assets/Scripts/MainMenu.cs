using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class MainMenu : MonoBehaviour {

    //[SerializeField] private bool debug = false;

    [SerializeField] private Global.Scenes sceneToLoad;

    [SerializeField] private GameObject optionMenu;
    [SerializeField] private GameObject continueMenu;

    public void newGame() {

        if(File.Exists(Application.persistentDataPath + Global.saveFileName)) {
            File.Delete(Application.persistentDataPath + Global.saveFileName);
        }

        //Time.timeScale = 1f;
        SceneManager.LoadScene((int)sceneToLoad);

    }

    public void continueGame() {

        if (File.Exists(Application.persistentDataPath + Global.saveFileName)) {
            //Time.timeScale = 1f;
            SceneManager.LoadScene((int)sceneToLoad);
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

    public void exitGame() {

        Application.Quit();

    }

    //sound
    private void Awake()
    {
        
        Global.audiomanager.stopAllSFX();
        Global.audiomanager.getBGM("pause_screen").stop();
        Global.audiomanager.getBGM("main_BGM").stop();
        Global.audiomanager.getBGM("main_menu").play();
    }
}
