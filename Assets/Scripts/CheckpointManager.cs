using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour {

    [SerializeField] private CheckPoint[] checkpoint;

    private void Start() {

        loadUnloadCheckpoint();

    }

    public void loadUnloadCheckpoint() {

        for (int i = 0; i < checkpoint.Length; i++) {

            if(checkpoint[i].getAreaConnect1() == Global.gameManager.getCurrentArea() || checkpoint[i].getAreaConnect2() == Global.gameManager.getCurrentArea()) {
                checkpoint[i].gameObject.SetActive(true);
                continue;
            }

            checkpoint[i].gameObject.SetActive(false);

        }

    }

    public void updateIndicator() {

        for (int i = 0; i < checkpoint.Length; i++) {

            if (checkpoint[i].getIsIndicatorActive()) {
                checkpoint[i].deactivate();
                break;
            }

        }

    }
    
}
