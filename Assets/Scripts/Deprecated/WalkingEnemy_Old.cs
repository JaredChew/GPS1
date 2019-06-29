using UnityEngine;

public class WalkingEnemy_Old : MonoBehaviour {
    /*
    [SerializeField] private LayerMask wallObjects;
    [SerializeField] private LayerMask playerObject;

    [SerializeField] private bool debug = false;
    [SerializeField] private bool lookingRight = true;

    [SerializeField] private float movementSpeed = 3f;
    [SerializeField] private float chaseSpeedMultiplier = 1.5f;
    [SerializeField] private float playerChaseTime = 8f;
    [SerializeField] private float wallVisionDistance = 2f;
    [SerializeField] private float playerVisionDistance = 5f;

    private Transform eyes;
    private Animator enemyAnimator;
    private Rigidbody2D enemyRigidbody;

    private Vector2 facingDirection;

    private bool playerDetected;

    private void Awake() {

        Physics2D.queriesStartInColliders = false;

        facingDirection = Vector2.right;

        playerDetected = false;

    }

    // Use this for initialization
    void Start() {

        //enemyAnimator = GetComponent<Animator>();
        enemyRigidbody = GetComponent<Rigidbody2D>();

        eyes = transform.Find("Eyes").GetComponent<Transform>();

        enemyRigidbody.freezeRotation = true;

        if (!lookingRight) { flip(); }

    }

    private void Update() {

        movement();

    }

    // Update is called once per frame
    void FixedUpdate() {

        //Flip enemy if close to wall
        if (Physics2D.Raycast(eyes.position, facingDirection, wallVisionDistance, wallObjects)) {
            flip();
        }

        playerDetected = Physics2D.Raycast(eyes.position, facingDirection, playerVisionDistance, playerObject);

        // ** DEBUG ** //
        if (debug) { debugVision(); }

    }

    private void movement() {
        
        enemyRigidbody.velocity = new Vector2((playerDetected && Global.gameMngr.getIsNight() ? movementSpeed * chaseSpeedMultiplier : movementSpeed) * facingDirection.x,
                                              enemyRigidbody.velocity.y);

        //enemyAnimator.SetFloat(Global.enemyAnimatorVariable_Velocity, enemyRigidbody.velocity.x); //old
        //enemyAnimator.SetFloat(Global.enemyAnimatorVariable_Velocity, enemyRigidbody.velocity.magnitude);

    }

    private void flip() {

        facingDirection = -facingDirection;

        enemyRigidbody.transform.localScale = new Vector3(Mathf.Abs(enemyRigidbody.transform.localScale.x) * facingDirection.x,
                                                          enemyRigidbody.transform.localScale.y,
                                                          enemyRigidbody.transform.localScale.z);

        
    }

    private void destroyObject() {

        gameObject.SetActive(false);

        Destroy(gameObject);

    }

    public void spawnAt(float x, float y, bool lookingRight) {

        transform.position = new Vector2(x, y);

        if (this.lookingRight != lookingRight) { flip(); }

    }

    private void debugVision() {

        Debug.DrawRay(eyes.position, wallVisionDistance * facingDirection, Color.green);
        Debug.DrawRay(eyes.position, playerVisionDistance * facingDirection, Color.red);

    }
    */
}
