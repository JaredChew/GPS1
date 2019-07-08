using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

    [SerializeField] private Global.CheckpointLocation checkpointLocation;

    [SerializeField] private Global.Areas areaConnect1;
    [SerializeField] private Global.Areas areaConnect2;

    private CheckpointManager checkpointManager;

    private bool indicatorActive = false;

    private void Start() {

        checkpointManager = transform.parent.GetComponent<CheckpointManager>();

        if(Global.gameManager.getLastCheckpointAt() == checkpointLocation) {
            activate();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.CompareTag(Global.tagPlayer)) {

            if (Global.gameManager.getCurrentArea() == areaConnect1) {
                Global.gameManager.transitionToNewArea(areaConnect2);
            }
            else if (Global.gameManager.getCurrentArea() == areaConnect2) {
                Global.gameManager.transitionToNewArea(areaConnect1);
            }

            checkpointManager.loadUnloadCheckpoint();

        }

    }

    void OnTriggerExit2D(Collider2D collision) {

        if (collision.CompareTag(Global.tagPlayer)) {

            Global.gameManager.saveGame();

            if (Global.gameManager.getLastCheckpointAt() != checkpointLocation) {
                Global.gameManager.setCurrentCheckpoint(checkpointLocation);
                checkpointManager.updateIndicator();
                activate();
            }

        }

    }

    public void activate() {

        indicatorActive = true;

        //indicator set animation to change colour

    }

    public void deactivate() {

        indicatorActive = false;

        //reset indicator animation

    }

    public bool getIsIndicatorActive() {
        return indicatorActive;
    }

    public Global.CheckpointLocation getCheckpointLocation() {
        return checkpointLocation;
    }

    public Global.Areas getAreaConnect1() {
        return areaConnect1;
    }

    public Global.Areas getAreaConnect2() {
        return areaConnect2;
    }

}
