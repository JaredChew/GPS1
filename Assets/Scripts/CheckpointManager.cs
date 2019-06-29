using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour {

    [SerializeField] private CheckPoint[] checkpoint;

    private void Start() {

        for (int i = 0; i < checkpoint.Length; i++) {

            if (checkpoint[i].getCurrentCheckpointLocation() == Global.gameManager.getLastCheckpointAt()) {
                checkpoint[i].activate();
                return;
            }

        }

    }

    public void updateIndicator() {

        for (int i = 0; i < checkpoint.Length; i++) {

            if (checkpoint[i].getIsIndicatorActive()) {
                checkpoint[i].resetIndicator();
                break;
            }

        }

    }
    
}
