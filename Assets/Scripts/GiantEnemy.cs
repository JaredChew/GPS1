using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GiantEnemy : MonoBehaviour {

    [SerializeField] private float leftPushRange;
    [SerializeField] private float rightPushRange;
    [SerializeField] private float velocityThresHold;

    private SpriteRenderer parentRenderer;

    private Rigidbody2D giantEnemyRigidbody;
    private SpriteRenderer giantEnemyRenderer;
    private EdgeCollider2D giantEnemyCollider;

    private void Start() {

        parentRenderer = transform.parent.GetComponent<SpriteRenderer>();

        giantEnemyRigidbody = GetComponent<Rigidbody2D>();
        giantEnemyRenderer = GetComponent<SpriteRenderer>();
        giantEnemyCollider = GetComponent<EdgeCollider2D>();

        giantEnemyRigidbody.angularVelocity = velocityThresHold;

        spawnDespawn();

    }

    // Update is called once per frame
    void Update() {

        GiantVison();
        spawnDespawn();

    }

    void spawnDespawn() {

        if(Global.gameManager.getIsNight() == parentRenderer.enabled) {

            parentRenderer.enabled = !parentRenderer.enabled;

            giantEnemyRenderer.enabled = !giantEnemyRenderer.enabled;
            giantEnemyCollider.enabled = !giantEnemyCollider.enabled;

        }

    }

    //GiantVision
    void GiantVison() {

        if (transform.rotation.z > 0  && transform.rotation.z < rightPushRange
            && giantEnemyRigidbody.angularVelocity > 0
            && giantEnemyRigidbody.angularVelocity < velocityThresHold) {

            giantEnemyRigidbody.angularVelocity = velocityThresHold;

        }
        else if (transform.rotation.z < 0 && transform.rotation.z > leftPushRange
            && giantEnemyRigidbody.angularVelocity < 0
            && giantEnemyRigidbody.angularVelocity > velocityThresHold * -1) {

            giantEnemyRigidbody.angularVelocity = velocityThresHold * -1;

        }

    }

    private void OnTriggerEnter2D(Collider2D collider) {

        if (collider.CompareTag(Global.tagPlayer)) {
            collider.gameObject.GetComponent<Player>().killPlayer();
        }

    }

}
