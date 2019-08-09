using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour {

    [SerializeField] private GameObject inGameMenuUI;
    [SerializeField] private GameObject optionMenu;
    [SerializeField] private GameObject deathUI;

    // Update is called once per frame
    void Update() {

        if (!Global.gameManager.getIsPlayerDead()) {
            pauseGame();
        }
        else {
            deathMenuDisplay();
        }

    }

    private void pause() {

        inGameMenuUI.SetActive(true);
        Global.gameManager.setGamePausedState(true);

        // sound
        Global.audiomanager.getBGM("main_BGM").stop();
        Global.audiomanager.stopAllSFX();
        Global.audiomanager.getBGM("pause_screen").play();

    }

    private void pauseGame() {

        if (Input.GetButtonDown(Global.controlsPause)) {

            if (Global.gameManager.getIsGamePaused()) { resume();
                Global.audiomanager.getBGM("pause_screen").stop();
                Global.audiomanager.stopAllSFX();
                Global.audiomanager.getBGM("main_BGM").play();
            }
            else { pause(); }

        }

    }

    private void deathMenuDisplay() {

        Global.gameManager.setGamePausedState(true);
        deathUI.SetActive(true);

    }

    public void continueFromLastCheckpoint() {

        deathUI.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void resume() {

        inGameMenuUI.SetActive(false);
        optionMenu.SetActive(false);

        Global.gameManager.setGamePausedState(false);

    }

    public void loadMainMenu() {

        Global.gameManager.setGamePausedState(false);
        SceneManager.LoadScene((int)Global.Scenes.mainMenu);

    }

    public void optionsMenu() {

        if (Global.gameManager.getIsPlayerDead()) { deathUI.SetActive(false); }
        else { inGameMenuUI.SetActive(false); }

        optionMenu.SetActive(true);

    }

    public void exitOptionsMenu() {

        optionMenu.gameObject.SetActive(false);

        if (Global.gameManager.getIsPlayerDead()) {
            deathUI.SetActive(true);
            return;
        }

        inGameMenuUI.SetActive(true);

    }

    public void exitGame() {

        Application.Quit();

    }

}
