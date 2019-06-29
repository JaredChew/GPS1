using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AimingScript : MonoBehaviour {

    Vector2 mousePosition;
    private Vector2 playerPosition;

    private float maxDistance;

    // Update is called once per frame
    void Update() {

        transform.position = new Vector2(
                Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                Camera.main.ScreenToWorldPoint(Input.mousePosition).y
        );
        /*
        //mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Vector2.Distance(mousePosition, playerPosition) < maxDistance) {

            transform.position = mousePosition;

        }
        */
    }

    public void getPlayerPosition(Vector2 position) {
        transform.position = position;
    }

    public void setMaxDistance(float maxDistance) {
        this.maxDistance = maxDistance;
    }

}
