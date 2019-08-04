using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour {

    private Transform clockTransform;

    private float timeSinceStart;

    private void Start() {

        timeSinceStart = Global.gameManager.getIsNight() ? Global.gameManager.getMaxTimeCycle() : 0;

        clockTransform = GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update() {

        clockTime();

    }

    private void clockTime() {

        if (timeSinceStart > Global.gameManager.getMaxTimeCycle() * 2) {
            timeSinceStart = 0f;//reset time count
        }

        timeSinceStart += Time.deltaTime; //time moves in real life seconds

        float angleToTurn = (180 / Global.gameManager.getMaxTimeCycle()) * timeSinceStart;

        clockTransform.eulerAngles = new Vector3(0, 0, angleToTurn);//clock moves

    }
    
}
