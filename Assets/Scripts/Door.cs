using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    [SerializeField] Button button;

    private BoxCollider2D doorCollider;

    private void Start() {

        doorCollider = GetComponent<BoxCollider2D>();

    }

    void Update() {

        if (button.getIsOn()) {
            doorCollider.isTrigger = true;
            button.resetButton();
        }

    }

}
