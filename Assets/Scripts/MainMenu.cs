using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class MainMenu : MonoBehaviour {

    //[SerializeField] private bool debug = false;
    [SerializeField] private Global.Scenes sceneToLoad;

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

    }

    public void exitGame() {

        Application.Quit();

    }

}
