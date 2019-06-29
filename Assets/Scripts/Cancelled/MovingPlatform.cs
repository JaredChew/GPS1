using UnityEngine;
using System;

public class MovingPlatform : MonoBehaviour {

    [SerializeField] private float movementSpeed = 0.3f;
    [SerializeField] private float distanceToMove = 0f;

    [SerializeField] private bool moveVertically = false;
    [SerializeField] private bool moveHorizontally = false;

    private Transform platformTransform;

    private int direction = 1;

    private float savedPositionX;
    private float savedPositionY;

    private void Awake() {

        platformTransform = GetComponent<Transform>();

        savedPositionX = platformTransform.position.x;
        savedPositionY = platformTransform.position.y;

    }

    // Update is called once per frame
    void Update() {

        inverseMovement();

    }

    private void FixedUpdate() {

        movement();

    }

    private void inverseMovement() {

        if (Math.Abs(platformTransform.position.x - savedPositionX) >= (moveHorizontally ? distanceToMove : 0) &&
            Math.Abs(platformTransform.position.y - savedPositionY) >= (moveVertically ? distanceToMove : 0)) {
            
            direction = -direction;

            savedPositionX = platformTransform.position.x;
            savedPositionY = platformTransform.position.y;

        }

    }

    private void movement() {

        platformTransform.position = new Vector2(platformTransform.position.x + (moveHorizontally && moveVertically ? 0.75f * movementSpeed * direction :
                                                                                        (moveHorizontally ? movementSpeed * direction : 0)),
                                                 platformTransform.position.y + (moveHorizontally && moveVertically ? 0.75f * movementSpeed * direction :
                                                                                        (moveVertically ? movementSpeed * direction : 0)));

    }

    void OnCollisionEnter2D(Collision2D collision) {

        collision.gameObject.transform.parent = platformTransform;

    }

    void OnCollisionExit2D(Collision2D collision) {

        collision.gameObject.transform.parent = null;

    }

}
