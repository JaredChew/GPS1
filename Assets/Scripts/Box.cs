﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class Box : MonoBehaviour {

    [SerializeField] private float disabledDuration;
    [SerializeField] private float Force;

    private Transform boxTransform;
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

        boxTransform = GetComponent<Transform>();
        boxRigidbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();

        boxRigidbody.freezeRotation = true;

        gameObject.SetActive(false);

    }

    private void Update() {

        disableRecovery();

    }

    private void OnCollisionEnter2D(Collision2D collision) {

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

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.CompareTag(Global.tagCharger) && ability[(int)Global.BoxAbilities.electricCharge]) {
            electricCharged = true;
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

    public bool hideUnhideMode() {

        if (ability[(int)Global.BoxAbilities.hidePlayer]) {

            boxCollider.isTrigger = !boxCollider.isTrigger;
            boxRigidbody.velocity = Vector2.zero;
            boxRigidbody.isKinematic = !boxRigidbody.isKinematic; //not let box slide when hiding

            return true;

        }

        return false;

    }

    public void thrown(Vector2 spawnAt, Vector2 force, ForceMode2D throwType, float torque) { //Vector2 spawnAt, Vector2 force, ForceMode2D throwType, float torque

        gameObject.SetActive(true);
        isStored = false;
        
        boxTransform.position = spawnAt;

        boxRigidbody.AddForce(force, throwType);
        boxRigidbody.AddTorque(torque);
        
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
    /*
    public BoxCollider2D getCollider() {
        return boxCollider;
    }

    public Transform getTransform() {
        return boxTransform;
    }
    */

    private void FixedUpdate()
    {
        LevitateMovement();
    }

    void LevitateMovement()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * Force);
        }
    }
}
