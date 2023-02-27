using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    

    
    
    
    
    public Animator animator;
    
    public float turnSpeed = 5f;

    public bool busy;    

    public float moveInput;

    public float offCourseMultiplier = 0.5f;

    OffCourseTimer offCourseTimer;

    Rigidbody2D rb;

    [Header("Controls")]
    private PlayerInputActions playerActionControls;


    [Header("Spawn Behavior")] 
    public Transform endingPosition;
    public float moveVelocity = 3f;

    private void Awake() {

        playerActionControls = new PlayerInputActions();
    }

    private void OnEnable()
    {
        playerActionControls.Enable();
    }

    private void OnDisable()
    {
        playerActionControls.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        offCourseTimer = GetComponent<OffCourseTimer>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = playerActionControls.Player.Turn.ReadValue<float>();

        float offCoursePenalty = offCourseTimer.offCourse ? offCourseMultiplier : 1f;

        HandleSpawnMovePosition();


        if(!busy){
            rb.velocity = new Vector2(moveInput * turnSpeed * offCoursePenalty, 0);
        }

        TurnAnimate();

        SpriteFlip();


    }

    void SpriteFlip() {

        if(rb.velocity.x > 0) {
            transform.localScale = new Vector3(1f,1f,1f);
        } else if (rb.velocity.x < 0) {
            transform.localScale = new Vector3(-1f,1f,1f);
        }
    }

    void TurnAnimate() {

        bool turn = (Mathf.Abs(rb.velocity.x) > 0.2);

        animator.SetBool("Turning", turn);
    }

    void OnTriggerEnter2D(Collider2D other) {

        if(other.gameObject.tag == "Hazard") {
        Debug.Log("Crashing into hazard.  Commence delete protocol");
        PlayerCrash();
        }
    }

    void HandleSpawnMovePosition() {

        if(gameObject.transform.position.y < endingPosition.position.y) {
            busy = true;
            rb.velocity = new Vector2(0, moveVelocity);
        } else {
            busy = false;
        }

    }

    void PlayerCrash() {
        Destroy(gameObject);
        GameManager.instance.HandlePlayerDeath();
    }

}
