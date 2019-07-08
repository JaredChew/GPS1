using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    [SerializeField] int doorIndex;

    [SerializeField] Button button;

    private BoxCollider2D doorCollider;

    private void Start() {

        doorCollider = GetComponent<BoxCollider2D>();

        doorCollider.isTrigger = Global.gameManager.getDoorStaus(doorIndex);

    }

    void Update() {

        if (button.getIsOn() && !doorCollider.isTrigger) {
            doorCollider.isTrigger = true;
            Global.gameManager.saveDoorStatus(doorIndex, true);
            button.resetButton();
        }

    }

}
