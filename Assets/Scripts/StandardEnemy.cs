using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StandardEnemy : MonoBehaviour {

    private enum EnemyType { overlook, guard, patrol };

    private enum BehaviourState { guard, patrol, chase, returnToGuardSpot, inspect, stunned };

    [SerializeField] private EnemyType enemyType;

    [SerializeField] private Transform patrolWallLeft;
    [SerializeField] private Transform patrolWallRight;

    [SerializeField] private LayerMask wallObjects;
    [SerializeField] private LayerMask playerObject;
    [SerializeField] private LayerMask boxObject;

    [SerializeField] private bool debug = false;
    [SerializeField] private bool lookingRight = true;

    [SerializeField] private float guardRotateTimer = 6f;

    [SerializeField] private float movementSpeed = 3f;
    [SerializeField] private float chaseSpeedMultiplier = 1.5f;

    [SerializeField] private float chaseDuration = 8f;
    [SerializeField] private float inspectDuration = 6f;
    [SerializeField] private float stunnedDuration = 20f;

    [SerializeField] private float visionDistance = 5f;
    [SerializeField] private float wallVisionDistance = 2f;

    private Transform eyes;

    private Transform enemyTransform;
    private Animator enemyAnimator;
    private Rigidbody2D enemyRigidbody;

    private BehaviourState currentState;

    private Vector2 guardingLocation;
    private Vector2 facingDirection;

    private float timer;

    private void Awake() {

        Physics2D.queriesStartInColliders = false;

        currentState = enemyType == EnemyType.patrol ? BehaviourState.patrol : BehaviourState.guard;

        guardingLocation = transform.position;
        facingDirection = Vector2.right;

        timer = 0;

    }

    // Start is called before the first frame update
    void Start() {

        enemyTransform = GetComponent<Transform>();
        eyes = transform.Find("Eyes").GetComponent<Transform>();

        enemyRigidbody = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();

        enemyRigidbody.freezeRotation = true;

        if (!lookingRight) { flip(); }

    }

    // Update is called once per frame
    private void Update() {
        
        // ** DEBUG ** //
        if (debug) { debugVision(); }

    }

    void FixedUpdate() {

        stateUpdate();
        stateController();
        setAnimation();

    }

    private void OnCollisionEnter2D(Collision2D collision) {

        if (collision.collider.CompareTag(Global.tagBox)) {
            collision.collider.gameObject.GetComponent<Box>().disable();
        }

        if (collision.collider.CompareTag(Global.tagPlayer)) {
            collision.collider.gameObject.GetComponent<Player>().killPlayer();
        }

    }

    private void stateUpdate() {

        if (currentState != BehaviourState.stunned) {

            if (currentState != BehaviourState.inspect && Physics2D.Raycast(eyes.position, facingDirection, visionDistance, playerObject)) {
                currentState = BehaviourState.chase;
                setPatrolWall(false);
            }

            else if (currentState != BehaviourState.chase && Physics2D.Raycast(eyes.position, facingDirection, visionDistance, boxObject)) {
                currentState = BehaviourState.inspect;
                setPatrolWall(false);
                
            }

        }

    }

    private void stateController() {

        switch(currentState) {

            case BehaviourState.guard:
                guard();
                break;

            case BehaviourState.patrol:
                patrol();
                break;

            case BehaviourState.inspect:
                inspect();
                break;

            case BehaviourState.chase:
                chase();
                break;

            case BehaviourState.returnToGuardSpot:
                returnToGuardSpot();
                break;

            case BehaviourState.stunned:
                stunned();
                break;

        }

    }

    private void setAnimation()
    {
        //Enemy chasing
        if(currentState == BehaviourState.chase)
        {
            enemyAnimator.SetBool("isAlerted", true);
        }
        else if(currentState == BehaviourState.patrol)
        {
            enemyAnimator.SetBool("isAlerted", false);
            enemyAnimator.SetBool("isWalking", true);
        }

        //Enemy inspect box
        if(currentState == BehaviourState.inspect)
        {
            enemyAnimator.SetBool("isStopped", true);
        }
        else if(currentState == BehaviourState.patrol)
        {
            enemyAnimator.SetBool("isStopped", false);
            enemyAnimator.SetBool("isWalking", true);
        }
    }

    private void stunned() {

        timer += Time.deltaTime;

        if(timer >= stunnedDuration) {
            timer = 0;
            returnToDefaultBehaviour();
        }

    }

    private void returnToGuardSpot() {

        if (Math.Sign(guardingLocation.x - enemyTransform.position.x) + facingDirection.x == 0) {
            flip();
        }

        movement();

        if(enemyTransform.position.x > guardingLocation.x - 3 && enemyTransform.position.x < guardingLocation.x + 3) {
            returnToDefaultBehaviour();
            setPatrolWall(true);
        }

    }

    private void chase() {

        movement();

        timer += Time.deltaTime;

        //sound
        Global.audiomanager.getSFX("enemy_chasing").play();

        if (timer >= chaseDuration || Physics2D.Raycast(eyes.position, facingDirection, wallVisionDistance, wallObjects)) {
            currentState = BehaviourState.returnToGuardSpot;
            timer = 0;
        }

    }

    private void inspect() {

        bool frontOfBox = Physics2D.Raycast(eyes.position, facingDirection, wallVisionDistance, boxObject);

        if (timer >= inspectDuration || (timer != 0 && !frontOfBox)) {
            currentState = BehaviourState.returnToGuardSpot;
            timer = 0;
            flip();
            return;
        }

        if (!frontOfBox) {
            movement();
            return;
        }

        enemyRigidbody.velocity = Vector2.zero;

        if (frontOfBox) { timer += Time.deltaTime; }

    }

    private void patrol() {

        movement();

        if (Physics2D.Raycast(eyes.position, facingDirection, wallVisionDistance, wallObjects)) {
            flip();
        }

    }

    private void guard() {

        if (enemyType == EnemyType.guard) {

            if (timer >= guardRotateTimer) {
                flip();
                timer = 0f;
            }

            timer += Time.deltaTime;

        }

    }

    private void returnToDefaultBehaviour() {

        currentState = enemyType == EnemyType.patrol ? BehaviourState.patrol : BehaviourState.guard;

    }

    private void movement() {

        enemyRigidbody.velocity = new Vector2((Global.gameManager.getIsNight() ? movementSpeed * chaseSpeedMultiplier : movementSpeed) * facingDirection.x,
                                               enemyRigidbody.velocity.y);

        //enemyRigidbody.velocity = new Vector2(movementSpeed * facingDirection.x, enemyRigidbody.velocity.y);

        //enemyAnimator.SetFloat(Global.enemyAnimatorVariable_Velocity, enemyRigidbody.velocity.x); //old
        //enemyAnimator.SetFloat(Global.enemyAnimatorVariable_Velocity, enemyRigidbody.velocity.magnitude);

    }

    private void flip() {

        facingDirection = -facingDirection;

        enemyRigidbody.transform.localScale = new Vector3(Mathf.Abs(enemyRigidbody.transform.localScale.x) * facingDirection.x,
                                                          enemyRigidbody.transform.localScale.y,
                                                          enemyRigidbody.transform.localScale.z);

    }

    private void setPatrolWall(bool active) {

        patrolWallLeft.gameObject.SetActive(active);
        patrolWallRight.gameObject.SetActive(active);

    }

    public void setStateToStun() {
        currentState = BehaviourState.stunned;
    }

    private void debugVision() {

        Debug.DrawRay(eyes.position, wallVisionDistance * facingDirection, Color.green);
        Debug.DrawRay(eyes.position, visionDistance * facingDirection, Color.red);

    }

}
