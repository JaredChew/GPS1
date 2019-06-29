using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class Box : MonoBehaviour {

    [SerializeField] private float disabledDuration;

    private Rigidbody2D boxRigidbody;
    private BoxCollider2D boxCollider;

    private float disabledCounter;

    private bool isStored;
    private bool electricCharged;
    private bool isOnGround;

    private bool[] ability = new bool[Enum.GetNames(typeof(Global.BoxAbilities)).Length];

    void Awake() {

        disabledCounter = 0f;

        isStored = true;
        isOnGround = false;
        electricCharged = false;

    }

    private void Start() {

        boxRigidbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();

        boxRigidbody.freezeRotation = true;

        gameObject.SetActive(false);

    }

    private void Update() {

        disableRecovery();

    }

    private void OnCollisionEnter2D(Collision2D collision) {
        
        if(collision.collider.CompareTag(Global.tagCharger)) {
            electricCharged = true;
        }

        if (collision.collider.CompareTag(Global.tagGround)) {
            isOnGround = true;
            boxRigidbody.velocity = Vector2.zero;
        }

    }

    private void OnCollisionExit2D(Collision2D collision) {

        if (collision.collider.CompareTag(Global.tagGround)) {
            isOnGround = false;
        }

    }

    private void disableRecovery() {

        if (disabledCounter > 0) {

            disabledCounter += Time.deltaTime;

            if (disabledCounter >= disabledDuration) {
                disabledCounter = 0;
            }

        }

    }

    public void discharge() {
        electricCharged = false;
    }

    public void disable() {

        store();

        disabledCounter += 0.1f;

    }

    public void store() {

        gameObject.SetActive(false);

        isStored = true;
        isOnGround = false;

    }

    public void thrown() { //float x, float y, Vector2 force, ForceMode2D throwType, float torque

        gameObject.SetActive(true);
        isStored = false;
        /*
        boxTransform.Translate(x, y, boxTransform.position.z);

        boxRigidbody.AddForce(force, throwType);
        boxRigidbody.AddTorque(torque);
        */
    }

    public void unlockAbility(Global.BoxAbilities boxAbility) {
        ability[(int)boxAbility] = true;
    }

    public bool getIsStored() {
        return isStored;
    }

    public bool getIsElectricallyCharged() {
        return electricCharged;
    }

    public bool getIsOnGround() {
        return isOnGround;
    }

    public bool getIsAbilityUnlocked (Global.BoxAbilities boxAbility) {
        return ability[(int)boxAbility];
    }

    public Rigidbody2D getRigidbody() {
        return boxRigidbody;
    }

    public BoxCollider2D getCollider() {
        return boxCollider;
    }

}
