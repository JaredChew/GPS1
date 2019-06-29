using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour {

    [SerializeField] private GameObject inGameMenuUI;
    [SerializeField] private GameObject deathUI;

    private bool GameIsPause = false;

    // Update is called once per frame
    void Update() {

        if (!Global.gameManager.getIsPlayerDead()) {
            pauseGame();
        }
        else {
            deathMenuDisplay();
        }

    }

    private void deathMenuDisplay() {

        Time.timeScale = 0f;
        deathUI.SetActive(true);

    }

    private void pauseGame() {

        if (Input.GetButtonDown(Global.controlsPause)) {

            if (GameIsPause) { Resume(); }
            else { Pause(); }

        }

    }

    public void continueFromLastCheckpoint() {
        inGameMenuUI.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Resume() {
        inGameMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPause = false;
    }

    void Pause() {
        inGameMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPause = true;
    }

    public void LoadMainMenu() {
        GameIsPause = false;
        SceneManager.LoadScene((int)Global.Scenes.mainMenu);
    }

    public void ExitGame() {
        Time.timeScale = 1f;
        Application.Quit();
    }

}
