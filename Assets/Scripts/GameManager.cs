using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour {

    [SerializeField] private bool isNight = false;

    [SerializeField] private float dayNightCycle = 180.0f;

    [SerializeField] private Player player;

    [SerializeField] private GameObject[] areas;

    private PersistentData saveLoad;

    private Global.CheckpointLocation lastSaveLocation;

    private Global.Areas currentArea;

    private float currentTime = 0.0f;

    private bool gameIsPaused = false;

    private void Awake() {

        Global.gameManager = this;

        if (Time.timeScale == 0f) {
            Time.timeScale = 1f;
        }

        saveLoad = new PersistentData();
        saveLoad.loadData();

        currentTime = saveLoad.getTime();

    }

    // Start is called before the first frame update
    void Start() {

        for (int i = 0; i < areas.Length; i++) {

            areas[i].SetActive(false);

        }

        if (saveLoad.isDataLoadable()) {

            player.setPlayerPosition(saveLoad.getPlayerPosition());
            player.setFacingDirection(saveLoad.getPlayerFacingDirection());

            for (int i = 0; i < Enum.GetNames(typeof(Global.BoxAbilities)).Length; i++) {

                if (saveLoad.getAbility()[i]) {
                    player.upgradeBox((Global.BoxAbilities)i);
                }

            }

            lastSaveLocation = saveLoad.getCurrentCheckPoint();
            currentArea = saveLoad.getCurrentArea();

        }

        loadNewArea(currentArea);

    }

    // Update is called once per frame
    void Update() {

        autoDayNightCycle();

        // !!! Delete after audio is implemented !!! //
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

    private void loadNewArea(Global.Areas newArea) {

        areas[(int)newArea].SetActive(true);

    }

    private void unloadOldArea() {

        areas[(int)currentArea].SetActive(false);

    }

    public void saveGame() {

        saveLoad.saveData(player.getPosition(), player.getFacingDirection(), player.getBoxUpgradeStatus(), currentTime, lastSaveLocation, currentArea);

    }

    public bool isPowerUpTaken(Global.BoxAbilities ability) {
        return saveLoad.isDataLoadable() ? saveLoad.getAbility()[(int)ability] : false;
    }

    public void setGamePausedState(bool gameIsPaused) {

        this.gameIsPaused = gameIsPaused;

        if(gameIsPaused) { Time.timeScale = 0f; }
        else { Time.timeScale = 1f; }

    }

    public void setCurrentCheckpoint(Global.CheckpointLocation lastSaveLocation) {
        this.lastSaveLocation = lastSaveLocation;
    }

    public Global.CheckpointLocation getLastCheckpointAt() {
        return lastSaveLocation;
    }

    public void transitionToNewArea(Global.Areas currentArea) {

        loadNewArea(currentArea);
        unloadOldArea();

        this.currentArea = currentArea;

    }

    public Global.Areas getCurrentArea() {
        return currentArea;
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

    public void saveDoorStatus(int doorIndex, bool isOpen) {
        saveLoad.saveDoorStatus(doorIndex, isOpen);
    }

    public bool getDoorStaus(int doorIndex) {

        try { return saveLoad.getDoorStatus()[doorIndex]; }
        catch { return false; }

    }

    public void destroy() {
        Destroy(gameObject);
    }

}
