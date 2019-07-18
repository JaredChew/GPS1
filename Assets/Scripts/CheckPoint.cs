using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

    [SerializeField] private Global.CheckpointLocation checkpointLocation;

    private bool indicatorActive = false;

    private void Start() {

        if(Global.gameManager.getLastCheckpointAt() == checkpointLocation) {
            indicatorActive = true;
            //play animation
        }

    }

    private void Update() {

        if (Global.gameManager.getLastCheckpointAt() != checkpointLocation && indicatorActive) {
            indicatorActive = false;
            //play animation
        }

    }

    private void OnTriggerExit2D(Collider2D collision) {

        if (collision.CompareTag(Global.tagPlayer)) {

            //sound
            FindObjectOfType<AudioManager>().getSFX("checkpoint_sound");

            if (Global.gameManager.getLastCheckpointAt() != checkpointLocation) {
                Global.gameManager.setCurrentCheckpoint(checkpointLocation);
                indicatorActive = true;
                //play animation
            }

            Global.gameManager.saveGame();

        }

    }

    public bool getIsIndicatorActive() {
        return indicatorActive;
    }

    public Global.CheckpointLocation getCheckpointLocation() {
        return checkpointLocation;
    }

}
