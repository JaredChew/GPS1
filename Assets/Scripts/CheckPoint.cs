using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

    [SerializeField] private Global.CheckpointLocation currentCheckpointLocation;

    private CheckpointManager checkpointManager;

    private bool indicatorActive = false;

    private void Start() {

        checkpointManager = transform.root.GetComponent<CheckpointManager>();

    }

    void OnTriggerEnter2D(Collider2D other) {
        
        if (Global.gameManager.getLastCheckpointAt() != currentCheckpointLocation) {
            Global.gameManager.saveGame();
            indicatorActive = true;
            //indicator set animation to change colour
            checkpointManager.updateIndicator();
        }
        
    }

    public void resetIndicator() {

        indicatorActive = false;

        //reset indicator animation

    }

    public bool getIsIndicatorActive() {
        return indicatorActive;
    }

    public Global.CheckpointLocation getCurrentCheckpointLocation() {
        return currentCheckpointLocation;
    }

    public void activate() {
        indicatorActive = true;
        //indicator set animation to change colour
    }

}
