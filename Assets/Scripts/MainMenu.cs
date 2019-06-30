using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class MainMenu : MonoBehaviour {

    //[SerializeField] private bool debug = false;

    public void newGame() {

        if(File.Exists(Application.persistentDataPath + Global.saveFileName)) {
            File.Delete(Application.persistentDataPath + Global.saveFileName);
        }

        //Time.timeScale = 1f;
        SceneManager.LoadScene((int)Global.Scenes.demo);

    }

    public void continueGame() {

        if (File.Exists(Application.persistentDataPath + Global.saveFileName)) {
            //Time.timeScale = 1f;
            SceneManager.LoadScene((int)Global.Scenes.demo);
        }

    }

    public void exitGame() {

        Application.Quit();

    }

}
