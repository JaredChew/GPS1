using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class Box : MonoBehaviour {

    [SerializeField] private float disabledDuration;
    [SerializeField] private float levitateSpeed;

    [SerializeField] private GameObject rig;

    [SerializeField] private GameObject leftArrow;
    [SerializeField] private GameObject rightArrow;

    private Animator boxAnimator;
    private Transform boxTransform;
    private Rigidbody2D boxRigidbody;
    private BoxCollider2D boxCollider;

    private Vector2 exitHideArrowDirection;

    private float disabledCounter;

    private bool isStored;
    private bool electricCharged;
    private bool isOnGround;
    private bool hidingPlayer;

    private bool[] ability = new bool[Enum.GetNames(typeof(Global.BoxAbilities)).Length];

    void Awake() {

        disabledCounter = 0f;

        exitHideArrowDirection = Vector2.zero;

        isStored = true;
        isOnGround = false;
        electricCharged = false;
        hidingPlayer = false;

    }

    private void Start() {

        boxTransform = GetComponent<Transform>();
        boxRigidbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        boxAnimator = GetComponent<Animator>();

        boxRigidbody.freezeRotation = true;

        gameObject.SetActive(false);

    }

    private void Update() {

        disableRecovery();
        activatingBoxAnimation();

    }

    private void activatingBoxAnimation()
    {
        // for the box change to idle animation after being thrown
        if (isOnGround)
        {
            boxAnimator.SetBool("IsThrown", true);
        }
        // for box hiding animation
        if (Input.GetButtonDown(Global.controlsHide) && hidingPlayer)
        {
            //boxAnimator.SetBool("IsHiding", true);
        }
        else if (!hidingPlayer)
        { 
            //boxAnimator.SetBool("IsHiding", false);
        }
        // for recall box animation
        if (Input.GetButtonDown(Global.controlsRecall))
        {
            StartCoroutine(RecallAnimation());
        }
        // for charge animation and colour charge
        if (electricCharged)
        {
            boxAnimator.SetBool("IsCharged", true);
        }
        // changing back to uncharged from colour charged
        if (!electricCharged)
        {
            boxAnimator.SetBool("IsCharged", false);
        }
    }

    private IEnumerator RecallAnimation()
    {
        boxAnimator.SetTrigger("IsReturned");
        yield return new WaitForSeconds(3f);
        boxAnimator.SetTrigger("BlindReturnAnimation");
        yield return new WaitForSeconds(1f);
        boxAnimator.SetTrigger("FloatingAnimation");    
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

        if (collision.CompareTag(Global.tagBox) && ability[(int)Global.BoxAbilities.electricCharge])
        {
            discharge();
        }

    }


    private void switchExitHideArrow() {

        if(hidingPlayer) {

            switch(exitHideArrowDirection.x) {

                case 1: //right
                    rightArrow.SetActive(true);
                    leftArrow.SetActive(false);
                    break;

                case -1: //left
                    rightArrow.SetActive(false);
                    leftArrow.SetActive(true);
                    break;

            }

        }

    }

    private void disableRecovery() {

        if (disabledCounter > 0) {

            disabledCounter += Time.deltaTime;

            if (disabledCounter >= disabledDuration) {

                disabledCounter = 0;

                rig.SetActive(true);

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

        rig.SetActive(false);

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

        if (disabledCounter == 0) {

            gameObject.SetActive(false);

            isStored = true;
            isOnGround = false;

        }

    }

    public bool hideUnhideMode() {

        if (ability[(int)Global.BoxAbilities.hidePlayer]) {

            boxCollider.isTrigger = !boxCollider.isTrigger;
            boxRigidbody.isKinematic = !boxRigidbody.isKinematic; //not let box slide when hiding

            boxRigidbody.velocity = Vector2.zero;

            hidingPlayer = !hidingPlayer;

            if (!hidingPlayer) {

                leftArrow.SetActive(false);
                rightArrow.SetActive(false);

                exitHideArrowDirection = Vector2.zero;

            }

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

    public void playerHideFacingDirection(float facingDirection) {

        exitHideArrowDirection.x = -facingDirection;

        switchExitHideArrow();

    }

    public void unlockAbility(Global.BoxAbilities boxAbility) {
        ability[(int)boxAbility] = true;
    }

    public bool getIsStored() {
        return isStored;
    }

    public float getExitHideArrowDirection() {
        return exitHideArrowDirection.x;
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
