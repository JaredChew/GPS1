using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    [SerializeField] private GameObject mapUI;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

        onOffMap();

    }

    private void onOffMap() {

        if (Input.GetButtonDown(Global.controlsMap) && !Global.gameManager.getIsGamePaused()) {

            mapUI.SetActive(!mapUI.activeSelf);

        }

    }

}
