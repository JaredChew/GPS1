using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour {

    [SerializeField] private bool isNight = false;

    [SerializeField] private float dayNightCycle = 180.0f;

    //[SerializeField] private Light worldLight;
    [SerializeField] private Player player;

    PersistentData saveLoad;

    private Global.CheckpointLocation lastSaveLocation;

    private float currentTime = 0.0f;

    private bool gameIsPaused = false;

    private void Awake() {

        Global.gameManager = this;

        if (Time.timeScale == 0f) {
            Time.timeScale = 1f;
        }

    }

    // Start is called before the first frame update
    void Start() {

        saveLoad = new PersistentData();

        if (saveLoad.loadData()) {

            player.setPlayerPosition(saveLoad.getPlayerPosition());

            for (int i = 0; i < Enum.GetNames(typeof(Global.BoxAbilities)).Length; i++) {

                if (saveLoad.getAbility()[i]) {
                        player.upgradeBox((Global.BoxAbilities)i);
                }

            }

            currentTime = saveLoad.getTime();
            lastSaveLocation = saveLoad.getSavedCheckpoint();

        }

        //worldLight.gameObject.SetActive(!isNight);

    }

    // Update is called once per frame
    void Update() {

        autoDayNightCycle();

        if(Input.GetKeyDown(KeyCode.M)) { //Sound test
            Global.audiomanager.getSFX(Global.audioSFX_Test).play();
        }

    }

    private void FixedUpdate() {

        currentTime += Time.deltaTime;

    }

    private void autoDayNightCycle() {

        if(currentTime >= dayNightCycle) {
            isNight = !isNight;
            currentTime = 0.0f;
        }

    }

    private void loadGame() {

        saveLoad.loadData();

        player.setPlayerPosition(saveLoad.getPlayerPosition());

        for (int i = 0; i < saveLoad.getAbility().Length; i++) {

            if (saveLoad.getAbility()[i]) {
                player.upgradeBox((Global.BoxAbilities)i);
            }

        }

        currentTime = saveLoad.getTime();

        lastSaveLocation = saveLoad.getSavedCheckpoint();

    }

    public void saveGame() {

        saveLoad.saveData(player.getPosition(), player.getBoxUpgradeStatus(), currentTime, lastSaveLocation);

    }

    public void setGamePausedState(bool gameIsPaused) {
        this.gameIsPaused = gameIsPaused;

        if(gameIsPaused) {
            Time.timeScale = 0f;
        }
        else {
            Time.timeScale = 1f;
        }

    }

    public Global.CheckpointLocation getLastCheckpointAt() {

        return lastSaveLocation;

    }

    public bool getIsNight() {

        return isNight;

    }

    public bool getIsPlayerDead() {

        return player.getIsDead();

    }

    public float getTime() {
        return currentTime;
    }

    public float getMaxTimeCycle() {
        return dayNightCycle;
    }

    public bool getIsGamePaused() {
        return gameIsPaused;
    }

    public void destroy() {

        Destroy(gameObject);

    }

}
