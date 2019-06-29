using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    //[SerializeField] private bool debug = false;

    public void PlayGame() {

        SceneManager.LoadScene((int)Global.Scenes.demo);

    }

    public void ExitGame() {

        Application.Quit();

    }

}
