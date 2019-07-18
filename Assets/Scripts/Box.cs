using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class Box : MonoBehaviour {

    [SerializeField] private float disabledDuration;
    [SerializeField] private float levitateSpeed;

    [SerializeField] private Animator boxAnimator;
    [SerializeField] Player playerScript;

    private Transform boxTransform;
    private Rigidbody2D boxRigidbody;
    private BoxCollider2D boxCollider;
    private SpriteRenderer boxRenderer;

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
        boxRenderer = GetComponent<SpriteRenderer>();

        boxRigidbody.freezeRotation = true;

        gameObject.SetActive(false);

    }

    private void Update() {

        disableRecovery();
        activatingBoxAnimation();

    }

    private void activatingBoxAnimation()
    {
        // for box hiding animation
        if (Input.GetButtonDown(Global.controlsHide) && playerScript.getIsPlayerHiding())
        {
            boxAnimator.SetBool("IsHiding", true);
        }
        else
        {
            boxAnimator.SetBool("IsHiding", false);
        }
        // for recall box animation
        if (isStored && Input.GetButtonDown(Global.controlsRecall))
        {
            boxAnimator.SetBool("IsReturned", true);
        }
        else if (!isStored)
        {
            boxAnimator.SetBool("IsReturned", false);
        }
        // for charge animation and colour charge
        if (electricCharged)
        {
            boxAnimator.SetBool("IsCharged", true);
        }
        // changing back to uncharged from colour charged
        if (!electricCharged && !isStored)
        {
            boxAnimator.SetBool("IsCharged", false);
            boxAnimator.SetBool("IsReturned", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {

        if (collision.collider.CompareTag(Global.tagGround)) {
            isOnGround = true;
            boxRigidbody.velocity = Vector2.zero;
        }

        if(collision.collider.CompareTag(Global.tagEnemy) && electricCharged) {
            collision.collider.GetComponent<StandardEnemy>().setStateToStun();
        }

    }

    private void OnCollisionExit2D(Collision2D collision) {

        if (collision.collider.CompareTag(Global.tagGround)) {
            isOnGround = false;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.CompareTag(Global.tagCharger) && ability[(int)Global.BoxAbilities.electricCharge]) {

            //sound
            Global.audiomanager.getSFX("box_charge_with_elec").play();
       
            electricCharged = true;

        }

    }

    private void disableRecovery() {

        if (disabledCounter > 0f) {

            disabledCounter += Time.deltaTime;

            if (disabledCounter >= disabledDuration) {
                disabledCounter = 0;

                boxRenderer.enabled = true;
                boxCollider.isTrigger = false;
                boxRigidbody.isKinematic = false;

                store();
            }

        }

    }

    public void discharge() {
        electricCharged = false;
    }

    public void disable() {

        boxRenderer.enabled = false;
        boxCollider.isTrigger = true;
        boxRigidbody.isKinematic = true;

        boxRigidbody.velocity = Vector2.zero;

        disabledCounter += 0.1f;

    }

    public void levitate() {

        if (ability[(int)Global.BoxAbilities.levitate]) {
            boxRigidbody.AddRelativeForce(Vector2.up * levitateSpeed);
        }

    }

    public void store() {

        gameObject.SetActive(false);

        isStored = true;
        isOnGround = false;

    }

    public bool hideUnhideMode() {

        if (ability[(int)Global.BoxAbilities.hidePlayer]) {

            
            boxCollider.isTrigger = !boxCollider.isTrigger;
            boxRigidbody.isKinematic = !boxRigidbody.isKinematic; //not let box slide when hiding

            boxRigidbody.velocity = Vector2.zero;

            return true;

        }

        return false;

    }

    public void thrown(Vector2 spawnAt, Vector2 force, ForceMode2D throwType, float torque) { //Vector2 spawnAt, Vector2 force, ForceMode2D throwType, float torque

        if (disabledCounter == 0) {

            gameObject.SetActive(true);
            isStored = false;

            boxTransform.position = spawnAt;

            boxRigidbody.AddForce(force, throwType);
            boxRigidbody.AddTorque(torque);

        }

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

}
