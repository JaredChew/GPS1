using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField] private bool isNight = false;

    [SerializeField] private float dayNightCycle = 180.0f;

    [SerializeField] private Global.Stages currentStage = Global.Stages.area1;

    [SerializeField] private bool replaceOldData = false;

    //[SerializeField] private Light worldLight;
    [SerializeField] private Player player;

    PersistentData saveLoad;

    private Global.CheckpointLocation lastSaveLocation;

    private float currentTime = 0.0f;

    private void Awake() {

        if (Global.gameManager == null) {

            Global.gameManager = this;
            DontDestroyOnLoad(gameObject);

            return;

        }
        else if(replaceOldData) {

            //To be tested

            Global.gameManager.isNight = isNight;
            Global.gameManager.dayNightCycle = dayNightCycle;
            Global.gameManager.currentStage = currentStage;

            replaceOldData = false;

        }

        Destroy(gameObject);
        
    }

    // Start is called before the first frame update
    void Start() {

        saveLoad = new PersistentData();

        saveLoad.loadData();
        player.setPlayerPosition(saveLoad.getPlayerPosition());
        currentTime = saveLoad.getTime();

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

        player.transform.position = saveLoad.getPlayerPosition();

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

    public void destroy() {

        Destroy(gameObject);

    }

}
