using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour {

    [SerializeField] private float directionSwitchDuration;

    private FlyingEnemyVision visionLeft;
    private FlyingEnemyVision visionRight;

    private float timer;

    private void Awake() {

        timer = 0f;

    }

    private void Start() {

        visionLeft = transform.Find("Vision Left").GetComponent<FlyingEnemyVision>();
        visionRight = transform.Find("Vision Right").GetComponent<FlyingEnemyVision>();

        visionRight.gameObject.SetActive(false);

    }

    private void Update() {

        rotateVision();

    }

    private void rotateVision() {

        if(timer >= directionSwitchDuration) {

            visionLeft.gameObject.SetActive(!visionLeft.gameObject.activeSelf);
            visionRight.gameObject.SetActive(!visionRight.gameObject.activeSelf);

            timer = 0f;

        }

        timer += Time.deltaTime;

    }

}
