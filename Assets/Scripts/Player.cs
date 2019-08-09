using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class Player : MonoBehaviour {

    [SerializeField] private float jumpForce = 100f;
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float groundCheckDistance = 5f;
    [SerializeField] [Range(0.01f, 0.99f)] private float crouchSpeedDemultiplier = 0.1f;

    [SerializeField] private float aimMaxDistane;
    [SerializeField] private float boxSeperationDistance = 5f;
    [SerializeField] private float boxDetectDistance = 0.2f;

    [SerializeField] private bool playerLookingRight = true;
    [SerializeField] private bool spriteLookingRight = true;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask boxLayer;

    [SerializeField] private Box arif;
    [SerializeField] private GameObject throwingArc;
    [SerializeField] private GameObject upgrade1;
    [SerializeField] private GameObject upgrade2;
    [SerializeField] private GameObject upgrade3;

    [SerializeField] private Transform groundCheck;

    [SerializeField] private Transform[] animationComponents;

    [SerializeField] private bool debug;

    private SpriteRenderer playerSpriteRenderer;
    private CapsuleCollider2D playerCollider;
    private Rigidbody2D playerRigidBody;
    private Transform playerTransform;
    private Animator playerAnimator;

    private AimingScript boxSilhouette;

    private PlayerController movementControl = null;
    private Vector2 facingDirection;

    private bool isCrouching;
    private bool isJumping;
    private bool isHiding;
    private bool isDead;

    //new for sound
    private bool justJumped;
    //teleport
    private bool inTeleport;

    // Throwing
    private float torque;
    private float firingAngle = 45.0f;

    private void Awake() {

        isCrouching = false;
        isJumping = false;
        isHiding = false;
        isDead = false;

        Physics2D.queriesStartInColliders = false;

        playerTransform = GetComponent<Transform>();
        facingDirection = playerLookingRight ? Vector2.right : Vector2.left;

    }

    // Start is called before the first frame update
    private void Start() {

        throwingArc.SetActive(false);

        playerAnimator = GetComponent<Animator>();
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();

        boxSilhouette = transform.Find("Aiming").GetComponent<AimingScript>();

        playerRigidBody.freezeRotation = true;
        playerSpriteRenderer.enabled = false;

        boxSilhouette.setMaxDistance(aimMaxDistane);

        adjustPlayerAndSpriteFacingPosition();

        movementControl = new PlayerController(ref playerRigidBody, ref playerTransform, ref facingDirection);

    }

    private void Update() {

        movement();
        activatingAnimation();
       

        if (arif.getIsStored()) {

            if (Time.timeScale > 0f) {
                throwAim();
                boxThrow();
            }

        }
        else {

            if (!isHiding && !Global.gameManager.getIsGamePaused()) {
                boxReturn();
            }

            if (Physics2D.Raycast(playerTransform.position, facingDirection, boxDetectDistance, boxLayer)) {
                hideInBox();
            }

            levitateBox();

        }

        isJumping = !Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);

        if (debug) { debugVision(); }

    }


    //also sound
    private void activatingAnimation() {

        // for walking animation
        if ((playerRigidBody.velocity.x > 0 | playerRigidBody.velocity.x < 0) && !isJumping) {
            playerAnimator.SetBool("IsWalking", true);

            //sound
            Global.audiomanager.getSFX("princess_walking").play();
           
        }
        else {

            //sound
            Global.audiomanager.getSFX("princess_walking").stop();

            playerAnimator.SetBool("IsWalking", false);
        }

        // for jumping animation
        if (isJumping && playerRigidBody.velocity.y > 0) {
            playerAnimator.SetBool("IsJumping", true);

            //sound
            justJumped = true;
            Global.audiomanager.getSFX("princess_jumping").play();

        }
        else { //playerRigidBody.velocity.y > 0
            playerAnimator.SetBool("IsJumping", false);


            //sound
            Global.audiomanager.getSFX("princess_jumping").stop();
            if (justJumped && !isJumping)
            {

                Global.audiomanager.getSFX("princess_landing").play();

                justJumped = false;
            }
        }

        // for crouching animation
        if (isCrouching) {
            playerAnimator.SetBool("IsCrouching", true);
        }
        else {
            playerAnimator.SetBool("IsCrouching", false);
        }

        // for crouching and walking animation
        if (isCrouching && playerRigidBody.velocity.x != 0) {
            playerAnimator.SetBool("IsCrouchWalking", true);

            //sound
            Global.audiomanager.getSFX("princess_walking").play();

        }
        else {
            playerAnimator.SetBool("IsCrouchWalking", false);
        }

    }

    private void levitateBox() {

        if (Input.GetButton(Global.controlsLevitate)) {
            arif.levitate();

            //sound
            Global.audiomanager.getSFX("box_levitation").play();
  
        }

    }

    private void hideInBox() {

        if(isHiding) {

            if (Math.Sign(Input.GetAxis(Global.controlsLeftRight)) - facingDirection.x == 0) {

                movementControl.flip(ref facingDirection);

                arif.playerHideFacingDirection(facingDirection.x);

                playerTransform.position = new Vector2(arif.transform.position.x - (arif.transform.localScale.x * facingDirection.x), playerTransform.position.y);

            }

        }

        if (Input.GetButtonDown(Global.controlsHide) && arif.getIsOnGround()) {

            if(arif.hideUnhideMode()) {

                isHiding = !isHiding;

               
                playerRigidBody.velocity = Vector2.zero;
                playerCollider.enabled = !playerCollider.enabled;
                playerRigidBody.isKinematic = !playerRigidBody.isKinematic;

                arif.playerHideFacingDirection(facingDirection.x);

                //sound
                Global.audiomanager.getSFX("box_hiding").play();

                enableDisableSpriteComponents(!isHiding);

            }
            
        }

    }

    private void boxThrow() {

        if (Input.GetButtonUp(Global.controlsThrow) && throwingArc.gameObject.activeSelf && boxSilhouette.gameObject.activeSelf) {

            throwingArc.gameObject.SetActive(false);
            boxSilhouette.gameObject.SetActive(false);

            float target_Distance = Vector3.Distance(boxSilhouette.transform.position, playerTransform.position);

            // Calculate the velocity needed to throw the object to the target at specific angle
            float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / (Physics2D.gravity.y * -1));

            // Extract the X Y component of velocity
            float velocityX = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
            float velocityY = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

            //Vector2 direction = (boxSilhouette.transform.position - playerTransform.position).normalized;
            Vector2 vel = new Vector2(velocityX * facingDirection.x, velocityY); //velocityX * direction.x
            Vector2 spawnPosition = new Vector2(playerTransform.position.x + ((playerSpriteRenderer.size.x / 2) * facingDirection.x), playerTransform.position.y);

            arif.thrown(spawnPosition, vel, ForceMode2D.Impulse, torque);

            //sound
            Global.audiomanager.getSFX("box_throw").play();
   
        }

    }

    private void throwAim() {

        if (Input.GetButtonDown(Global.controlsThrow)) {

            throwingArc.gameObject.SetActive(true);
            boxSilhouette.gameObject.SetActive(true);

            //boxSilhouette.getPlayerPosition(playerTransform.position);

        }

    }

    private void movement() {
   
        if (!isHiding && !Global.gameManager.getIsGamePaused()) {
            movementControl.horizontalMovement(isCrouching ? movementSpeed * crouchSpeedDemultiplier : movementSpeed, ref facingDirection);
            movementControl.Jump(ref isJumping, jumpForce);
            movementControl.crouch(ref isCrouching);
        }

    }

    private void boxReturn() {

        if (Input.GetButtonDown(Global.controlsRecall) || (arif.transform.position - playerTransform.position).magnitude > boxSeperationDistance) {
            arif.store();

            //sound
            Global.audiomanager.getSFX("box_retrieval").play();
            
        }

    }

    private void adjustPlayerAndSpriteFacingPosition() {

        // ! Work in progress ! //

        if (facingDirection != Vector2.right) {

            if (!spriteLookingRight) {

                for (int i = 0; i < animationComponents.Length; i++) {

                    animationComponents[i].parent = playerTransform.parent;

                }

            }

            playerTransform.localScale = new Vector3(Mathf.Abs(playerTransform.transform.localScale.x) * facingDirection.x,
                                                playerTransform.transform.localScale.y,
                                                playerTransform.transform.localScale.z);

            if (!spriteLookingRight) {

                for (int i = 0; i < animationComponents.Length; i++) {

                    animationComponents[i].SetParent(playerTransform);

                }

            }

        }
        else if (!spriteLookingRight) {

            for (int i = 0; i < animationComponents.Length; i++) {

                animationComponents[i].localScale = new Vector3(-animationComponents[i].localScale.x,
                        animationComponents[i].localScale.y,
                        animationComponents[i].localScale.z);

            }

        }

    }

    private void enableDisableSpriteComponents(bool enable) {

        for (int i = 0; i < animationComponents.Length; i++) {

            animationComponents[i].gameObject.SetActive(enable);

        }

        playerAnimator.enabled = !playerAnimator.enabled;

    }

    //animation portal make invisible
    public void teleportAnim(bool activate)
    {
        playerRigidBody.velocity = Vector2.zero;
        inTeleport = !inTeleport;
        enableDisableSpriteComponents(activate);
    }

    public bool getIsDead() {
        return isDead;
    }

    public bool getIsPlayerHiding() {
        return isHiding;
    }

    public void setPlayerPosition(Vector2 position) {
        playerTransform.position = new Vector3(position.x, position.y, playerTransform.position.z);
    }

    public Vector3 getPosition() {
        return playerTransform.position;
    }

    public float getFacingDirection() {
        return facingDirection.x;
    }

    public void setFacingDirection(float facingDirection) {
        this.facingDirection.x = facingDirection;
    }

    public bool[] getBoxUpgradeStatus() {

        bool[] boxAbilities = new bool[Enum.GetNames(typeof(Global.BoxAbilities)).Length];

        for(int i = 0; i < Enum.GetNames(typeof(Global.BoxAbilities)).Length; i++) {

            boxAbilities[i] = arif.getIsAbilityUnlocked((Global.BoxAbilities)i);

        }

        return boxAbilities;

    }

    public void upgradeBox(Global.BoxAbilities ability) {

        arif.unlockAbility(ability);

        if(ability == Global.BoxAbilities.hidePlayer)
        {
            upgrade1.SetActive(true);
        }
        else if(ability == Global.BoxAbilities.levitate)
        {
            upgrade2.SetActive(true);
        }
        else if (ability == Global.BoxAbilities.electricCharge)
        {
            upgrade3.SetActive(true);
        }
    }

    public void killPlayer() {
        //sound
        Global.audiomanager.getSFX("princess_death").play();

        isDead = true;
    }

    private void debugVision() {

        Debug.DrawRay(playerTransform.position, boxDetectDistance * facingDirection, Color.red);
        Debug.DrawRay(groundCheck.position, groundCheckDistance * Vector2.down, Color.blue);

    }

}
