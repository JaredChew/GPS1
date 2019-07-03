using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour {

    [SerializeField] private GameObject inGameMenuUI;
    [SerializeField] private GameObject deathUI;
    [SerializeField] private GameObject mapUI;

    private bool gameIsPause = false;
    private bool gameMapIsOn = false;

    // Update is called once per frame
    void Update() {

        if (!Global.gameManager.getIsPlayerDead()) {
            pauseGame();
            mapUIDisplay();
        }
        else {
            deathMenuDisplay();
        }

    }

    private void deathMenuDisplay() {

        Time.timeScale = 0f;
        gameIsPause = true;
        deathUI.SetActive(true);

    }

    private void pauseGame() {

        if (Input.GetButtonDown(Global.controlsPause)) {

            if (gameIsPause) { resume(); }
            else { pause(); }

        }

    }

    public void continueFromLastCheckpoint() {
        deathUI.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void resume() {
        inGameMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPause = false;
    }

    void pause() {
        inGameMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPause = true;
    }

    public void loadMainMenu() {
        gameIsPause = false;
        SceneManager.LoadScene((int)Global.Scenes.mainMenu);
    }

    public void exitGame() {
        Time.timeScale = 1f;
        Application.Quit();
    }

    private void mapUIDisplay()
    {
        if (Input.GetKey(KeyCode.P))
        {
            if (gameMapIsOn == false)
            {
                mapUI.SetActive(true);
                gameMapIsOn = true;
            }
            else if (gameMapIsOn)
            {
                gameMapIsOn = false;
                mapUI.SetActive(false);
            }
        }
            
    }

}
